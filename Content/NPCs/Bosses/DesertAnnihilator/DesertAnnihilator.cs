using CalamityMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlayerProxyLib.Common;
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
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Desert;
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

        private int attackCounter;
        /*private int tornadoCounter;
        private int summonCounter;*/
        private List<int> markProjectileIndices = [];
        private int CurrentPhase {
            get
            {
                if (NPC.life <= NPC.lifeMax / 4) return 3;
                if (NPC.life <= NPC.lifeMax / 2) return 2;
                return 1;
            }
        }
        private bool _BossIsInRage = false;
        private bool BossIsInRage
        {
            get => _BossIsInRage;
            set
            {
                _BossIsInRage = value;
                if (value) SoundEngine.PlaySound(SoundID.Roar);
            }
        }
        private bool BossActive => !CurrentTarget.dead && CurrentTarget.active;

        Point NpcFloor => Utils.ToTileCoordinates(NPC.Center);
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }
        Player CurrentTarget => Main.player[NPC.target];

        public float ScreenAnimationTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);
        public bool NoAI = DificultyUtils.InfernumMode;

        public override void AI()
        {
            NPC.frame.Y = 0;
            NPC.TargetClosest(true);
            if (DificultyUtils.InfernumMode && NoAI)
            {
                if (ScreenAnimationTimer <= 0)
                {
                    NoAI = false;
                }
                else
                {
                    NPC.velocity = Vector2.Zero;
                    ScreenAnimationTimer--;
                }
            }

            if (CurrentTarget == null || NoAI) return;
            CheckRage();
            UpdateScale();
            CheckForCheating();
            MovementAI();

            if (Main.netMode != NetmodeID.MultiplayerClient)
                AttackIA(CurrentTarget);


            if (CurrentTarget.dead)
            {
                NPC.EncourageDespawn(7);
                DespawnBoss();
            }

            if (RemnantOfTheAncientsMod.FargosSoulMod != null) EternityIA(CurrentTarget);

        }

        void CheckRage()
        {
            bool rage = BossActive && !CurrentTarget.ZoneDesert && !CurrentTarget.ZoneUndergroundDesert;
            if (BossIsInRage != rage) BossIsInRage = rage;
        }

        #region Anticheat

        private readonly int ForceTpDistance = 150.ToCoordinatePosition(); // 110 tiles in pixels
        void CheckForCheating()
        {
            if (CurrentTarget == null || !CurrentTarget.active || CurrentTarget.dead) return;

            float distanceToTarget = NPC.Distance(CurrentTarget.Center);
            if (distanceToTarget >= ForceTpDistance && !Reaper.ReaperMode)
            {
                GenerateTpParticles();
                NPC.velocity = Vector2.Zero;
                DesertTp();
            }

            if (Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidAmount > 0)
            {
                Point PlayerFloor = Utils.ToTileCoordinates(CurrentTarget.Center);
                if (Main.tile[PlayerFloor.X, PlayerFloor.Y + 1].LiquidAmount == 0)
                {
                    GenerateTpParticles();
                    DesertTp();
                }
            }
        }
        #endregion


        #region Movement
        float inertia => 1.3f;//0.95f;
        public void MovementAI()
        {
            ResetNPCFrameAndRotation();

            // Handle behavior when NPC is wet
            if (NPC.wet) HandleWetBehavior();

            // Reset aiAction and set ai[2] if ai[2] is 0f
            InitializeTargeting();

            // Handle movement when NPC velocity.Y is 0f
            if (NPC.velocity.Y == 0f)
            {
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

                if (Math.Abs(NPC.velocity.X) < 0.1) NPC.velocity.X = 0f;

                int jumpType = Main.rand.NextBool(4) ? 3 : 2;
                HandlejumpTypeBehavior(jumpType);
            }
            else if (NPC.target < 255 && ((NPC.direction == 1 && NPC.velocity.X < 3f) || (NPC.direction == -1 && NPC.velocity.X > -3f)))
            {
                HandleXMovement();
            }
        }


        // Handles resetting NPC frame and rotation
        private void ResetNPCFrameAndRotation()
        {
            NPC.frame.Y = 0;
            NPC.frameCounter = 0.0;
            NPC.rotation = 0f;
        }

        // Handles behavior when NPC is wet
        private void HandleWetBehavior()
        {
            if (NPC.collideY) NPC.velocity.Y = -2f;

            if (NPC.velocity.Y < 0f && NPC.ai[3] == NPC.position.X)
            {
                NPC.direction *= -1;
                NPC.ai[2] = 200f;
            }
            if (NPC.velocity.Y > 0f) NPC.ai[3] = NPC.position.X;
            if (NPC.velocity.Y > 2f) NPC.velocity.Y *= 0.9f;

            NPC.velocity.Y -= 0.5f;
            if (NPC.velocity.Y < -4f) NPC.velocity.Y = -4f;
            if (NPC.ai[2] == 1f) NPC.TargetClosest();

        }

        // Handles resetting aiAction and setting ai[0] and ai[2]
        private void InitializeTargeting()
        {
            NPC.aiAction = 0;
            if (NPC.ai[2] == 0f)
            {
                NPC.ai[2] = 1f;
                NPC.TargetClosest();
            }
        }

        // Handles behavior based on the value of num34

        private void HandlejumpTypeBehavior(int jumpType)
        {
            NPC.netUpdate = true;

            if (NPC.ai[2] == 1f) NPC.TargetClosest();

            if (jumpType == 3)
            {
                NPC.velocity.Y = -8f;
                NPC.velocity.X += 3 * NPC.direction;
                NPC.ai[3] = NPC.position.X;
            }
            else
            {
                NPC.velocity.Y = -6f;
                NPC.velocity.X += 2 * NPC.direction;

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
        #region Attacks
        private void AttackIA(Player target)
        {
            List<int[]> AttackValue = SetAttackCounter();
            UpdateCounters(AttackValue);

            int timeReduced = 0;

            if (Reaper.ReaperMode) timeReduced += Utils1.FormatTimeToTick(0, 0, 0, 2);
            if (BossIsInRage || InfernumMode) timeReduced += Utils1.FormatTimeToTick(0, 0, 0, 4);

            for (int i = 1; i <= AttackValue.Count - 1; i++)
            {
                for (int j = 0; j < AttackValue[i].Length; j++)
                {
                    AttackValue[i][j] -= timeReduced;
                }
            }

            TornadoAI();

            if (Main.expertMode)
            {
                ShootAI(target);
            }

            if (attackCounter == 500) DesertTp();

            if (MathUtils.NumberBetween(501, 560, attackCounter)) GenerateTpParticles();
        }

        public int shootTimeline = -1;
        int shootTimelineMax = Utils1.FormatTimeToTick(Second: 50);
        public void ShootAI(Player target)
        {
            shootTimelineMax = Utils1.FormatTimeToTick(Second: 10);
            if (shootTimeline++ >= shootTimelineMax) shootTimeline = 0;

            ShootData data = new ShootData()
            {
                type = Reaper.ReaperMode ? NPCType<DesertTyphoonParry>() : ProjectileType<DesertTyphoon>(),
                projectileType = Reaper.ReaperMode ? ShootData.ProjectileType.NPC : ShootData.ProjectileType.Projectile
            };

            if (isShootTimeLinePercent(10))
            {
                ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), data, target, 12f, rotationGrades: Main.rand.Next(-20, 20));
            }
            else if (isShootTimeLinePercent(50))
            {
                for (int i = -1; i <= 1; i++)
                {
                    ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), data, target, 12f, rotationGrades: i * 60 + Main.rand.Next(-20, 20));
                }
            }
            else if ((isShootTimeLinePercent(80) || isShootTimeLinePercent(85) || isShootTimeLinePercent(90)) && BossIsInRage)
            {
                for (int i = -2; i <= 2; i++)
                {
                    ShootHelper((int)(20 * RemnantGlobalNPC.DamageBonus), data, target, 12f, rotationGrades: i * 60 + Main.rand.Next(-30, 30));
                }
            }
        }

        private bool isShootTimeLinePercent(int value)
        {
            return shootTimeline == MathUtils.GetValueFromPorcentage(shootTimelineMax, value);
        }
        private List<int> lightSpawn =
        [
            NPCID.Antlion,
            NPCID.FlyingAntlion,
            NPCID.WalkingAntlion,
            NPCID.TombCrawlerHead,
        ];

        private List<int> heavySpawn =
        [
            NPCID.Antlion,
            NPCID.GiantFlyingAntlion,
            NPCID.GiantWalkingAntlion,
            NPCID.DuneSplicerHead,
        ];

        int tornadoInterval = Utils1.FormatTimeToTick(Second: 8);
        int markDelay 
        {
            get{
                if (DificultyUtils.EternityMode || DificultyUtils.MasochistMode) return 1;
                if (DificultyUtils.InfernumMode || DificultyUtils.LegendaryMode) return 1;
                if (DificultyUtils.ReaperMode || DificultyUtils.Revengeance || DificultyUtils.Death) return 2;
                return 3;
            }
        }
        int markDelayTicks => Utils1.FormatTimeToTick(Second: markDelay);
        int tornadoCounter2 = Utils1.FormatTimeToTick(Second: 8);
        int spawnIndex = 0;
        List<(Vector2 Position, int Timer)> pendingTornados = [];

        public void TornadoAI()
        {
            tornadoCounter2--;

            if (tornadoCounter2 <= 0)
            {
                tornadoCounter2 = tornadoInterval;

                int numberOfTornados = DificultyUtils.ReaperMode ? 3 : 1;

                for (int i = 0; i < numberOfTornados; i++)
                {
                    float randomX = Main.rand.NextBool()
                        ? Main.rand.Next(-50, -20).ToCoordinatePosition()
                        : Main.rand.Next(20, 50).ToCoordinatePosition();

                    float randomY = NPC.Bottom.Y + Main.rand.Next(-3, 3).ToCoordinatePosition();

                    Vector2 spawnPosition = new(CurrentTarget.Center.X + randomX,randomY);

                    Projectile.NewProjectile(Projectile.GetSource_None(), spawnPosition, Vector2.Zero,ModContent.ProjectileType<SandnadoMarkClone>(),0,0,Main.myPlayer,ai0: markDelay);

                    pendingTornados.Add((spawnPosition, markDelayTicks));      
                }

                foreach (Player player in Main.player)
                {
                    if (!player.active || player.dead || player.ghost || player.IsProxyPlayer()) continue;
                    Vector2 pos = player.position;
                    Projectile.NewProjectile(Projectile.GetSource_None(), pos, Vector2.Zero, ModContent.ProjectileType<SandnadoMarkClone>(), 0, 0, Main.myPlayer, ai0: markDelay);

                    pendingTornados.Add((pos, markDelayTicks));
                }
            }

            // Actualizar cada tornado pendiente
            for (int i = pendingTornados.Count - 1; i >= 0; i--)
            {
                var tornado = pendingTornados[i];
                tornado.Timer--;

                int enemyId = Reaper.ReaperMode || Main.masterMode ? heavySpawn[spawnIndex] : lightSpawn[spawnIndex];
                if (tornado.Timer <= 0)
                {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)tornado.Position.X, (int)tornado.Position.Y - 16, enemyId);
                    
                    pendingTornados.RemoveAt(i);
                }
                else
                {
                    pendingTornados[i] = tornado;
                }
            }
            if (spawnIndex++ >= lightSpawn.Count - 1) spawnIndex = 0;
        }

        public int GetFinalStagePorcentage()
        {
            if (DificultyUtils.MasochistMode) return 15;
            if (DificultyUtils.EternityMode || DificultyUtils.InfernumMode) return 10;
            return 5;
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public void EternityIA(Player player)
        {
            if (!DificultyUtils.MasochistMode && !DificultyUtils.EternityMode) return;
            
            if (attackCounter == Utils1.FormatTimeToTick(0, 0, 0, 7))
            {
                NPC.netUpdate = true;

                if (Main.netMode != NetmodeID.MultiplayerClient)  
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "GlowRing"), 0, 0f, Main.myPlayer, NPC.whoAmI, -19);

                if (NPC.HasValidTarget)  
                    SoundEngine.PlaySound(in SoundID.ForceRoarPitched, Main.player[NPC.target].Center);
                    
            }
            if (attackCounter == Utils1.FormatTimeToTick(0, 0, 0, 7) - 10)
            {
                float ofset = 100f.ToCoordinatePosition();
                float ofsetY = player.position.Y - ofset;

                Vector2 start = new(player.position.X - ofset, ofsetY);
                Vector2 end = new(player.position.X + ofset, ofsetY);

                int numberOfProjectiles = DificultyUtils.EternityMode ? 40 : 80;

                List<Vector2> points = GeneratePoints(start, end, numberOfProjectiles);

                int damage = 100;
                int proj = RemnantOfTheAncientsMod.ParticleMeterChoice() ? ProjectileID.RollingCactus : ProjectileType<CactusBoulderClone>();

                foreach (var point in points)
                {
                    var p = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(point.X, point.Y + (100f * 16)), Vector2.Zero, CallUtils.TryGetProjectileFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "WOFReticle"), 0, 0f, Main.myPlayer);
                    Main.projectile[p].scale = 0.5f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), point, Vector2.Zero,proj, damage, 0f, Main.myPlayer);
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
        public bool spawnGuardians = true;
        public override void HitEffect(NPC.HitInfo hit)
        {
            SpawnAddsOnHit();

            if (NPC.life > 0) return;
            
            if (Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("DesertAniquilatorGore").Type, NPC.scale);
            }
            for (int j = 0; j < RemnantOfTheAncientsMod.ParticleMeter(1000); j++)
            {
                Dust.NewDust(NPC.position, (int)(NPC.width * NPC.scale), (int)(NPC.height * NPC.scale), DustID.Sandstorm, hit.HitDirection, -1f);
            }       
        }
        private void SpawnAddsOnHit()
        {
            if (InfernumMode) InfernumHitEffect();
            
            int choice = Main.rand.Next(2, 8);
            if (Reaper.ReaperMode || BossIsInRage) choice *= 2;
            
            if (!Main.rand.NextBool(3)) return;

            for (int i = 0; i < choice; i++)
            {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCType<DesertAnnihilatorServant>());
            }  
        }
        #endregion

        private void InfernumHitEffect()
        {
            if (!spawnGuardians) return;
            int offset = 10;
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X + offset, (int)NPC.position.Y, NPCType<DesertAnnihilatorGuard>());
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X - offset, (int)NPC.position.Y, NPCType<DesertAnnihilatorGuard>());
            spawnGuardians = false;
        }
        enum TimmerType { MaxTimmer, Attack, Summon }

        public List<int[]> SetAttackCounter()
        {
            int[] MaxCounter = [0, 0];
            int[] AttackCounter = [0, 0, 0, 0, 0, 0];
            //int[] SummonCounter = [0, 0, 0, 0, 0];

            MaxCounter[(int)TimmerType.Attack-1] = GetTimerDificultyValue(normal: 15, expert: 13, master: 10); // 0
            MaxCounter[(int)TimmerType.Summon-1] = GetTimerDificultyValue(normal: 14, expert: 13, master: 8); // 1

            DistribuirTimerEquivalente(AttackCounter, MaxCounter[(int)TimmerType.Attack-1]);
            //DistribuirTimerEquivalente(SummonCounter, MaxCounter[(int)TimmerType.Summon-1]);

            List<int[]> ListCounterMax =
            [
                MaxCounter,
                AttackCounter,
                //SummonCounter
            ];
            return ListCounterMax;
        }
        private void DistribuirTimerEquivalente(int[] timer,int timerMax)
        {
            int interval = timerMax / timer.Length;
            for (int i = 0;  i < timer.Length; i++) 
                timer[i] = interval * i;     
        }
        private static int GetTimerDificultyValue(int normal, int expert, int master)
        {
            normal = Utils1.FormatTimeToTick(0, 0, 0, normal);
            expert = Utils1.FormatTimeToTick(0, 0, 0, expert);
            master = Utils1.FormatTimeToTick(0, 0, 0, master);
            if (Main.expertMode && !Main.masterMode) return expert;
            if (Main.expertMode && Main.masterMode) return master;
            return normal;
        }
        public void UpdateCounters(List<int[]> ListCounterMax)
        {
            if (attackCounter-- <= 0)
            {
                attackCounter = ListCounterMax[(int)TimmerType.MaxTimmer][(int)TimmerType.Attack-1];
            }
            NPC.netUpdate = true;
        }
        public static Vector2 GetSecurePosition(Vector2 position)
        {
            Vector2 newPos;
            int blockIncrement = 0;

            position = new (Math.Abs(position.X), Math.Abs(position.Y));

            if (!CoordHasTile(position) && !CoordHasLiquid(position)) return position;

            do
            {
                float positionY = position.Y - (blockIncrement++).ToCoordinatePosition();
                newPos = new(position.X, positionY);
            } while (CoordHasTile(newPos) || CoordHasLiquid(newPos));

            if (newPos.Y < 0) newPos.Y *= -1;
            return newPos;   
        }
        public static bool CoordHasTile(Vector2 pos) => Collision.SolidCollision(pos, 6 * 16, 6 * 16);
        public static bool CoordHasLiquid(Vector2 pos)
        {
            if (Collision.LavaCollision(pos, 6 * 16, 6 * 16) || Collision.WetCollision(pos, 6 * 16, 6 * 16)) return true;
            if (Main.tile[(new Point((int)pos.X / 16, (int)(pos.Y - 5 * 16) / 16))].LiquidAmount > 0) return true;
            return false;
        }
        public void DesertTp()
        {
            NPC.alpha = 0;
            int tpDirection = Main.rand.NextBool() ? -1 : 1;
            Vector2 tileDistance = new(30f, -5f); 
            Vector2 tpDistance = new Vector2(tileDistance.X,tileDistance.Y).ToCoordenatePosition();
            NPC.Center = GetSecurePosition(Main.player[NPC.target].Center + new Vector2(tpDirection * tpDistance.X, tpDistance.Y));
            GenerateTpParticles(appear: true);
        }

        public void GenerateTpParticles(bool appear = false)
        {
            int particleCount = RemnantOfTheAncientsMod.ParticleMeter(45);//25

            int width = (int)(NPC.width * NPC.scale);
            int height = (int)(NPC.height * NPC.scale);
            if (!appear) NPC.alpha = 150;
          
                
            for (int i = 0; i < particleCount; i++)
            {
                Vector2 dustPosition = NPC.position - new Vector2(Main.rand.Next(width/2), Main.rand.Next(height/2));
                Dust dust = Dust.NewDustDirect(dustPosition, width, height, DustID.Sand, 0, 0, 100, default, 3f);
                dust.velocity = NPC.velocity * 0.2f;
                dust.noGravity = true;
            }
            
        }
        private void UpdateScale()
        {
            NPC.scale = LifeSize(NPC);
            int oldCenterX = (int)NPC.Center.X;
            int oldCenterY = (int)NPC.Center.Y;

            NPC.width = (int)(originalSize.X * NPC.scale);
            NPC.height = (int)(originalSize.Y * NPC.scale);

            NPC.Center = new Vector2(oldCenterX, oldCenterY);
        }

        private float LifeSize(NPC npc)
        {
            float percentage = MathUtils.GetPorcentage(npc.life, npc.lifeMax);
            float maxValue = 1.25f;

            if (DificultyUtils.MasochistMode) maxValue = 4f;
            else if (DificultyUtils.EternityMode) maxValue = 2.5f;
            else if(DificultyUtils.InfernumMode) maxValue = 2.3f;
            else if(DificultyUtils.Death) maxValue = 2f;
            else if(DificultyUtils.Revengeance || Reaper.ReaperMode) maxValue = 1.5f;
            else if(Main.masterMode) maxValue = 1.35f;
            else if(Main.expertMode) maxValue = 1.3f;
            return ApplyLifeSize(percentage, maxValue);
        }

        private float ApplyLifeSize(float percentage, float maxValue)
        {
            float valorMinimo = MathUtils.GetValueFromPorcentage(maxValue, 30);

            if (percentage < 0 || percentage > 100) return valorMinimo;

            float valorActual = MathUtils.GetValueFromPorcentage(maxValue, percentage);
            return Math.Max(valorActual, valorMinimo);
        }
        public void ShootHelper(int dammage, ShootData shoot, Player player, float Speed, float rotationGrades = 0f)
        {

            Vector2 direction = player.Center - NPC.Center;
            direction.Normalize();
            direction = direction.RotatedBy(MathHelper.ToRadians(rotationGrades));
            direction *= Speed;
            

            if (shoot.projectileType == ShootData.ProjectileType.Projectile)
            {      
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, direction, shoot.type, dammage, 0f, Main.myPlayer);
            }
            else if(shoot.projectileType == ShootData.ProjectileType.NPC) 
            {
                var n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, shoot.type);
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
            if (NPC.IsABestiaryIconDummy) return true;

            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Color color = NPC.GetAlpha(drawColor);
            Vector2 position = NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY + (3 * 16) * NPC.scale);
            Main.EntitySpriteDraw(Texture, position, NPC.frame, color, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return false;
        }

        Vector2 originalSize = new();
        public override void OnSpawn(IEntitySource source)
        {
            ScreenAnimationTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);
            originalSize.X = NPC.width;
            originalSize.Y = NPC.height;
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

    public class ShootData
    {
        public int type { get; set; }
        public ProjectileType projectileType { get; set; }

        public enum ProjectileType
        {
            Projectile = 0,
            NPC = 1,
        }
    }
}