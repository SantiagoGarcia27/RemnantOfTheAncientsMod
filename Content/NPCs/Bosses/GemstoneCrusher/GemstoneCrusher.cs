using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.Trower;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.GemstoneCrusher
{
    [AutoloadBossHead]
    public class GemstoneCrusher : ModNPC
    {
        private Player target;

        private enum BossState
        {
            Sleep,
            Idle,
            Stomp,
            Falling
        }

        private BossState _currentState = BossState.Sleep;

        private BossState CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState == value)
                    return;

                _currentState = value;
                if (Main.netMode != NetmodeID.MultiplayerClient) NPC.netUpdate = true;
            }


        }

        private int stompTimer = (int)Utils1.FormatTimeToTick(Second: 2);
        private readonly int stompActivationTimerTrigger = (int)Utils1.FormatTimeToTick(Second: 20);
        private readonly int shootTimerTrigger = (int)Utils1.FormatTimeToTick(Second: 5);
        private readonly int spawnTimerTrigger = (int)Utils1.FormatTimeToTick(Second: 15);

        private float shootTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;          
        }
        private float spawnerTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float stompActivationTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }


        private bool spawnWorms;

        private readonly List<int> gemProjectiles =
        [
            ModContent.ProjectileType<GemstoneCrusherProj_Sapphire>(),
            ModContent.ProjectileType<GemstoneCrusherProj_Emerald>(),
            ModContent.ProjectileType<GemstoneCrusherProj_Ruby>(),
            ModContent.ProjectileType<GemstoneCrusherProj_Diamond>()
        ];
        private readonly List<int> gemDust =
        [
            DustID.GemSapphire,
            DustID.GemEmerald,
            DustID.GemRuby,
            DustID.GemDiamond
        ];

        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f,
                Frame = 0,
                Velocity = 1f
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 500;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.width = 60;
            NPC.height = 60;
            NPC.value = Item.buyPrice(0, 1, 0, 0);
            NPC.npcSlots = 30f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.netAlways = true;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            base.SetDefaults();
        }

        int projectileType = -1;
        bool esServer = Main.netMode != NetmodeID.MultiplayerClient;
        public override void AI()
        {
            UpdateTarget();

            if (target == null)
                return;

            stompActivationTimer++;

            if (stompActivationTimer >= stompActivationTimerTrigger)
            {
                stompActivationTimer = 0;
                stompTimer = (int)Utils1.FormatTimeToTick(Second: 2);
                CurrentState = BossState.Stomp;
            }

            if (CurrentState == BossState.Stomp || CurrentState == BossState.Falling)
            {
                MovementAI();
            }
            else
            {
                shootTimer++;
                spawnerTimer++;

                if (shootTimer >= shootTimerTrigger - (int)Utils1.FormatTimeToTick(Second: 1))
                { 
                    ShootAI();
                }

                if (spawnerTimer >= spawnTimerTrigger)
                {
                    SpawnAI();
                    spawnerTimer = 0;
                }
            }

            base.AI();
        }

        private void UpdateTarget()
        {
            if (target != null && target.active && !target.dead)
                return;

            NPC.TargetClosest(true);
            target = Main.player[NPC.target];
        }

        private void MovementAI()
        {
            if (NPC.Bottom.Y >= target.Center.Y && CurrentState == BossState.Falling && NPC.noTileCollide)
            {
                NPC.noTileCollide = false;
                CurrentState = BossState.Idle;
            }

            StompMovementAI();
        }

        private void StompMovementAI()
        {

            Mod.Logger.Info($"stompTimer = {stompTimer}");
            if (stompTimer <= 0)
            {
                StompAI();
                return;
            }

            FollowPlayerCeilingAI();
        }

        private void FollowPlayerCeilingAI()
        {
            stompTimer--;

            const float ceilingOffset = 300f;

            NPC.Center = new Vector2(target.Center.X,target.Center.Y - ceilingOffset);
        }

        private void StompAI()
        {
            if (NPC.Bottom.Y >= target.Center.Y)
            {
                stompTimer = (int)Utils1.FormatTimeToTick(Second: 2);
                return;
            }

            if (NPC.velocity.Y == 0)
                NPC.velocity = new Vector2(0f, 9f);

            NPC.velocity.Y += 0.3f;

            CurrentState = BossState.Falling;
            NPC.noTileCollide = true;
        }

        private void ShootAI()
        {
            if (projectileType == -1) projectileType = 0;
            Vector2 velocity = Vector2.Normalize(target.Center - NPC.Center) * Main.rand.Next(3, 10);

            int projectileIndex = -1;

            if (shootTimer >= shootTimerTrigger) 
            {
                if (!esServer) return;
                switch (projectileType)
                {
                    case 1:
                        velocity /= 2f;
                        break;
                    case 2:
                        velocity = Vector2.Normalize(target.Center - NPC.Center) * Main.rand.Next(6, 8);
                        velocity.Y = -7f;
                        break;
                    case 3:
                        velocity.X *= 1.5f;
                        break;
                }
                projectileIndex = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, gemProjectiles[projectileType], 10, 0f, Main.myPlayer, NPC.whoAmI);

                if (projectileIndex < 0)
                    return;

                Projectile proj = Main.projectile[projectileIndex];

                proj.penetrate = -1;
                proj.hostile = true;
                proj.friendly = false;
                projectileType = Main.rand.Next(gemProjectiles.Count);
                shootTimer = 0;
                NPC.netUpdate = true;
            }
            else 
            {
                Dust.NewDust(NPC.Center, 10, 10, gemDust[projectileType]);   
            }
        }

        private void SpawnAI()
        {
            if (!esServer) return;
            NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.Center.X,(int)NPC.Center.Y,NPCID.CaveBat);

            if (!spawnWorms && Main.expertMode && NPC.life < NPC.lifeMax / 2)
            {
                SpawnWorm((int)NPC.Center.X, (int)NPC.Center.Y + 160);
                SpawnWorm((int)NPC.Center.X + 30, (int)NPC.Center.Y + 160);
                SpawnWorm((int)NPC.Center.X - 30, (int)NPC.Center.Y + 160);

                spawnWorms = true;
            }
        }

        private void SpawnWorm(int x, int y)
        {
            int npcIndex = NPC.NewNPC(NPC.GetSource_FromAI(),x,y,NPCID.GiantWormHead);

            Main.npc[npcIndex].lifeMax = 50;
            Main.npc[npcIndex].life = 50;
        }

        public override void ModifyHitByItem(Player player,Item item,ref NPC.HitModifiers modifiers)
        {
            if (item.pick > 0)
                modifiers.FinalDamage *= 5;

            base.ModifyHitByItem(player, item, ref modifiers);
        }

        public override void OnHitByItem(Player player,Item item,NPC.HitInfo hit,int damageDone)
        {
            if (CurrentState == BossState.Sleep)
                CurrentState = BossState.Idle;

            base.OnHitByItem(player, item, hit, damageDone);
        }

        public override void OnHitByProjectile(Projectile projectile,NPC.HitInfo hit,int damageDone){
            if (CurrentState == BossState.Sleep)
                CurrentState = BossState.Idle;

            base.OnHitByProjectile(projectile, hit, damageDone);
        }

        public override void OnSpawn(IEntitySource source)
        {
            CurrentState = BossState.Sleep;

            shootTimer = 0; // Shoot
            spawnerTimer = 0; // Spawn
            stompActivationTimer = 0; // Stomp

            base.OnSpawn(source);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)CurrentState);
            writer.Write(stompTimer);
            writer.Write(spawnWorms);
            writer.Write(projectileType);
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            _currentState = (BossState)reader.ReadByte();
            stompTimer = reader.ReadInt32();
            spawnWorms = reader.ReadBoolean();
            projectileType = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
        }
    }
}