using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Extensions;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Gemstone;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
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
        private int _currentPhase = 0;
        private int CurrentPhase
        {
            get => _currentPhase;
            set
            {
                _currentPhase = value;
                if (Main.netMode != NetmodeID.MultiplayerClient) NPC.netUpdate = true;
            }
        }


        private int breackingDelay => Utils1.FormatTimeToTick(Second: 1);
        private int breackingTimer = 0;



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
            if (CurrentPhase == 1 || CurrentPhase == 3)
            {
                AnimatePhase();
                return;
            }
            CheckPhase();

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
                if(NPC.Center.DistanceSQ(playerHead) <= (tolerance * tolerance).ToTilePosition())
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

        public void CheckPhase()
        {
            if (CurrentPhase == 0 && NPC.life < (NPC.lifeMax / 5) * 4)
            {
                CurrentPhase = 1;
            }
            if (CurrentPhase == 2 && NPC.life < (NPC.lifeMax / 5) * 2)
            {
                CurrentPhase = 3;
            }
        }

        public void AnimatePhase()
        {
            if (CurrentPhase == 1 || CurrentPhase == 3)
            {
                NPC.dontTakeDamage = true;
                if(breackingTimer < breackingDelay)
                {
                    breackingTimer++;
                }
                else if(currentIndex < frameCount-1)
                {
                    currentIndex++;
                    SoundEngine.PlaySound(SoundID.Tink with { Pitch = -0.8f, PitchVariance = 0.3f, Volume = 1.6f });
                    breackingTimer = 0;
                }
                else
                {
                    CurrentPhase++;
                    currentIndex = 0;
                    NPC.dontTakeDamage = false;
                    SoundEngine.PlaySound(SoundID.Tink with { Pitch = -0.9f, PitchVariance = 0.3f, Volume = 1.6f });

                    if (Main.netMode != NetmodeID.Server)
                    {
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("GemstoneCrusherP1Gore1").Type, NPC.scale);
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("GemstoneCrusherP1Gore2").Type, NPC.scale);
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("GemstoneCrusherP1Gore3").Type, NPC.scale);
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("GemstoneCrusherP1Gore4").Type, NPC.scale);
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("GemstoneCrusherP1Gore5").Type, NPC.scale);
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("GemstoneCrusherP1Gore6").Type, NPC.scale);
                    }
                }
                
            }
        }
 

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)CurrentState);
            writer.Write(stompTimer);
            writer.Write(spawnWorms);
            writer.Write((int)projectileType);
            writer.Write((byte)CurrentStompPhase);
            writer.Write(CurrentPhase);
            base.SendExtraAI(writer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            _currentState = (BossState)reader.ReadByte();
            stompTimer = reader.ReadInt32();
            spawnWorms = reader.ReadBoolean();
            projectileType = (GemType)reader.ReadInt32();
            _currentStompPhase = (StompType)reader.ReadByte();
            _currentPhase = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
        }
         
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if(NPC.AnyNPCs(Type)) return 0f;
            float chance = SpawnCondition.Cavern.Chance * 0.006f; 
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

            string TextureGlowPath = $"{ruta}_Glow";
            if (CurrentPhase >= 2) TextureGlowPath = TextureGlowPath +"_2";

            Color color = !shootTelegraph ? Color.White : gemData[projectileType].color;
            if (TextureGlowPath != null && CurrentState != BossState.Sleep)
            {
                // item.glowMask = RemnantOfTheAncientsMod.AddGlowMask(Texture);
                Texture2D texture = ModContent.Request<Texture2D>(TextureGlowPath, AssetRequestMode.ImmediateLoad).Value;
                Vector2 position = (NPC.position - Main.screenPosition) + new Vector2(NPC.width * 0.5f, NPC.height - texture.Height * 0.5f + 2f);
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), color, NPC.rotation, texture.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0f);
            }
            base.PostDraw(spriteBatch, screenPos, drawColor);
        }

        int frameCount => CurrentPhase == 4? 1: 8;
        int currentIndex = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            if (NPC.IsABestiaryIconDummy)
                return true;


            // string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;

            

            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(texturePath());
            
            Vector2 drawPos = NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY);
             

            int frameHeight = Texture.Height / frameCount;

            
            Rectangle frame = new Rectangle(
                0,
                frameHeight * currentIndex,
                Texture.Width,
                frameHeight
            );
            NPC.frame = frame;

            Vector2 drawOrigin = frame.Size() / 2f;
            Vector2 position = new Vector2
            (
                NPC.position.X - Main.screenPosition.X + NPC.width * 0.5f,
                NPC.position.Y - Main.screenPosition.Y + NPC.height - Texture.Height/ frameCount * 0.5f + 2f
            );
            DrawDash(drawColor, drawOrigin, Texture);
            Main.EntitySpriteDraw(Texture, position, NPC.frame, drawColor, 0, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            return false;
        }
        internal void DrawDash(Color drawColor, Vector2 drawOrigin, Texture2D Texture)
        {
          
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
                    Vector2 position = new Vector2
                    (
                        NPC.oldPos[k].X - Main.screenPosition.X + NPC.width * 0.5f,
                        NPC.oldPos[k].Y - Main.screenPosition.Y + NPC.height - Texture.Height / frameCount * 0.5f + 2f
                    );
                    Color color = NPC.GetAlpha(drawColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                    Main.EntitySpriteDraw(Texture, position, NPC.frame, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);

                }
            }
        }

        internal string texturePath()
        {
            string variation = CurrentPhase switch
            {
                4 => "_P4",
                3 => "_P2",
                2 => "_P2",
                _ => "_P1"
            };
            string result = GetType().FullName.Replace('.', '/') + variation;
            return result;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Amethyst, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Topaz, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Emerald, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Sapphire, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Ruby, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Diamond, 3, 1, 3));


            npcLoot.Add(ItemDropRule.NormalvsExpertOneFromOptions(1, 999999999,
            [
                ModContent.ItemType<WandOfCaverns>(),
                ModContent.ItemType<StoneBlunderbuss>(),
                ModContent.ItemType<StoneMortarStaff>(),
                /*ItemType<DesertEdge>(),
                ItemType<DesertStaff>(),
                ItemType<DesertTome>()*/
            ]));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<TreasureRock>()));
            /*
            npcLoot.Add(ItemDropRule.Common(ItemType<Sand_escense>(), 1, 5, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 1, 1, 50));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.Amber, 6, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.IsHardModeRule(), ItemID.AncientBattleArmorMaterial, 5, 1, 1, Utils1.ReaperDropScaler(1)));

            npcLoot.Add(ItemDropRule.Common(ItemType<DesertAMask>(), 7));
            npcLoot.Add(ItemDropRule.Common(ItemType<DesertTrophy>(), 10));

           
            if (DificultyUtils.InfernumMode) npcLoot.Add(RemnantDropRules.InfernumModeCommonDrop(ItemType<Desert_Relic>()));
            else npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<Desert_Relic>()));*/

        }
    }
}