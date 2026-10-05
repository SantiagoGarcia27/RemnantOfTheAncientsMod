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
using SangarUtilities.Common.UtilsTweaks;
using System;
using System.IO;
using Terraria;
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
        InfernalTyrant_MovmentModule movementModule;

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

            InitializeModules();

            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();
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
            MaxSegmentLength = InfernalTyrant_AuxiliaryModule.MaxSegmentCount(MinSegmentLength : 15);
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
            };

            attackModule = new InfernalTyrant_AttackModule(animationModule, auxModule);
            animationModule = new InfernalTyrant_AnimationModule(auxModule);
            movementModule = new InfernalTyrant_MovmentModule(auxModule, this);
        }
        internal static void CommonWormInit(TyrantBaseWorm worm)
        {
            worm.MoveSpeed = 30f;
            worm.Acceleration = 0.245f;
        }
   

        public override void SendExtraAI(BinaryWriter writer)
        {
            animationModule.SendExtraAI(writer);
            auxModule.SendExtraAI(writer);
            attackModule.SendExtraAI(writer);
            movementModule.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            animationModule.ReceiveExtraAI(reader);
            auxModule.ReceiveExtraAI(reader);
            attackModule.ReceiveExtraAI(reader);
            movementModule.ReceiveExtraAI(reader);
        }
        
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
                        NPC.Size = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f);
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

            if (RemnantOfTheAncientsMod.CalamityMod != null) attackModule.CalamityAttacks(target);
            if (RemnantOfTheAncientsMod.FargosSoulMod != null) attackModule.FargosAttacks(target);    
            
            InfernalTyrant_AuxiliaryModule.LifeSpeed(this);
            movementModule.UpdateMovement();
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
                if (auxModule.attackCounter <= 0)
                {
                    if (auxModule.IsEnraged) auxModule.attackCounterMaxValue = !Main.expertMode ? 600 : 700;
                    else if (IsPhase2) auxModule.attackCounterMaxValue = !Main.expertMode ? 650 : 750;
                    else auxModule.attackCounterMaxValue = !Main.expertMode ? 700 : 800;

                    auxModule.attackCounter = auxModule.attackCounterMaxValue;
                    NPC.netUpdate = true;
                }       
            }
            if (auxModule.attackCounter > 0) auxModule.attackCounter--;        
        }

       


        private float amplitud = 3.0f;
        private float frecuencia = 0.02f;

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
            if(DificultyUtils.InfernumMode) npcLoot.Add(RemnantDropRules.InfernumModeCommonDrop(ModContent.ItemType<Tyrant_Relic>()));
            else npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Tyrant_Relic>()));

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

            string textureBasePath = NPC.localAI[2] == 1 ? base.Texture+ "_alt" :  base.Texture;
            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{textureBasePath}_Eternity" : textureBasePath;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //TyranStats.DrawGlow(NPC, "InfernalTyrantBody");
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0) return;
            
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
        public override void AI()
        {
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.defense = TyranStats.TyrantArmor(999, NPC);
            GenericVariables gv = new GenericVariables();
            if (!GenericVariables.SizeChanged[1])
            {
                try
                {
                    if (NPC.localAI[2] == 1) NPC.Size = new(70, 66);
                    else NPC.Size = new(62, 66);
                  /*  if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.Size = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f);
                    }*/
                }
                catch (Exception)
                {
                    if (NPC.localAI[2] == 1) NPC.Size = new(70, 66);
                    else NPC.Size = new(62, 66);
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
                    RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,1f, 1f, 1f, 1f, 1f);
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
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0) { Hide = true };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            NPCID.Sets.ImmuneToRegularBuffs[Type] = true; 
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.lifeMax = BaseStats.LifeMax;
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity(); 
            NPC.Size = new(86, 176);  
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
            //TyranStats.DrawGlow(NPC, "InfernalTyrantTail");   
        }
        public override void Init()
        {
            InfernalTyrantHead.CommonWormInit(this);
        }
    }
}