using CalamityMod;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Rep;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant
{


    [AutoloadBossHead]
    public class InfernalTyrantHead : TyrantHead
    {
        public override int BodyType => ModContent.NPCType<InfernalTyrantBody>();

        public override int TailType => ModContent.NPCType<InfernalTyrantTail>();

        InfernalTyrant_AnimationModule animationModule;
        InfernalTyrant_AttackModule attackModule;

        InfernalTyrant_AuxiliaryModule auxModule;

        public override void SetStaticDefaults()
        {
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/InfernalTyrantHead_Bestiary", 
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }
   
        public bool head;
        public bool CanTp = false;
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerHead);      
            NPC.aiStyle = -1;
            NPC.Size = new(75, 75);
            
            NPC.boss = true;
            NPC.lifeMax = BaseStats.LifeMax;
           
            NPC.damage = 200;
            NPC.defense = TyranStats.TyrantArmor(999, NPC);
            NPC.npcSlots = 20f;
            NPC.lavaImmune = true;
            
            Music = MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Infernal_Tyrant");

            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();

            InitializeModules();
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {
            if(NPC.TryGetGlobalNPC(out CalamityGlobalNPC calamityGlobalNPC))
            {
                calamityGlobalNPC.canBreakPlayerDefense = true;
            }

            RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,0.02f,0.22f,0.29f,0.3f,0.5f);
            InfernalTyrant_AuxiliaryModule.CalamityLifeScale(NPC,BaseStats.LifeMax);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("A great and dreaded worm rules the underworld with an iron fist and his flames, powerful and majestic in equal parts, maintain the order and warmth of the underworld.")
            ]);
        }

        public override void Init()
        {
            MinSegmentLength = 15;
            MaxSegmentLength = MaxSegmentCount(MinSegmentLength);
            head = true;
            CommonWormInit(this);
        }

        bool modulesInitialized = false;
        private void InitializeModules()
        {
            if (modulesInitialized)
                return;

            modulesInitialized = true;
            auxModule = new InfernalTyrant_AuxiliaryModule
            {
                npc = NPC,
                currentTarget = Main.player[NPC.target],
                attackCounter = attackCounter
            };

            attackModule = new InfernalTyrant_AttackModule(animationModule, auxModule);
            animationModule = new InfernalTyrant_AnimationModule(auxModule);
        }
        internal static void CommonWormInit(TyrantBaseWorm worm)
        {
            worm.MoveSpeed = 30f;
            worm.Acceleration = 0.245f;
        }

        private int attackCounter;
        private int attackCounterMaxValue = 800;
        private int movementTimer;
        private int movementPhase;
        private Vector2 dashDirection;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
            writer.Write(movementTimer);
            writer.Write(movementPhase);
         
            writer.Write(dashDirection.X);
            writer.Write(dashDirection.Y);

            animationModule.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
            movementTimer = reader.ReadInt32();
            movementPhase = reader.ReadInt32();
            dashDirection = new Vector2(reader.ReadSingle(), reader.ReadSingle());

            animationModule.ReceiveExtraAI(reader);
        }
        private int MaxSegmentCount(int MinSegmentLength) 
        {
            int scale = 0;
            if (DificultyUtils.MasochistMode) scale = 30;
            else if (DificultyUtils.EternityMode) scale = 25;
            else if (DificultyUtils.InfernumMode) scale = 25;
            else if (DificultyUtils.Death) scale = 20;
            else if (DificultyUtils.Revengeance) scale = 10;
            else if (Main.masterMode) scale = 5;
            else if (Main.expertMode) scale = 2;
            return MinSegmentLength + scale;
        }

        public bool SpawnClon = false;
        
        public bool IsPhase2 => auxModule.IsPhase2;
        public override void AI()
        {
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.defense = TyranStats.TyrantArmor(999, NPC);

            if (!GenericVariables.SizeChanged[0])
            {
                try
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.Size = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f);
                    }
                }
                catch 
                {
                    NPC.Size = new(75, 75);
                }
                GenericVariables.SizeChanged[0] = true;
            }

            Player target = Main.player[NPC.target];

            UpdateCounters(target);
            DespawnSafeCheck(target, this);
            attackModule.DoAttacks(target);

            if(IsPhase2) SerpentMovementAi(target);


            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (GenericVariables.SpawnCounter >= GenericVariables.TimeInmune)
                {
                    SetDefautsCalamity();
                    GenericVariables.IsSpawned = true;
                }
                else
                {
                    if (!GenericVariables.IsSpawned)
                    {
                        GenericVariables.SpawnCounter++;
                        RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC, 1f, 1f, 1f, 1f, 1f);
                    }
                }

                if (DificultyUtils.InfernumMode)
                {
                    if (attackCounter % 4 == 0)
                    {
                        for (int i = -3; i <= 3; i++)
                        {
                            Vector2 FlameVelocity = NPC.velocity * 1.25f;//1.25
                            FlameVelocity = FlameVelocity.RotatedBy(i * 20);
                            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, FlameVelocity, ProjectileID.Flames, 30, 0f, Main.myPlayer, Math.Sign(i));
                            Main.projectile[projectile].timeLeft = 30;
                            Main.projectile[projectile].tileCollide = false;
                            Main.projectile[projectile].friendly = false;
                            Main.projectile[projectile].hostile = true;
                            Main.projectile[projectile].usesLocalNPCImmunity = true;
                        }
                    }
                }

            }
            if (RemnantOfTheAncientsMod.FargosSoulMod != null)
            {
                if (DificultyUtils.EternityMode || DificultyUtils.MasochistMode)
                {
                    if (NPC.boss && !SpawnClon)
                    {
                        if (MathUtils.GetPorcentage(NPC.life, NPC.lifeMax) < (DificultyUtils.MasochistMode ? 70f : 50f))
                        {
                            var a = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<InfernalTyrantHead>());
                            Main.npc[a].boss = false;
                            Main.npc[a].lifeMax /= 8;
                            Main.npc[a].life /= 8;
                            Main.npc[a].scale = 0.5f;
                            Main.npc[a].damage /= 2;

                            SpawnClon = true;
                        }
                    }
                }
            }

            InfernalTyrant_AuxiliaryModule.LifeSpeed(this);
            UpdateMovement();
        }

        public override void OnSpawn(IEntitySource source)
        {
            GenericVariables.IsSpawned = false;
            GenericVariables.SpawnCounter = 0;
        }
        
        public void UpdateCounters(Player target)
        {
            int distance = (int)Vector2.Distance(NPC.Center, target.Center);
            int distanceThreshold = auxModule.IsEnraged ? 600 : IsPhase2 ? 400 : 200;

            if (distance < distanceThreshold && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
            {
                if (attackCounter <= 0)
                {
                    if (auxModule.IsEnraged)
                        attackCounterMaxValue = !Main.expertMode ? 600 : 700;
                    else if (IsPhase2)
                        attackCounterMaxValue = !Main.expertMode ? 650 : 750;
                    else
                        attackCounterMaxValue = !Main.expertMode ? 700 : 800;

                    attackCounter = attackCounterMaxValue;
                    NPC.netUpdate = true;
                }       
            }
            if (attackCounter > 0)
            {
                attackCounter--;
            }
        }

        private void UpdateMovement()
        {
            if (!IsPhase2)
            {
                Acceleration = 0.245f;
                return;
            }

            movementTimer--;
            if (movementTimer <= 0)
            {
                int maxPhases = auxModule.IsEnraged ? 3 : 2;
                movementPhase = (movementPhase + 1) % maxPhases;
                movementTimer = movementPhase switch
                {
                    0 => 180,
                    1 => auxModule.IsEnraged ? 120 : 150,
                    2 => 100,
                    _ => 180
                };
                NPC.netUpdate = true;
            }

            switch (movementPhase)
            {
                case 1:
                    MoveSpeed = auxModule.IsEnraged ? 42f : 36f;
                    Acceleration = 0.20f;
                    break;
                case 2:
                    MoveSpeed = 34f;
                    Acceleration = 0.15f;
                    break;
                default:
                    MoveSpeed = 30f;
                    Acceleration = auxModule.IsEnraged ? 0.28f : 0.26f;
                    break;
            }
        }


        private float amplitud = 3.0f;          // grados máximos de desviación por frame
        private float frecuencia = 0.02f;       // velocidad de ondulación (ciclo completo cada ~50 ticks)

        private void SerpentMovementAi(Player target)
        {
            if (NPC.velocity.LengthSquared() < 0.01f) return;

            float tiempo = (float)Main.timeForVisualEffects;

            // Ángulo oscilante en radianes: rota la velocidad ±amplitud grados
            float angulo = MathHelper.ToRadians(amplitud)
                * (float)Math.Sin(tiempo * frecuencia * MathHelper.TwoPi + NPC.whoAmI * 1.5f);

            // Rotar la velocidad existente (no suma, no usa dirección externa)
            NPC.velocity = NPC.velocity.RotatedBy(angulo);
        }

        public void FindTarget()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                if (InfernalTyrant_AuxiliaryModule.AllPlayersDead())
                {
                    NPC.TargetClosest(true);
                    NPC.velocity *= 0.1f;
                }
            }
        } 
        public void DespawnSafeCheck(Player player, TyrantBaseWorm worm)
        {
            float positionY = NPC.position.Y / 16;
            float limit = Main.maxTilesY - 50f;
            if ((!NPC.WithinRange(player.Center, 170 * 16f) || positionY >= limit) && !player.dead)
            {
                Vector2 directionToPlayer = Vector2.Normalize(player.Center - NPC.Center);
                NPC.velocity = directionToPlayer * worm.MoveSpeed/4;
                NPC.netUpdate = true;
            }
        }
     
       
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantHeadGore1").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantHeadGore2").Type, NPC.scale);
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood,hit.HitDirection, -1f);
                }
                RemnantDownedBossSystem.downedTyrant = true;
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => head ? null : false;
        public override void BossLoot(ref string name, ref int potionType)
        {
            RemnantDownedBossSystem.downedTyrant = true;
            potionType = ItemID.GreaterHealingPotion;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                return true;

            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                return;
        }

       

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.NormalvsExpertOneFromOptions(1, 999999999,
            [
                ModContent.ItemType<SpikeSaber>(), 
                ModContent.ItemType<TyrantRepeater>(), 
                ModContent.ItemType<Tyran_Blast>(), 
                ModContent.ItemType<EvilEyeStaff>() 
            ]));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernalMask>(), 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernalTrophy>(), 10));
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<infernalBag>()));
            if(DificultyUtils.InfernumMode) 
                npcLoot.Add(RemnantDropRules.InfernumModeCommonDrop(ModContent.ItemType<Tyrant_Relic>()));
            else 
                npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Tyrant_Relic>()));

            if (RemnantOfTheAncientsMod.CalamityMod != null) CalamityDrop(npcLoot);
        }
        [JITWhenModsEnabled("CalamityMod")]
        private static void CalamityDrop(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(CallUtils.TryGetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EssenceofChaos"), 1, 5, Utils1.ReaperDropScaler(15)));
        }
    }

    internal class InfernalTyrantBody : TyrantBody
    {
        [Obsolete]
        public override void SetStaticDefaults()
        { 
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
        }

        public override void SetDefaults()
        {

            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.lifeMax = BaseStats.LifeMax;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();
            NPC.defense = TyranStats.TyrantArmor(999, NPC);
            NPC.aiStyle = -1;
            NPC.Size = new(75, 75);

            NPC.boss = true;
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {
            NPC.Calamity().canBreakPlayerDefense = true;
            RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,0.2f, 0.42f, 0.59f, 0.6f,0.6f);
            InfernalTyrant_AuxiliaryModule.CalamityLifeScale(NPC,BaseStats.LifeMax);
        }
       
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                return true;

            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            TyranStats.DrawGlow(NPC, "InfernalTyrantBody");
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    for(int i = 1; i < 3; i++) 
                    {
                        Vector2 velocity = new(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7));
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, velocity, Mod.Find<ModGore>("InfernalTyrantBodyGore"+i).Type, NPC.scale);
                    }      
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood,hit.HitDirection, -1f);
                }
                RemnantDownedBossSystem.downedTyrant = true;
            }
        }
        public override void AI()
        {
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.defense = TyranStats.TyrantArmor(999, NPC);
            GenericVariables gv = new GenericVariables();
            if (!GenericVariables.SizeChanged[1])
            {
                try
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.Size = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f);
                    }
                }
                catch (Exception)
                {
                    NPC.Size = new(75, 75);
                }
                GenericVariables.SizeChanged[1] = true;
            }
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (GenericVariables.SpawnCounter >= GenericVariables.TimeInmune)
                {
                    SetDefautsCalamity();
                }
                else
                {
                    if (RemnantOfTheAncientsMod.CalamityMod != null) RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,1f, 1f, 1f, 1f, 1f);
                }
            }
        }
        public override void Init()
        {
            InfernalTyrantHead.CommonWormInit(this);
        }
    }

    internal class InfernalTyrantTail : TyrantTail
    {
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                return true;

            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return false;
        }

        [Obsolete]
        public override void SetStaticDefaults()
        {

            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true;
            
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.lifeMax = BaseStats.LifeMax;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity(); 
            NPC.Size = new(75, 75);  
            NPC.defense = 25;
            NPC.aiStyle = -1;
            NPC.boss = true;
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {
            NPC.Calamity().canBreakPlayerDefense = true;
            RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,0.01f, 0.12f, 0.19f, 0.2f, 0.2f);
            InfernalTyrant_AuxiliaryModule.CalamityLifeScale(NPC,BaseStats.LifeMax);
        }

        public override void AI()
        {
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.defense = TyranStats.TyrantArmor(25, NPC);
            if (!GenericVariables.SizeChanged[2])
            {
                try
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.Size = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f);
                    }
                }
                catch (Exception)
                {
                    NPC.Size = new(75, 75);
                }
                GenericVariables.SizeChanged[2] = true;
            }
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                if (GenericVariables.SpawnCounter >= GenericVariables.TimeInmune)
                {
                    SetDefautsCalamity();
                }
                else
                {
                    RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC, 1f, 1f, 1f, 1f, 1f);
                }
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantTailGore1").Type, NPC.scale);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantTailGore2").Type, NPC.scale);
                }
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood,hit.HitDirection, -1f);
                }
                RemnantDownedBossSystem.downedTyrant = true;
            }
            base.HitEffect(hit);
        }
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 2;
            base.ModifyHitByItem(player, item, ref modifiers);
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 2;
            base.ModifyHitByProjectile(projectile, ref modifiers);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            TyranStats.DrawGlow(NPC, "InfernalTyrantTail");   
        }
        public override void Init()
        {
            InfernalTyrantHead.CommonWormInit(this);
        }
    }
}