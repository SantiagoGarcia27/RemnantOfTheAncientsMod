using CalamityMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile.Desert;
using RemnantOfTheAncientsMod.World;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.DesertAnnihilator
{
    [AutoloadBossHead]
    public class DesertAnnihilator : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f,
                Frame = 0,
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 3500;
            NPC.damage = 30;
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
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefaultsCalamity();
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefaultsCalamity()
        {
            NPC.Calamity().canBreakPlayerDefense = true;

            RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC, 0.02f, 0.22f, 0.3f, 0.4f, 0.4f);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
            ]);
        }
        public bool InfernumMode = DificultyUtils.InfernumMode;
        enum Attacks {none, TpShoot, Smash}
        Attacks currenAttack = Attacks.none;

        private int attackCounter;
        private int tornadoCounter;
        private int summonCounter;
        private List<int> markProjectileIndices = [];
        private int tpDirection;
        private int currentPhase;
        private bool BossIsInRage = false;
        Point NpcFloor;
        int tpParticleTimer = (int)Utils1.FormatTimeToTick(0, 0, 0, 5);
        int ShootTpTimmer = 0;
        int ShootTpDir = 1;
        int ShootTpAttackCounter = 0;
        int SmashTimmer = 0;
        int SmashCounter = 0;
        int blockIncrement = 0;
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
        Player currentTarget = null;

        public float ScreenAnimationTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);
        public bool NoAI = DificultyUtils.InfernumMode;
        public override void AI()
        {
            NPC.frame.Y = 0;
            NPC.TargetClosest(true);
            currentTarget = Main.player[NPC.target];

            if (DificultyUtils.InfernumMode)
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

            if (currentTarget != null)
            {
                Point PlayerFloor = Utils.ToTileCoordinates(currentTarget.Center);
                if (!NoAI)
                {
                    NPC.ai[0] = 10;
                    NPC.ai[1] = (NPC.ai[1] + 1) % 800;
                    BossIsInRage = CheckRage(currentTarget);
                    NPC.scale = LifeSize(NPC);
                    float distance = NPC.Distance(currentTarget.Center);
                    if (distance >= 110 * 16 && !currentTarget.dead && !Reaper.ReaperMode)
                    {
                        GenerateTpParticles();
                        DesertTp();
                    }
                    MovementAI();
                    NpcFloor = Utils.ToTileCoordinates(NPC.Center);
                    
                    if (Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidAmount > 0)
                    {
                        if (Main.tile[PlayerFloor.X, PlayerFloor.Y + 1].LiquidAmount == 0)
                        {
                            GenerateTpParticles();
                            DesertTp();
                        }
                    }
                    if (currentTarget.dead || NPC.target < 0 || NPC.target == 255 || !currentTarget.active)
                    {
                        NPC.TargetClosest(true);
                    }
                    SetCurrentPhase(NPC);
                    AttackIA(currentTarget);


                    if (currentTarget.dead)
                    {
                        NPC.EncourageDespawn(7);
                        DespawnBoss();
                    }


                    if (RemnantOfTheAncientsMod.FargosSoulMod != null)
                    {
                        EternityIA(currentTarget);
                    }
                }
            }
        }
        #region Movement
        float inertia = 0.95f;
        public void MovementAI()
        {
            ResetNPCFrameAndRotation();
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
                NPC.velocity.X *= inertia;

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

                if (NPC.ai[0] >= intervalo * 0.5f)      // >= -500
                    num34 = 2;
                else if (NPC.ai[0] >= intervalo)          // >= -1000
                    num34 = 1;
                else if (NPC.ai[0] >= intervalo * 2f)     // >= -2000
                    num34 = 3;

                if (num34 > 0 && num34 != 3 && Main.rand.NextBool(4))
                    num34 = 3;

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
            return true;
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

            float acceleration = 0.6f * Main.player[NPC.target].maxRunSpeed;
            float deceleration = 0.97f;

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
        public bool CheckRage(Player player) => !player.dead && player.active && !player.ZoneDesert && !player.ZoneUndergroundDesert;
        private void AttackIA(Player target)
        {
            List<int[]> AttackValue = SetAttackCounter();
            UpdateCounters(AttackValue);

            if (Reaper.ReaperMode)
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
                TornadoAI();
            }
            if (attackCounter == 500)
            {
                DesertTp();
            }
            if (MathUtils.NumberBetween(500, 560, attackCounter))
            {
                GenerateTpParticles();
            }
        }
        public void ShootAI(List<int[]> AttackValue, Player target)
        {
            int type = Reaper.ReaperMode ? NPCType<DesertTyphoonParry>() : ProjectileType<DesertTyphoon>();
            bool proj = !Reaper.ReaperMode;

            for (int i = 0; i < 4; i++)
            {
                if (attackCounter == AttackValue[1][i])
                {
                    int damage = (int)(20 * RemnantGlobalNPC.DamageBonus);
                    ShootHelper(damage, type, target, 12f, 0.5f * Math.Sign(Main.rand.Next(-4, 4)), 0.5f * Math.Sign(Main.rand.Next(-4, 4)), proj);
                }
            }
            if (attackCounter == AttackValue[1][5])
            {
                if (BossIsInRage || InfernumMode)
                {
                    for (int i = 0; i <= 7; i++)
                    {
                        ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), type, target, 12f + i, -0.5f + i, 0.5f, proj);
                    }
                }
                else
                {
                    for (float i = 0f; i < (Reaper.ReaperMode ? 2 : 0); i += 0.5f)
                    {
                        ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), type, target, 12f, -3.5f + i, 3.5f - i, proj);
                    }
                }
            }
        }
        public void ShootTp(int tpNumber, float secondsDelay = 0.5f)
        {

            if (ShootTpAttackCounter > tpNumber)
            {
                ShootTpAttackCounter = 0;
                currenAttack = Attacks.none;
            }
            if (ShootTpTimmer % (int)(secondsDelay * 60) == 0)
            {
                NPC.Center = currentTarget.Center + new Vector2(-600 * ShootTpDir, 0);
                Vector2 velocity = currentTarget.Center - NPC.Center;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(velocity.X, velocity.Y) /4, ProjectileType<DesertTyphoon>(), 30, 0, Main.myPlayer);
                ShootTpDir *= -1;
                ShootTpTimmer = 0;
                ShootTpAttackCounter++;
            }
            ShootTpTimmer++;
        }

        public void SmashAttack(int tpNumber, float secondsDelay = 1.5f)
        {
            if (SmashCounter > tpNumber)
            {
                SmashCounter = 0;
                currenAttack = Attacks.TpShoot;
                NPC.GravityMultiplier = MultipliableFloat.One;
            }
            if (SmashTimmer % (secondsDelay * 60) == 0 && !DistanceUtils.TouchFlour(NPC))
            {   
                NPC.velocity = new Vector2(0, 40);
                SmashTimmer = 0;
                SmashCounter++;
                NPC.GravityMultiplier = MultipliableFloat.One;
            }
            else
            {
                NPC.Center = currentTarget.Center + new Vector2(0, -600);
                NPC.velocity = Vector2.Zero;
                NPC.GravityMultiplier *= 0;
            }
                SmashTimmer++;
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
                int a = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.FlyingAntlion);
                Main.npc[a].lifeMax /= 2;
                Main.npc[a].damage *= 2;
                Main.npc[a].value = 0;
                a = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.WalkingAntlion);
                Main.npc[a].lifeMax /= 2;
                Main.npc[a].damage *= 2;
                Main.npc[a].value = 0;
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
        public void TornadoAI()
        {
            if (tornadoCounter == 0 && NPC.life > MathUtils.GetValueFromPorcentage(NPC.lifeMax, SetFinalStagePorcentage()))
            {
                tornadoCounter = (int)Utils1.FormatTimeToTick(0, 0, 0, 5);
            }
            else if (tornadoCounter > 0)
            {
                tornadoCounter--;
            }

            if (tornadoCounter == (int)Utils1.FormatTimeToTick(0, 0, 0, 4))
            {
                markProjectileIndices.Clear();
                if (!Reaper.ReaperMode)
                {
                    int idx = Projectile.NewProjectile(Projectile.GetSource_None(), Main.player[NPC.target].position, Vector2.Zero, ProjectileID.SandnadoHostileMark, 0, 0, Main.myPlayer);
                    markProjectileIndices.Add(idx);
                }
                else
                {
                    float distanceBetweenTornados = 5f;
                    for (int a = 0; a < 7; a++)
                    {
                        int idx = Projectile.NewProjectile(Projectile.GetSource_None(), Main.player[NPC.target].position + new Vector2(a * 16 * distanceBetweenTornados * Main.player[NPC.target].direction, 0), Vector2.Zero, ProjectileID.SandnadoHostileMark, 0, 0, Main.myPlayer);
                        markProjectileIndices.Add(idx);
                    }
                }
            }

            if (tornadoCounter == (int)Utils1.FormatTimeToTick(0, 0, 0, 1) && markProjectileIndices.Count > 0)
            {
                int damage = (int)(30 * RemnantGlobalNPC.DamageBonus);
                int time = (int)Utils1.FormatTimeToTick(Second: 1);
                foreach (int idx in markProjectileIndices)
                {
                    if (idx >= 0 && idx < Main.maxProjectiles && Main.projectile[idx].active)
                    {
                        int sandnadoId = Projectile.NewProjectile(Projectile.GetSource_None(), Main.projectile[idx].position, Vector2.Zero, ProjectileID.SandnadoHostile, damage, 1, Main.myPlayer);
                        Main.projectile[sandnadoId].timeLeft = time;
                    }
                }
                markProjectileIndices.Clear();
            }
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public void EternityIA(Player player)
        {
            if (DificultyUtils.MasochistMode || DificultyUtils.EternityMode)
            {
                if (attackCounter == (int)Utils1.FormatTimeToTick(0, 0, 0, 7))
                {
                    NPC.netUpdate = true;
                    if (FargowiltasSouls.FargoSoulsUtil.HostCheck)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "GlowRing"), 0, 0f, Main.myPlayer, NPC.whoAmI, -19);
                    }
                    if (NPC.HasValidTarget)
                    {
                        SoundEngine.PlaySound(in SoundID.ForceRoarPitched, Main.player[NPC.target].Center);
                    }
                }
                if (attackCounter == (int)Utils1.FormatTimeToTick(0, 0, 0, 7) - 10)
                {

                    Vector2 start = new(player.position.X + (-100 * 16), (player.position.Y - 100 * 16));
                    Vector2 end = new(player.position.X + (100 * 16), (player.position.Y - 100 * 16));

                    int numberOfProjectiles = DificultyUtils.EternityMode ? 40 : 80;

                    List<Vector2> points = GeneratePoints(start, end, numberOfProjectiles);

                    foreach (var point in points)
                    {
                        var p = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(point.X, point.Y + (100f * 16)), Vector2.Zero, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "WOFReticle"), 0, 0f, Main.myPlayer);
                        Main.projectile[p].scale = 0.5f;
                        int proj = RemnantOfTheAncientsMod.ParticleMeterChoice() ? ProjectileID.RollingCactus : ModContent.ProjectileType<CactusBoulderClone>();
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), point, Vector2.Zero,proj, 100, 0f, Main.myPlayer);
                    }
                    static List<Vector2> GeneratePoints(Vector2 start, Vector2 end, int pointCount)
                    {
                        List<Vector2> points = [];

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
        public bool spawnGuardians = true;
        public override void HitEffect(NPC.HitInfo hit)
        {
            int choice = Main.rand.Next(2, 8);
            if (Reaper.ReaperMode || BossIsInRage) choice *= 2;
            if (Main.rand.NextBool(3))
            {
                for (int i = 0; i < choice; i++)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DesertAnnihilatorServant>());
                }
            }
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("DesertAniquilatorGore").Type, NPC.scale);
                }
                for (int j = 0; j < RemnantOfTheAncientsMod.ParticleMeter(1000); j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Sandstorm, hit.HitDirection, -1f);
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
        public List<int[]> SetAttackCounter()
        {
            int[] MaxCounter = [0, 0];
            int[] AttackCounter = [0, 0, 0, 0, 0, 0];
            int[] SummonCounter = [0, 0, 0, 0, 0];

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

            List<int[]> ListCounter =
            [
                MaxCounter,
                AttackCounter,
                SummonCounter
            ];
            return ListCounter;
        }
        public void UpdateCounters(List<int[]> ListCounter)
        {
            if (summonCounter > 0) summonCounter--;
            if (attackCounter > 0) attackCounter--;

            if (summonCounter <= 0)
            {
                summonCounter = ListCounter[0][1];
            }
            if (attackCounter <= 0)
            {
                attackCounter = ListCounter[0][0];
            }
            NPC.netUpdate = true;
        }
        public Vector2 GetSecurePosition(Vector2 pos)
        {
            Vector2 newPos;
            if (pos.Y < 0) pos.Y *= -1;
            if (pos.X < 0) pos.X *= -1;
            if (!CoordHasTile(pos) && !CoordHasLiquid(pos))
            {
                return pos;
            }
            else
            {
                do
                {
                    newPos = new(pos.X, pos.Y - blockIncrement++ * 16);
                } while (CoordHasTile(newPos) || CoordHasLiquid(newPos));

                blockIncrement = 0;
                if (newPos.Y < 0) newPos.Y *= -1;
                return newPos;
            }
        }
        public static bool CoordHasTile(Vector2 pos) => Collision.SolidCollision(pos, 6 * 16, 6 * 16);
        public static bool CoordHasLiquid(Vector2 pos)
        {
            if (Collision.LavaCollision(pos, 6 * 16, 6 * 16) || Collision.WetCollision(pos, 6 * 16, 6 * 16))
            {
                return true;
            }
            else if (Main.tile[(new Point((int)pos.X / 16, (int)(pos.Y - 5 * 16) / 16))].LiquidAmount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void SetCurrentPhase(NPC NPC)
        {
            if (NPC.life <= NPC.lifeMax / 4) currentPhase = 3;
            else if (NPC.life <= NPC.lifeMax / 2) currentPhase = 2;
            else currentPhase = 1;
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
                    Dust dust = Dust.NewDustDirect(dustPosition, NPC.width, NPC.height, DustID.Sand, 0, 0, 100, default, 3f);
                    dust.velocity = NPC.velocity * 0.2f;
                    dust.noGravity = true;
                }
            } while (tpParticleTimer > 0);

        }
        private float LifeSize(NPC npc)
        {
            float porcentage = MathUtils.GetPorcentage(npc.life, npc.lifeMax);
            if (DificultyUtils.MasochistMode) return ApplyLifeSize(porcentage, 4f);
            else if (DificultyUtils.EternityMode) return ApplyLifeSize(porcentage, 2.5f);
            else if (DificultyUtils.InfernumMode) return ApplyLifeSize(porcentage, 2.3f);
            else if (DificultyUtils.Death) return ApplyLifeSize(porcentage, 2f);
            else if (DificultyUtils.Revengeance || Reaper.ReaperMode) return ApplyLifeSize(porcentage, 1.5f);
            else if (Main.masterMode) return ApplyLifeSize(porcentage, 1.35f);
            else if (Main.expertMode) return ApplyLifeSize(porcentage, 1.3f);
            else return ApplyLifeSize(porcentage, 1.25f);
        }
        private float ApplyLifeSize(float porcentage, float MaxValue)
        {

            for (int i = 100; i >= 0; i--)
            {
                if ((int)porcentage == i)
                {
                    float j = MathUtils.GetValueFromPorcentage(MaxValue, i);
                    if (j > MathUtils.GetValueFromPorcentage(MaxValue, 30))
                    {
                        return j;
                    }
                }

            }
            return MathUtils.GetValueFromPorcentage(MaxValue, 30);
        }

        public void ShootHelper(int dammage, int type, Player player, float Speed, double x, double y, bool proj)
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
            if (NPC.IsABestiaryIconDummy)
                return true;

            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY + (3 * 16)), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
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
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SandBlock, 60);
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemType<Sand_escense>(), 10);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.NormalvsExpertOneFromOptions(1, 999999999,
            [
                ItemType<DesertBow>(),
                ItemType<DesertEdge>(),
                ItemType<DesertStaff>(),
                ItemType<DesertTome>()
            ]));
            npcLoot.Add(ItemDropRule.Common(ItemType<Sand_escense>(), 1, 5, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 1, 1, 50));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.Amber, 6, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.IsHardModeRule(), ItemID.AncientBattleArmorMaterial, 5, 1, 1, Utils1.ReaperDropScaler(1)));

            npcLoot.Add(ItemDropRule.Common(ItemType<DesertAMask>(), 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<DesertTrophy>(), 10));

            npcLoot.Add(ItemDropRule.BossBag(ItemType<desertBag>()));
            if (DificultyUtils.InfernumMode) npcLoot.Add(RemnantDropRules.InfernumModeCommonDrop(ItemType<Desert_Relic>()));
            else npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Desert_Relic>()));

        }
    }
}