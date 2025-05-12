using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Rep;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
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
using RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using CalamityMod;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.World;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using SangarUtilities.Common;
using SangarUtilities.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant
{


    [AutoloadBossHead]
    public class InfernalTyrantHead : WormHead
    {
        public override int BodyType => ModContent.NPCType<InfernalTyrantBody>();

        public override int TailType => ModContent.NPCType<InfernalTyrantTail>();

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
            if (RemnantOfTheAncientsMod.CalamityMod != null) SetDefautsCalamity();
            NPC.damage = 200;
            NPC.defense = TyranStats.TyrantArmor(999, NPC);
            NPC.npcSlots = 20f;
            NPC.lavaImmune = true;
            
            Music = MusicLoader.GetMusicSlot(Mod, "Content/Sounds/Music/Infernal_Tyrant");
        }
        [JITWhenModsEnabled("CalamityMod")]
        public void SetDefautsCalamity()
        {

            NPC.Calamity().canBreakPlayerDefense = true;
            RemnantGlobalNPC.SetNpcDamageReductionCalamity(NPC,0.02f,0.22f,0.29f,0.3f,0.5f);
            InfernalTyrantAuxiliaryClass.CalamityLifeScale(NPC,BaseStats.LifeMax);
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
        internal static void CommonWormInit(Worm worm)
        {
            worm.MoveSpeed = 30f;
            worm.Acceleration = 0.245f;
        }

        private int attackCounter;
        private int attackCounterMaxValue = 800;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
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
        public override void AI()
        {
            NPC.buffImmune[BuffID.OnFire] = true;
           
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
            DoAttacks(target);


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
                            FlameVelocity.RotatedBy(i * 20);
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

            InfernalTyrantAuxiliaryClass.LifeSpeed(this);
        }
  
        public override void OnSpawn(IEntitySource source)
        {
            GenericVariables.IsSpawned = false;
            GenericVariables.SpawnCounter = 0;
        }
        private void DoAttacks(Player target)
        {
            switch (attackCounter)
            {
                case 200:
                    SpikeIa((int)(40 * RemnantGlobalNPC.DamageBonus), false, -3, -3);
                    break;
                case 250:
                    FireBallIa(12f, (int)(70 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "*", 4, 3, target, 0f);
                    FireBallIa(12f, (int)(70 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBallF>(), "/", 4, 3, target, 0f);
                    break;
                case 300:
                    SummonIa(NPCID.Demon);
                    break;
                case 400:
                    SpikeIa((int)(90 * RemnantGlobalNPC.DamageBonus), true, 3, -3);
                    break;
                case 430:
                    SpikeIa((int)(40 * RemnantGlobalNPC.DamageBonus), false, 3, -3);
                    break;
                case 600:
                    for (int i = -2; i <= 3; i++)
                        FireBallIa(12f, (int)(30 * RemnantGlobalNPC.DamageBonus), ModContent.ProjectileType<InfernalBall>(), "*", i - 1, i + 2, target, 0);
                    break;
                case 700:
                    if (RemnantOfTheAncientsMod.InfernumMod != null)
                    {
                        if (DificultyUtils.InfernumMode)
                        {
                            for (int i = 0; i <= 10; i++)
                            {
                                TornadoIa((int)(70 * RemnantGlobalNPC.DamageBonus), CallUtils.GetProjectileFromMod(RemnantOfTheAncientsMod.CalamityMod, "Flarenado"), target, i);
                            }
                           // FireBallIa(12f, (int)(70 * RemnantGlobalNPC.DamageBonus), CallUtils.Get<ModProjectile>(RemnantOfTheAncients.CalamityMod, "Flarenado"), "*", 4, 3, target, 0f);
                        }
                    }
                    SummonIa(NPCID.RedDevil);
                    break;
            }
        }
        public void UpdateCounters(Player target)
        {

            int distance = (int)Vector2.Distance(NPC.Center, target.Center);

            if (distance < 200 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
            {
                if (attackCounter <= 0)
                {
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

        public void FindTarget()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                if (InfernalTyrantAuxiliaryClass.AllPlayersDead())
                {
                    NPC.TargetClosest(true);
                    NPC.velocity *= 0.1f;
                }
            }
        } 
        public void DespawnSafeCheck(Player player, Worm worm)
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
        public void FireBallIa(float Speed, int damage, int type, string signo1, int cordx, int cordy, Player player, float grades)
        {
            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width * cordx), NPC.position.Y + (NPC.height * cordy));
            if (signo1 == "/")
                vector8 = new Vector2(NPC.position.X + (NPC.width / cordx), NPC.position.Y + (NPC.height / cordy));

           // int type = ProjectileType<InfernalBallF>();
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);

            Vector2 direction = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
            direction = direction.RotatedBy(grades);

            //float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8, direction * 12, type, damage, 0f, 0);
            Main.projectile[projectile].timeLeft = 300;
        }
        public void TornadoIa(int damage, int type, Player player, int i)
        {
            Vector2 spawn = DistanceUtils.SearchLiquidCoordenates(LiquidID.Lava, 10, player);
            bool m = false;
            Vector2 fixedSpawn = new(spawn.X, DistanceUtils.CheckHigh(spawn));
          
            

            // player.position = spawn;
            Vector2 ppos = player.position;

            Vector2 position =  new(player.Center.X + (40 *16f), fixedSpawn.Y -i *16 *4);
           

            // int type = ProjectileType<InfernalBallF>();
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
            //float rotation = (float)Math.Atan2(vector8.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), vector8.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), position, Vector2.Zero, type, damage, 0f, player.whoAmI,1);
            Main.projectile[projectile].timeLeft = 1300;
        }
        public void SpikeIa(int damage, bool isStrong, int cordx, int cordy)
        {
            int type = isStrong ? ModContent.ProjectileType<InfernalSpikeF>() : ModContent.ProjectileType<InfernalSpike>();
            Vector2 spawnPosition = NPC.Center + new Vector2(cordx * NPC.width, cordy * NPC.height);
            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.Center);
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(),spawnPosition, Vector2.Zero, type, damage, 0f, Main.myPlayer);
            Main.projectile[projectile].timeLeft = 1500;
        }
      

        public void SummonIa(int Npc) => NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, Npc);
       
     
       
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
            string fargos = DificultyUtils.EternityMode || DificultyUtils.MasochistMode ? $"{base.Texture}_Eternity" : base.Texture;
            Texture2D Texture = (Texture2D)ModContent.Request<Texture2D>(fargos);
            Main.EntitySpriteDraw(Texture, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f), NPC.scale, SpriteEffects.None, 0);
            return false;
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
            npcLoot.Add(ItemDropRule.Common(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EssenceofChaos"), 1, 5, Utils1.ReaperDropScaler(15)));
        }
    }

    internal class InfernalTyrantBody : WormBody
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
            InfernalTyrantAuxiliaryClass.CalamityLifeScale(NPC,BaseStats.LifeMax);
        }
       
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
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
                    for(int i = 1; i >= 3; i++) 
                    {
                        Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), Mod.Find<ModGore>("InfernalTyrantBodyGore"+i).Type, NPC.scale);
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

    internal class InfernalTyrantTail : WormTail
    {
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
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
            InfernalTyrantAuxiliaryClass.CalamityLifeScale(NPC,BaseStats.LifeMax);
        }

        public override void AI()
        {
            NPC.buffImmune[BuffID.OnFire] = true;
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

    public static class TyranStats 
    {
        public static int TyrantArmor(int i, NPC npc)
        {
            if (Main.expertMode || Main.masterMode)
            {
                if (npc.life > npc.life / 10)
                {
                    i /= 3;
                }
                else if (npc.life > npc.life / 15 && Reaper.ReaperMode)
                {
                    i = 0;
                }
            }
            else if (npc.life > npc.life / 4)
            {
                i /= 2;
            }

            int a = RemnantOfTheAncientsMod.CalamityMod != null ? i * 2 : i;
            return a;
        }
       

        public static void DrawGlow(NPC npc,string NpcName)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vectorFrame = new(TextureAssets.Npc[npc.type].Value.Width / 2, TextureAssets.Npc[npc.type].Value.Height / Main.npcFrameCount[npc.type] / 2);
            Vector2 position = new Vector2(npc.Center.X, npc.Center.Y) - Main.screenPosition;
            var a = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/"+ NpcName + "_Glow");
            position -= new Vector2(a.Width(), a.Height() / Main.npcFrameCount[npc.type]) / 2f;
            position += vectorFrame * 1f + new Vector2(0f, 4f + npc.gfxOffY);
            Color color = Utils.MultiplyRGBA(new Color(127 - npc.alpha, 127 - npc.alpha, 127 - npc.alpha, 0), Color.LightYellow);
            Main.spriteBatch.Draw((Texture2D)a, position, npc.frame, color, npc.rotation, vectorFrame, npc.scale, effects, 0f);
        }
    }
}