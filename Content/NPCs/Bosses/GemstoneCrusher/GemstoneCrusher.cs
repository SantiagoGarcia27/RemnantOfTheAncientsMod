using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Extensions;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static RemnantOfTheAncientsMod.Common.Enums.GlobalEnum;

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
        private enum StompType
        {
            none,
            elevate,
            moveTo,
            follow
        }
        private StompType _currentStompPhase = StompType.none;
        private StompType CurrentStompPhase 
        {
            get => _currentStompPhase;
            set
            {
                if (_currentStompPhase == value)
                    return;

                _currentStompPhase = value;
                if (Main.netMode != NetmodeID.MultiplayerClient) NPC.netUpdate = true;
            }
        }

        private int stompTimer = Utils1.FormatTimeToTick(Second: 2);
        private int stompActivationTimerTrigger 
        {
            get {
                if (NPC.life < NPC.lifeMax / 5) return Utils1.FormatTimeToTick(Second: 5);
                if (NPC.life < NPC.lifeMax / 2) return Utils1.FormatTimeToTick(Second: 10);
                return Utils1.FormatTimeToTick(Second: 20);
            }
        }

        private readonly int shootTimerTrigger = Utils1.FormatTimeToTick(Second: 5);
        private readonly int spawnTimerTrigger = Utils1.FormatTimeToTick(Second: 15);

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
        private int StompTimmerMax
        {
            get
            {
                if (NPC.life < NPC.lifeMax / 3) return Utils1.FormatTimeToTick(Second: 1.5f);
                if (NPC.life < NPC.lifeMax / 2) return Utils1.FormatTimeToTick(Second: 1.7f);
                return Utils1.FormatTimeToTick(Second: 2);
            }
        }

        private bool shootTelegraph = false;
        private bool spawnWorms;

        private readonly Dictionary<GemType, (int Projectile, Color color)> gemData = new()
        {
            [GemType.Sapphire] = (
                ModContent.ProjectileType<GemstoneCrusherProj_Sapphire>(),
                Color.Blue
            ),

            [GemType.Emerald] = (
                ModContent.ProjectileType<GemstoneCrusherProj_Emerald>(),
                Color.Green
            ),

            [GemType.Ruby] = (
                ModContent.ProjectileType<GemstoneCrusherProj_Ruby>(),
                Color.Red
            ),

            [GemType.Diamond] = 
            (
                ModContent.ProjectileType<GemstoneCrusherProj_Diamond>(),
                Color.White
            )
        };
     
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.TrailCacheLength[Type] = 10;
            NPCID.Sets.TrailingMode[Type] = 0;

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
            NPC.boss = false;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.netAlways = true;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.Dig with { Pitch = -1.8f, PitchVariance = 0.3f, Volume = 1.6f };

            base.SetDefaults();
        }

        GemType projectileType = GemType.none;
        bool esServer = Main.netMode != NetmodeID.MultiplayerClient;
        public override void AI()
        {
            UpdateTarget();
            if (esServer && CurrentState == BossState.Sleep)
            {
                if (NPC.justHit)
                {
                    CurrentState = BossState.Idle;
                    NPC.boss = true;
                }

                return;
            }
            if (target == null || CurrentState == BossState.Sleep)
                return;
            stompActivationTimer++;

            if (stompActivationTimer >= stompActivationTimerTrigger)
            {
                stompActivationTimer = 0;
                stompTimer = StompTimmerMax;
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

                if (shootTimer >= shootTimerTrigger - Utils1.FormatTimeToTick(Second: 1))
                { 
                    ShootAI();
                }

                if (spawnerTimer >= spawnTimerTrigger)
                {
                    SpawnAI();
                    spawnerTimer = 0;
                }

                //Stomp collision Effects
                if (NPC.collideY && !NPC.noTileCollide && CanUseStompEffect)
                {
                    StompFallEffectAi();
                }
            }
            SecurityCheck();
            base.AI();
        }
        private void SecurityCheck()
        {
            if (CurrentStompPhase == StompType.elevate) NPC.noTileCollide = true;
        }
        private void UpdateTarget()
        {

            if (Main.netMode == NetmodeID.SinglePlayer && target != null && target.active && !target.dead)
                return;
           
            NPC.TargetClosest(faceTarget: true);
            target = Main.player[NPC.target]; 
        }
        private bool CanUseStompEffect = false;

        private void MovementAI()
        {
            if (NPC.Bottom.Y >= target.Center.Y && CurrentState == BossState.Falling && NPC.noTileCollide)
            {
                NPC.noTileCollide = false;
                CurrentState = BossState.Idle;
                CanUseStompEffect = true;
            }
            
            StompMovementAI();
        }
        private void StompFallEffectAi()
        {
            int cloudWidth = NPC.width / 2 + 3;
            Tile tile = Framing.GetTileSafely(NPC.Bottom.ToTileCoordinates());
           
            ModTile floorTile =  TileLoader.GetTile(tile != null ? tile.TileType : 1);
            int dustType = floorTile != null? floorTile.DustType : (int)DustID.Stone;
            for (int offset = -cloudWidth; offset <= cloudWidth; offset++)
            {
                Vector2 pos = NPC.Center + new Vector2(offset, 0);
                Vector2 velocity = pos - NPC.Center;
                velocity.X /= 2;
                velocity.Y -= 1;
                Dust.NewDustDirect(pos, 10, 10, DustID.Stone, velocity.X, velocity.Y, Scale: Main.rand.Next(1, 3));
                velocity += new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(1, 1));
                Dust.NewDustDirect(pos, 10, 10, DustID.Dirt, velocity.X, velocity.Y, Scale: Main.rand.Next(1, 3));
                velocity += new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(1, 1));
                Dust.NewDustDirect(pos, 10, 10, dustType, velocity.X, velocity.Y, Scale: Main.rand.Next(1, 5));

            }
            SoundEngine.PlaySound(SoundID.Item14 with { Pitch = -1.8f, PitchVariance = 0.2f, Volume = 1.6f });
            SoundEngine.PlaySound(SoundID.Item14 with { Pitch = -0.8f, PitchVariance = 0.2f, Volume = 1.0f });
            SoundEngine.PlaySound(SoundID.Item62 with { Pitch = -0.8f, PitchVariance = 0.3f, Volume = 1.6f });
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode with { Pitch = -0.5f, Volume = 0.4f });
            
            CanUseStompEffect = false;
        }

        private void StompMovementAI()
        {

            if (stompTimer <= 0)
            {
                StompAI();
                CurrentStompPhase = StompType.none;
                return;
            }
            if(CurrentStompPhase == StompType.none) CurrentStompPhase = StompType.elevate;
            FollowPlayerCeilingAI();
        }

        
        private void FollowPlayerCeilingAI()
        {
            
            const float velocity = 5f;
            const float ceilingOffset = 300f;
            const float tolerance = 5; //In Tiles
            Vector2 playerHead = new Vector2(target.Center.X, target.Center.Y - ceilingOffset);

            NPC.velocity = Vector2.Zero;
            if (CurrentStompPhase == StompType.elevate)
            {
                if (NPC.Center.Y <= playerHead.Y)
                {
                    CurrentStompPhase = StompType.moveTo;
                    return;
                }
                else NPC.velocity -= new Vector2(0, velocity);
            }
            else if (CurrentStompPhase == StompType.moveTo)
            {
                if(NPC.Center.DistanceSQ(playerHead) <= DistanceUtils.ToCoordenatePosition(tolerance * tolerance))
                {
                    CurrentStompPhase = StompType.follow;
                    return;
                }
                NPC.velocity = playerHead - NPC.Center;
                NPC.velocity.Normalize();
                NPC.velocity *= velocity * 3;
            }
            else
            {
                stompTimer--;
                NPC.Center = new Vector2(target.Center.X, target.Center.Y - ceilingOffset);
            }
        }

        private void StompAI()
        {
            if (NPC.Bottom.Y >= target.Center.Y)
            {
                stompTimer = StompTimmerMax;
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
            if (projectileType == GemType.none) projectileType = GemType.Sapphire;
            Vector2 velocity = Vector2.Normalize(target.Center - NPC.Center) * Main.rand.Next(3, 10);

            int projectileIndex = -1;

            if (shootTimer >= shootTimerTrigger) 
            {
                if (!esServer) return;
                switch (projectileType)
                {
                    case GemType.Emerald:
                        velocity /= 2f;
                        break;
                    case GemType.Ruby:
                        velocity = Vector2.Normalize(target.Center - NPC.Center) * Main.rand.Next(5, 7);
                        velocity.Y = -7f;
                        break;
                    case GemType.Diamond:
                        velocity.X *= 1.5f;
                        break;
                }
                projectileIndex = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, gemData[projectileType].Projectile, 10, 0f, Main.myPlayer, NPC.whoAmI);

                if (projectileIndex < 0)
                    return;

                Projectile proj = Main.projectile[projectileIndex];

                proj.penetrate = -1;
                proj.hostile = true;
                proj.friendly = false;

                projectileType = gemData.RandomKey();
                shootTimer = 0;
                NPC.netUpdate = true;
                shootTelegraph = false;
            }
            else 
            {
                shootTelegraph = true; 
            }
        }

        private void SpawnAI()
        {
            if (!esServer) return;
            int batID = Main.masterMode ? NPCID.GiantBat : NPCID.CaveBat;
            int npcIndex = NPC.NewNPC(NPC.GetSource_FromAI(),(int)NPC.Center.X,(int)NPC.Center.Y, batID);
            if (Main.masterMode)
            {
                Main.npc[npcIndex].lifeMax /= 5;
                Main.npc[npcIndex].life /= 5;
                Main.npc[npcIndex].defense = 2;
                Main.npc[npcIndex].damage /= 3;
            }

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
            {
                modifiers.DefenseEffectiveness *= 0;
                modifiers.FinalDamage *= 2.1f;
            }
            base.ModifyHitByItem(player, item, ref modifiers);
        }

        public override void OnHitByItem(Player player,Item item,NPC.HitInfo hit,int damageDone)
        {
            if (CurrentState == BossState.Sleep)
            {
                CurrentState = BossState.Idle;
                NPC.boss = true;
            }

            base.OnHitByItem(player, item, hit, damageDone);
        }

        public override void OnHitByProjectile(Projectile projectile,NPC.HitInfo hit,int damageDone){
            if (CurrentState == BossState.Sleep)
            {
                CurrentState = BossState.Idle;
                NPC.boss = true;
            }
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
            writer.Write((int)projectileType);
            writer.Write((byte)CurrentStompPhase);
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            _currentState = (BossState)reader.ReadByte();
            stompTimer = reader.ReadInt32();
            spawnWorms = reader.ReadBoolean();
            projectileType = (GemType)reader.ReadInt32();
            _currentStompPhase = (StompType)reader.ReadByte();
            base.ReceiveExtraAI(reader);
        }
         
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if(NPC.AnyNPCs(Type)) return 0f;
            float chance = SpawnCondition.Cavern.Chance * 0.19f; 
            return chance;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if(NPC.life <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item14 with { Pitch = -1.8f, PitchVariance = 0.2f, Volume = 1.6f });
                SoundEngine.PlaySound(SoundID.Item14 with { Pitch = -0.8f, PitchVariance = 0.2f, Volume = 1.0f });
                SoundEngine.PlaySound(SoundID.Item62 with { Pitch = -0.8f, PitchVariance = 0.3f, Volume = 1.6f });
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode with { Pitch = -0.5f, Volume = 1.8f });
            }
            base.HitEffect(hit);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Type type = GetType();
            String ruta = type.FullName.Replace('.', '/');
            string Texture = $"{ruta}_Glow";

            Color color = !shootTelegraph ? Color.White : gemData[projectileType].color;
            if (Texture != null && CurrentState != BossState.Sleep)
            {
                // item.glowMask = RemnantOfTheAncientsMod.AddGlowMask(Texture);
                Texture2D texture = ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad).Value;
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        NPC.position.X - Main.screenPosition.X + NPC.width * 0.5f,
                        NPC.position.Y - Main.screenPosition.Y + NPC.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    color,
                    NPC.rotation,
                    texture.Size() * 0.5f,
                    NPC.scale,
                    SpriteEffects.None,
                    0f
                );
            }
            base.PostDraw(spriteBatch, screenPos, drawColor);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            if (NPC.IsABestiaryIconDummy)
                return true;


           // string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(base.Texture);
            Vector2 drawOrigin = NPC.frame.Size() / 2f;
            Vector2 drawPos = NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY);
            if (CurrentStompPhase != StompType.none)
            {
                drawColor = Main.rand.Next(0, 2) switch
                {
                    0 => Color.Gray,
                    1 => Color.DarkGray,
                    2 => Color.LightGray,
                    _ => Color.SlateGray
                };
                for (int k = 0; k < NPC.oldPos.Length; k++)
                {
                    Vector2 drawPos2 = NPC.oldPos[k] + NPC.Size / 2f - Main.screenPosition + new Vector2(0f, NPC.gfxOffY);
                    Color color = NPC.GetAlpha(drawColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                    Main.EntitySpriteDraw(Texture, drawPos2, NPC.frame, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);

                }
            }

            //Main.EntitySpriteDraw(Texture, drawPos, NPC.frame, drawColor, 0, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            return true;
        }
    }
}