using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Items;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using System;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using System.IO;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using System.Collections.Generic;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.GameContent.Bestiary;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using Terraria.DataStructures;
using Terraria.Audio;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod;
using CalamityMod.NPCs.TownNPCs;
using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator
{
    [AutoloadBossHead]
    public class DesertAniquilator : ModNPC
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Desert Annihilator");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aniquilador del desierto");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Aniquilateur du désert");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                //CustomTexturePath = "RemnantOfTheAncientsMod/Content/NPCs/Bosses/DAniquilator/DesertAniquilator",
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f,
                Frame = 0,
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }  
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 3500;// (int)NpcChanges1.ExpertLifeScale(3500);
            NPC.damage = 30;// (int)NpcChanges1.ExpertDamageScale(30);
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.width = 100;
            NPC.height = 100;
            NPC.value = Item.buyPrice(0, 2, 75, 45);
            NPC.npcSlots = 30f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[BuffID.OnFire] = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Desert_Aniquilator");
            NPC.netAlways = true;
            AnimationType = NPCID.BlueSlime;
            NPC.stepSpeed = 8f;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {
            NPC.Calamity().canBreakPlayerDefense = true;
            RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC, 0.02f, 0.22f, 0.3f, 0.4f, 0.4f);   
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                //new FlavorTextBestiaryInfoElement("A great and dreaded worm rules the underworld with an iron fist and his flames, powerful and majestic in equal parts, maintain the order and warmth of the underworld.")
            });
        }
        public bool InfernumMode = DificultyUtils.InfernumMode;

        private int attackCounter;
        private int tornadoCounter;
        private int summonCounter;
        private int tpDirection;
        private int currentPhase;
        private bool BossIsInRage = false;
        Point NpcFloor; 
        int tpParticleTimer = (int)Utils1.FormatTimeToTick(0,0,0,5);
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
            writer.Write(summonCounter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
            summonCounter = reader.ReadInt32();
        } 

        public static float ScreenAnimationTimer = Utils1.FormatTimeToTick(0,0,0,5);
        public static bool NoAI = RemnantOfTheAncientsMod.InfernumMod != null;
        public override void AI()
        {

            if (RemnantOfTheAncientsMod.InfernumMod != null)
            {
                if (ScreenAnimationTimer > 0)
                {
                    NPC.velocity = Vector2.Zero;
                    ScreenAnimationTimer--;
                    return;
                }
                else
                {
                    NoAI = false;
                }
            }
            Player player = Main.player[NPC.target];
            if (!NoAI)
            {
                NPC.ai[0] = 10;
                NPC.ai[1] = (NPC.ai[1] + 1) % 800;
               // NPC.ai[2]++;

                BossIsInRage = CheckRage(player);
                NPC.scale = LifeSize(NPC);
                float distance = NPC.Distance(player.Center);
                if (distance >= 110 * 16 && !player.dead && !DificultyUtils.ReaperMode)
                {
                    GenerateTpParticles();
                    DesertTp();
                }
                //if(InfernumMode != null)
                //{     
                movmentAi();
                //}
                NpcFloor = Utils.ToTileCoordinates(NPC.Center);
                Point PlayerFloor = Utils.ToTileCoordinates(Main.player[NPC.target].Center);

                if (Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidAmount > 0)
                {
                    if (Main.tile[PlayerFloor.X, PlayerFloor.Y + 1].LiquidAmount == 0)
                    {
                        GenerateTpParticles();
                        DesertTp();
                    }
                }
                if (player.dead || NPC.target < 0 || NPC.target == 255 || !player.active)
                {
                    NPC.TargetClosest(true);
                }
                setCurrentPhase(NPC);
                AttackIA(player);

                //Main.NewText(NPC.velocity);

                if (player.dead)
                {
                    NPC.EncourageDespawn(7);
                    DespawnBoss();
                }


                if (RemnantOfTheAncientsMod.FargosSoulMod != null)
                {
                    EthernityIa(player);
                }
            }
        }
        #region Movment
        public void movmentAi()
        {
            // Reset NPC.ai[1] if it falls within a certain range
            if (NPC.ai[1] >= 1f && NPC.ai[1] <= 3f)
            {
                NPC.ai[1] = -1f;
            }

            // Handle special behavior for ai[1] == 75f
            if (NPC.ai[1] == 75f)
            {
                HandleSpecialBehavior();
            }

            // Handle resetting ai[1] and other behavior
            HandleResetBehavior();

            // Reset NPC frame and rotation if ai[0] is -999f
            if (NPC.ai[0] == -999f)
            {
                ResetNPCFrameAndRotation();
                return;
            }

            // Check if NPC should be aggressive
            bool isAggressive = ShouldBeAggressive();

            // Handle behavior when NPC is wet
            if (NPC.wet)
            {
                HandleWetBehavior(isAggressive);
            }

            // Reset aiAction and set ai[0] and ai[2] if ai[2] is 0f
            HandleResetAiAction();

            // Handle movement when NPC velocity.Y is 0f
            if (NPC.velocity.Y == 0f)
            {
                bool flag = !Main.dayTime || NPC.life != NPC.lifeMax || NPC.position.Y > Main.worldSurface * 16.0 || Main.slimeRain;

                if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.position.X -= NPC.velocity.X + NPC.direction;
                }

                if (NPC.ai[3] == NPC.position.X)
                {
                    NPC.direction *= -1;
                    NPC.ai[2] = 200f;
                }

                NPC.ai[3] = 0f;
                NPC.velocity.X *= 0.8f;

                if (Math.Abs(NPC.velocity.X) < 0.1)
                {
                    NPC.velocity.X = 0f;
                }

                if (flag)
                {
                    NPC.ai[0] += 1f;
                }

                NPC.ai[0] += 1f;

                float intervalo = -1000f;
                int num34 = 0;

                if (NPC.ai[0] >= intervalo)
                {
                    num34 = 1;
                }

                else if (NPC.ai[0] >= intervalo * 2f)
                {
                    num34 = 3;
                }
                else if (NPC.ai[0] >= intervalo * 0.5f)
                {
                    num34 = 2;
                }

                if (num34 > 0)
                {
                    HandleNum34Behavior(num34, flag, intervalo);
                }
                else if (NPC.ai[0] >= -30f)
                {
                    NPC.aiAction = 1;
                }
            }
            else if (NPC.target < 255 && ((NPC.direction == 1 && NPC.velocity.X < 3f) || (NPC.direction == -1 && NPC.velocity.X > -3f)))
            {
                HandleXMovement();
            }
        }

        // Handles special behavior for ai[1] == 75f
        private void HandleSpecialBehavior()
        {
            float num = 0.3f;
            Lighting.AddLight((int)(NPC.Center.X / 16f), (int)(NPC.Center.Y / 16f), 0.8f * num, 0.7f * num, 0.1f * num);
            if (Main.rand.NextBool(12))
            {
                CreateDustEffect();
            }
        }

        // Handles creating a dust effect
        private void CreateDustEffect()
        {
            Dust dust = Dust.NewDustPerfect(NPC.Center + new Vector2(0f, (float)NPC.height * 0.2f) + Main.rand.NextVector2CircularEdge(NPC.width, (float)NPC.height * 0.6f) * (0.3f + Main.rand.NextFloat() * 0.5f), 228, new Vector2(0f, (0f - Main.rand.NextFloat()) * 0.3f - 1.5f), 127);
            dust.scale = 0.5f;
            dust.fadeIn = 1.1f;
            dust.noGravity = true;
            dust.noLight = true;
        }

        // Handles resetting ai[1] and other behavior
        private void HandleResetBehavior()
        {
            if (NPC.ai[1] == 0f && Main.netMode != NetmodeID.MultiplayerClient && NPC.value > 0f)
            {
                ResetAiAndNetUpdate();
            }

            // Additional conditions...
        }

        // Handles resetting ai[1] and setting netUpdate to true
        private void ResetAiAndNetUpdate()
        {
            NPC.ai[1] = -1f;
            if (Main.remixWorld && NPC.ai[0] != -999f && Main.rand.NextBool(3))
            {
                NPC.ai[1] = 75f;
                NPC.netUpdate = true;
            }
        }

        // Handles resetting NPC frame and rotation
        private void ResetNPCFrameAndRotation()
        {
            NPC.frame.Y = 0;
            NPC.frameCounter = 0.0;
            NPC.rotation = 0f;
        }

        // Checks if NPC should be aggressive
        private bool ShouldBeAggressive()
        {
            bool flag = !Main.dayTime || NPC.life != NPC.lifeMax || NPC.position.Y > Main.worldSurface * 16.0 || Main.slimeRain;
            return flag;
        }

        // Handles behavior when NPC is wet
        private void HandleWetBehavior(bool isAggressive)
        {
            if (NPC.collideY)
            {
                NPC.velocity.Y = -2f;
            }
            if (NPC.velocity.Y < 0f && NPC.ai[3] == NPC.position.X)
            {
                NPC.direction *= -1;
                NPC.ai[2] = 200f;
            }
            if (NPC.velocity.Y > 0f)
            {
                NPC.ai[3] = NPC.position.X;
            }
            if (NPC.velocity.Y > 2f)
            {
                NPC.velocity.Y *= 0.9f;
            }
            NPC.velocity.Y -= 0.5f;
            if (NPC.velocity.Y < -4f)
            {
                NPC.velocity.Y = -4f;
            }

            if (NPC.ai[2] == 1f && isAggressive)
            {
                NPC.TargetClosest();
            }
        }

        // Handles resetting aiAction and setting ai[0] and ai[2]
        private void HandleResetAiAction()
        {
            NPC.aiAction = 0;
            if (NPC.ai[2] == 0f)
            {
                NPC.ai[0] = -100f;
                NPC.ai[2] = 1f;
                NPC.TargetClosest();
            }
        }

        // Handles behavior based on the value of num34
        private void HandleNum34Behavior(int num34, bool isAggressive, float intervalo)
        {
            NPC.netUpdate = true;

            if (isAggressive && NPC.ai[2] == 1f)
            {
                NPC.TargetClosest();
            }

            if (num34 == 3)
            {
                NPC.velocity.Y = -8f;
                NPC.velocity.X += 3 * NPC.direction;
                NPC.ai[0] = -200f;
                NPC.ai[3] = NPC.position.X;
            }
            else
            {
                NPC.velocity.Y = -6f;
                NPC.velocity.X += 2 * NPC.direction;
                NPC.ai[0] = -120f;

                if (num34 == 1)
                {
                    NPC.ai[0] += intervalo;
                }
                else
                {
                    NPC.ai[0] += intervalo * 2f;
                }
            }
        }

        // Handles X movement logic
        private void HandleXMovement()
        {
            if (NPC.collideX && Math.Abs(NPC.velocity.X) == 0.2f)
            {
                NPC.position.X -= 1.4f * NPC.direction;//1.4
            }

            if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                NPC.position.X -= NPC.velocity.X + NPC.direction;
            }

            float acceleration = 1.4f * Main.player[NPC.target].maxRunSpeed;//0.2
            float deceleration = 0.93f;

            if ((NPC.direction == -1 && NPC.velocity.X < 0.01f) || (NPC.direction == 1 && NPC.velocity.X > -0.01f))
            {
                NPC.velocity.X += acceleration * NPC.direction;
            }
            else
            {
                NPC.velocity.X *= deceleration;
            }
        }
        #endregion
        public static bool CheckRage(Player player) => !player.dead && player.active && !player.ZoneDesert && !player.ZoneUndergroundDesert;
        private void AttackIA(Player target)
        {
            int mark = NPC.target;
            List<int[]> AttackValue = setAttackCounter();
            UpdateCounters(AttackValue);

            if (DificultyUtils.ReaperMode)
            {
                for (int i = 1; i <= 2; i++)
                {
                    for (int j = 0; j < AttackValue[i].Length; j++)
                    {
                        AttackValue[i][j] -= (int)Utils1.FormatTimeToTick(0, 0, 0, 2);
                    }
                }
            }
            if (BossIsInRage || InfernumMode)
            {
                for (int i = 1; i <= 2; i++)
                {
                    for (int j = 0; j < AttackValue[i].Length; j++)
                    {
                        AttackValue[i][j] -= (int)Utils1.FormatTimeToTick(0, 0, 0, 4);
                    }
                }
            }
            SummonAI(AttackValue);
            if (Main.expertMode)
            {
                ShootAI(AttackValue, target);
                TornadoAI(mark);
            }
            if (attackCounter == 500)
            {
                DesertTp();
            }
            if (Utils1.NumberBetween(500, 560, attackCounter))
            {
                GenerateTpParticles();
            }
        }
        public void ShootAI(List<int[]> AttackValue, Player target)
        {
            int type = DificultyUtils.ReaperMode? ModContent.NPCType<DesertTyphoonParry>(): ModContent.ProjectileType<DesertTyphoon>();
            bool proj = !DificultyUtils.ReaperMode;

            for(int i = 0; i < 4; i++) 
            {
                if (attackCounter == AttackValue[1][i])
                {
                    int damage = (int)NpcChanges1.ExpertDamageScale(20);
                    ShootHelper(damage, type, target, 12f, 0.5f * Utils1.GetSign(Main.rand.Next(-4,4)), 0.5f * Utils1.GetSign(Main.rand.Next(-4, 4)), proj);
                }
            }  
            if (attackCounter == AttackValue[1][5])
            {
                if (BossIsInRage || InfernumMode)
                {
                    for (int i = 0; i <= 7; i++)
                    {
                        ShootHelper((int)NpcChanges1.ExpertDamageScale(20), type, target, 12f + i, -0.5f + i, 0.5f, proj);
                    }  
                }
                else
                {
                    for (float i = 0f; i < (DificultyUtils.ReaperMode ? 2 : 0); i += 0.5f)
                    {
                        ShootHelper((int)NpcChanges1.ExpertDamageScale(20), type, target, 12f, -3.5f + i, 3.5f - i, proj);
                    }
                }
            }
        }
        public void SummonAI(List<int[]> AttackValue)
        {

            if (summonCounter == AttackValue[2][0] || summonCounter == AttackValue[2][1])
            {
                int NumberOfNPCs = BossIsInRage ? 4 : 0;

                for (int i = 0; i <= NumberOfNPCs; i++)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DesertAnnihilatorServant>());
                }
            }

            if (summonCounter == AttackValue[2][2] && Main.expertMode && Main.tile[NpcFloor.X, NpcFloor.Y + 1].TileType == TileID.Sand)
            {
                int NumberOfNPCs = BossIsInRage ? 4 : 0;

                for (int i = 0; i <= NumberOfNPCs; i++)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)Main.worldSurface + (3 * 16), NPCID.TombCrawlerHead);
                }
            }
            if (summonCounter == AttackValue[2][3])
            {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<FlyingAntlionD>());
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<WalkingAntlionD>());
            }
            if (summonCounter == AttackValue[2][4])
            {
                if (currentPhase >= 2)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)Main.worldSurface + (3 * 16), NPCID.DuneSplicerHead);

                    if (Main.expertMode)
                    {
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.DesertScorpionWalk);
                    }
                }
            }

        }
        public void TornadoAI(int mark)
        {
            int damage = 30;
            if (tornadoCounter == 0 && NPC.life > Utils1.GetValueFromPorcentage(NPC.lifeMax, SetFinalStagePorcentage()))
            {
                tornadoCounter = (int)Utils1.FormatTimeToTick(0, 0, 0, 5);
            }
            else if (tornadoCounter > 0)
            {
                tornadoCounter--;
            }
            else
            {
                tornadoCounter = 0;
            }

            if (!DificultyUtils.ReaperMode)
            {
                if (tornadoCounter == 0)
                {
                    mark = Projectile.NewProjectile(Projectile.GetSource_None(), Main.player[NPC.target].position, Vector2.Zero, ProjectileID.SandnadoHostileMark, 0, 0, Main.myPlayer);
                }
                damage = 30;           
            }
            else
            {
                float distanceBetweenTornados = 5f;
                if (tornadoCounter == 1)
                {
                    for (int a = 0; a < 7; a++)
                    {
                        mark = Projectile.NewProjectile(Projectile.GetSource_None(), Main.player[NPC.target].position + new Vector2(a * 16 * distanceBetweenTornados * Main.player[NPC.target].direction, 0), Vector2.Zero, ProjectileID.SandnadoHostileMark, 0, 0, Main.myPlayer);
                    }
                    damage = 40;
                }           
            }
            if (tornadoCounter == (int)Utils1.FormatTimeToTick(0, 0, 0, 4))
            {
                Projectile.NewProjectile(Projectile.GetSource_None(), Main.projectile[mark].position, Vector2.Zero, ProjectileID.SandnadoHostile, damage, 1, Main.myPlayer);
            }

        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public void EthernityIa(Player player)
        {
            if (DificultyUtils.MasochistMode || DificultyUtils.EternityMode)
            {
                if (attackCounter == (int)Utils1.FormatTimeToTick(0, 0, 0, 7))
                {

                    NPC.netUpdate = true;

                    //EModeNPCBehaviour.NetSync(NPC);
                    if (FargowiltasSouls.FargoSoulsUtil.HostCheck)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero,ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.FargosSoulMod,"GlowRing"), 0, 0f, Main.myPlayer, NPC.whoAmI, -19);
                    }
                    if (NPC.HasValidTarget)
                    {
                        SoundEngine.PlaySound(in SoundID.ForceRoarPitched, Main.player[NPC.target].Center);
                    }
                }
                if (attackCounter == (int)Utils1.FormatTimeToTick(0, 0, 0, 7) - 10)
                {
                    Vector2 start = new Vector2(player.position.X + (-100 * 16),  (player.position.Y - 100*16));
                    Vector2 end = new Vector2(player.position.X + (100 *16), (player.position.Y - 100*16));

                    int numberOfProjectiles = DificultyUtils.EternityMode ? 40: 80;

                    List<Vector2> points = GeneratePoints(start, end, numberOfProjectiles);

                    foreach (var point in points)
                    {
                        //Center.X += (i + spacing) * 16 / numberOfProjectiles * Utils1.GetSign(i);
                        var p = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(point.X,point.Y +(100f *16)), Vector2.Zero, ExternalModCallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "WOFReticle"), 0, 0f, Main.myPlayer);
                        Main.projectile[p].scale = 0.5f;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), point, Vector2.Zero, ProjectileID.RollingCactus, 100, 0f, Main.myPlayer);
                    }



                    static List<Vector2> GeneratePoints(Vector2 start, Vector2 end, int pointCount)
                    {
                        List<Vector2> points = new List<Vector2>();

                        for (int i = 0; i < pointCount; i++)
                        {
                            float t = i / (float)(pointCount - 1);
                            Vector2 point = Vector2.Lerp(start, end, t);
                            points.Add(point);
                        }

                        return points;
                    }
                }
            }
        }
        public static bool spawnGuardians = true;
        public override void HitEffect(NPC.HitInfo hit)
        {
            int choice = Main.rand.Next(2, 8);
            if (DificultyUtils.ReaperMode || BossIsInRage) choice *= 2;
            for (int i = 0; i < choice; i++)
            {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DesertAnnihilatorServant>());
            }
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("DesertAniquilatorGore").Type, NPC.scale);         
                }
                for (int j = 0; j < new RemnantOfTheAncientsMod().ParticleMeter(1000); j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Sandstorm,hit.HitDirection, -1f);
                }
            }
            if (spawnGuardians && InfernumMode)
            {
                int m = -1;
                for (int i = 0; i < 2; i++)
                { 
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X + 10 * m, (int)NPC.position.Y, NPCType<DesertAnnihilatorGuard>());
                    m *= -1;
                }
                spawnGuardians = false;
            }
        }

        public int SetFinalStagePorcentage()
        {
            if (DificultyUtils.MasochistMode) return 15;
            else if (DificultyUtils.EternityMode || DificultyUtils.InfernumMode) return 10;
            return 5;
        }
        public List<int[]> setAttackCounter()
        {
            int[] MaxCounter = new int[2] { 0, 0 };
            int[] AttackCounter = new int[6] { 0, 0, 0, 0, 0, 0 };
            int[] SummonCounter = new int[5] { 0, 0, 0, 0, 0};

            if (!Main.expertMode && !Main.masterMode)
            {
                MaxCounter[0] = (int)Utils1.FormatTimeToTick(0, 0, 0, 15);
                MaxCounter[1] = (int)Utils1.FormatTimeToTick(0, 0, 0, 14);
            }
            else if (Main.expertMode && !Main.masterMode)
            {
                MaxCounter[0] = (int)Utils1.FormatTimeToTick(0, 0, 0, 13);
                MaxCounter[1] = (int)Utils1.FormatTimeToTick(0, 0, 0, 13);
            }
            else if (Main.expertMode && Main.masterMode)
            {
                MaxCounter[0] = (int)Utils1.FormatTimeToTick(0, 0, 0, 10);
                MaxCounter[1] = (int)Utils1.FormatTimeToTick(0, 0, 0, 8);
            }
            for (int i = 0; i < AttackCounter.Length; i++)
            {
                AttackCounter[i] = (MaxCounter[0] / AttackCounter.Length) * i;
            }
            for (int l = 0; l < SummonCounter.Length; l++)
            {
                SummonCounter[l] = (MaxCounter[1] / SummonCounter.Length) * l;
            }

            List<int[]> ListCounter = new List<int[]>()
            {
                MaxCounter,
                AttackCounter,
                SummonCounter
            };
            return ListCounter;
        }
        public void UpdateCounters(List<int[]> ListCounter)
        {
            if (summonCounter > 0) summonCounter--;
            if (attackCounter > 0) attackCounter--;

            if (summonCounter <= 0)
            {
                var a = ListCounter[0][1];
                summonCounter = ListCounter[0][1];
                NPC.netUpdate = true;
            }

            if (attackCounter <= 0)
            {
                attackCounter = ListCounter[0][0];
                NPC.netUpdate = true;
            }
        }
        int blockIncrement = 0;
        public Vector2 GetSecurePosition(Vector2 pos)
        {
            Vector2 newPos;

            if (!CoordHasTile(pos) && !CoordHasLiquid(pos))
            {
                return pos;
            }
            else
            {
                do
                {
                    blockIncrement++;
                    newPos = new(pos.X, pos.Y - blockIncrement * 16);
                } while (CoordHasTile(newPos) && CoordHasLiquid(newPos));

                blockIncrement = 0;
                return newPos;
            }
        }
        public bool CoordHasTile(Vector2 pos) => Collision.SolidCollision(pos, 6 * 16, 6 * 16);
        public bool CoordHasLiquid(Vector2 pos)
        {
            if (Collision.LavaCollision(pos, 6 * 16, 6 * 16) || Collision.WetCollision(pos, 6 * 16, 6 * 16))
            {
                return true;
            }
            else if (Main.tile[(new Point((int)pos.X / 16, (int)(pos.Y - 5 * 16)/16))].LiquidAmount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void setCurrentPhase(NPC NPC)
        {
            if (NPC.life <= NPC.lifeMax / 4) currentPhase = 3;
            else if (NPC.life <= NPC.lifeMax / 2) currentPhase = 2;
            else currentPhase = 1;
        }
        public int AttackCounterScale(int Num, Player player) 
        {
            return (!DificultyUtils.ReaperMode) ? Num : Num - 100;
        }
        public void DesertTp()
        {
            tpDirection = Main.rand.Next(1, 100);

            int dir = tpDirection <= 50 ? -1 : 1;

            tpParticleTimer = (int)Utils1.FormatTimeToTick(0, 0, 0, 5);
            NPC.Center = GetSecurePosition(Main.player[NPC.target].Center + new Vector2(dir * 30 * 16, -5 * 16));
            GenerateTpParticles();
            tpParticleTimer = (int)Utils1.FormatTimeToTick(0, 0, 0, 5);
        }
        public void GenerateTpParticles()
        {
            do
            {
                tpParticleTimer--;
                for (int i = 0; i < 25; i++)
                {
                    Vector2 dustPosition = NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
                    Dust dust = Dust.NewDustDirect(dustPosition, NPC.width, NPC.height, DustID.Sand,0,0,100,default,3f);
                    dust.velocity = NPC.velocity * 0.2f; 
                    dust.noGravity = true; 
                }
            } while (tpParticleTimer > 0);

        }
        private float LifeSize(NPC npc)
        {
            float porcentage = Utils1.GetPorcentage(npc.life, npc.lifeMax);
            if (DificultyUtils.MasochistMode) return applyLifeSize(porcentage, 4f);
            else if (DificultyUtils.EternityMode) return applyLifeSize(porcentage, 2.5f);
            else if (DificultyUtils.InfernumMode) return applyLifeSize(porcentage, 2.3f);
            else if (DificultyUtils.Death) return applyLifeSize(porcentage, 2f);
            else if (DificultyUtils.Revengeance || DificultyUtils.ReaperMode) return applyLifeSize(porcentage, 1.5f);
            else if (Main.masterMode) return applyLifeSize(porcentage, 1.35f);
            else if (Main.expertMode) return applyLifeSize(porcentage, 1.3f);
            else return applyLifeSize(porcentage, 1.25f);
        }
        private float applyLifeSize(float porcentage, float MaxValue)
        {
           
            for (int i = 100; i >= 0; i--)
            {
                if ((int)porcentage == i)
                {
                    float j = Utils1.GetValueFromPorcentage(MaxValue, i);
                    if(j > Utils1.GetValueFromPorcentage(MaxValue, 30))
                    {
                        return j;
                    }
                }

            }
            return Utils1.GetValueFromPorcentage(MaxValue, 30);
        }
        
        public void ShootHelper(int dammage, int type, Player player, float Speed, double x, double y,bool proj)
        {
            Vector2 NpcPosition = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
            float rotation = (float)Math.Atan2(NpcPosition.Y - (player.position.Y + (player.height * x)), NpcPosition.X - (player.position.X + (player.width * y)));
            Vector2 direction;
            direction.X = (float)(Math.Cos(rotation) * Speed * -1);
            direction.Y = (float)(Math.Sin(rotation) * Speed * -1);
            if (proj)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NpcPosition, direction, type, dammage, 0f, Main.myPlayer);
            }
            else
            {
                var n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NpcPosition.X, (int)NpcPosition.Y, type);
                Main.npc[n].lifeMax = 10;
                Main.npc[n].damage = dammage;
                Main.npc[n].velocity = direction;
            }
        }
        public void DespawnBoss()
        {
            NPC.velocity.X -= 3.09f;
            NPC.velocity.Y -= 3f;

            NPC.EncourageDespawn(7);
            return;
        }
        
        public override bool? CanFallThroughPlatforms() => false;

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.rand.NextBool(3)) target.AddBuff(BuffType<Burning_Sand>(), 100, true);	
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY +(3 *16)), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f),NPC.scale,SpriteEffects.None,0);
            return false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            ScreenAnimationTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);
            spawnGuardians = true;
            base.OnSpawn(source);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            RemnantDownedBossSystem.downedDesert = true;
            potionType = ItemID.HealingPotion;  
            Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SandBlock, 60);
            Item.NewItem(NPC.GetSource_Loot(),(int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<Sand_escense>(), 10);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.NormalvsExpertOneFromOptions(1, 999999999, new[]
            {
                ItemType<desertbow>(),
                ItemType<DesertEdge>(),
                ItemType<DesertStaff>(),
                ItemType<DesertTome>()
            }));
            npcLoot.Add(ItemDropRule.Common(ItemType<Sand_escense>(), 1,5,20));
            npcLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 1,1,50));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.Amber, 6, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.IsHardModeRule(), ItemID.AncientBattleArmorMaterial, 5, 1,1, Utils1.ReaperDropScaler(1)));

            npcLoot.Add(ItemDropRule.Common(ItemType<DesertAMask>(), 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<DesertTrophy>(), 10));

            npcLoot.Add(ItemDropRule.BossBag(ItemType<desertBag>()));
            if (RemnantOfTheAncientsMod.InfernumMod != null) npcLoot.Add(RemnantDropRules.InfernumModeCommonDrop(ItemType<Desert_Relic>()));
            else npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Desert_Relic>()));
           
        }	
    }
}