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
using RemnantOfTheAncientsMod.World;
using SangarUtilities.Common.UtilsTweaks;
using System;
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
            Main.npcFrameCount[NPC.type] = 6;
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

        Player CurrentTarget => auxiliaryModule.CurrentTarget;
        private bool BossActive => !CurrentTarget.dead && CurrentTarget.active;
        Point NpcFloor => Utils.ToTileCoordinates(NPC.Center);
      
        DesertAnnihilator_Intro introModule = new();
        DesertAnnihilator_Movment movmentModule = new();
        DesertAnnihilator_Attack attackModule = new();
        DesertAnnihilator_Aux auxiliaryModule = new();
        DesertAnnihilator_Animation animationModule = new();

        public bool spawnGuardians = true;
        public override void AI()
        {

            NPC.TargetClosest(true);

            animationModule.UpdateScale();
            animationModule.UpdateAnimation();

            NPC.ai[0]++;

            if (introModule.NoAI) introModule.SpawnAnimationAI();
            
            if (CurrentTarget == null || introModule.NoAI) return;

            CheckRage(); 
            CheckForCheating();
            movmentModule.MovementAI();

            if (Main.netMode != NetmodeID.MultiplayerClient) attackModule.AttackIA(NPC, CurrentTarget);

            if (CurrentTarget.dead)
            {
                NPC.EncourageDespawn(7);
                DespawnBoss();
            }

            if (RemnantOfTheAncientsMod.FargosSoulMod != null) attackModule.EternityIA(NPC, CurrentTarget);
        }

        void CheckRage()
        {
            bool rage = BossActive && !CurrentTarget.ZoneDesert && !CurrentTarget.ZoneUndergroundDesert;
            if (auxiliaryModule.BossIsInRage != rage) auxiliaryModule.BossIsInRage = rage;
        }

        #region Anticheat

        private readonly int ForceTpDistance = 150.ToCoordinatePosition(); // 110 tiles in pixels
        void CheckForCheating()
        {
            if (CurrentTarget == null || !CurrentTarget.active || CurrentTarget.dead) return;

            float distanceToTarget = NPC.Distance(CurrentTarget.Center);
            if (distanceToTarget >= ForceTpDistance && !Reaper.ReaperMode)
            {
                attackModule.GenerateTpParticles();
                NPC.velocity = Vector2.Zero;
                attackModule.DesertTp();
            }

            if (Main.tile[NpcFloor.X, NpcFloor.Y + 1].LiquidAmount > 0)
            {
                Point PlayerFloor = Utils.ToTileCoordinates(CurrentTarget.Center);
                if (Main.tile[PlayerFloor.X, PlayerFloor.Y + 1].LiquidAmount == 0)
                {
                    attackModule.GenerateTpParticles();
                    attackModule.DesertTp();
                }
            }
        }

        #endregion

        #region Attacks

       
       
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
            if (DificultyUtils.InfernumMode) InfernumHitEffect();
            
            int choice = Main.rand.Next(2, 8);
            if (Reaper.ReaperMode || auxiliaryModule.BossIsInRage) choice *= 2;
            
            if (!Main.rand.NextBool(5)) return;

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
      
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(introModule.NoAI);
            writer.Write(introModule.ScreenAnimationTimer);
            writer.Write(introModule.SpawnerAnimationTimer);

            writer.Write((byte)animationModule.CurrentTexture);
            writer.Write(auxiliaryModule.BossIsInRage);
            writer.Write(spawnGuardians);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            introModule.NoAI = reader.ReadBoolean();
            introModule.ScreenAnimationTimer = reader.ReadSingle();
            introModule.SpawnerAnimationTimer = reader.ReadSingle();

            animationModule.CurrentTexture = (DesertAnnihilator_Animation.TextureType)reader.ReadByte();

            // Idealmente asignar un campo interno, sin disparar sonido.
            auxiliaryModule.BossIsInRage = reader.ReadBoolean();
            spawnGuardians = reader.ReadBoolean();
        }

        int despawnCounter = 0;
        int despawnCounterMax = Utils1.FormatTimeToTick(0, 0, 0, 2);
        public void DespawnBoss()
        {
            NPC.velocity = new Vector2(0,3);
            NPC.EncourageDespawn(7);
            if (despawnCounter > Utils1.FormatTimeToTick(0, 0, 0, 0.3f)) attackModule.GenerateTpParticles();
            if (despawnCounter++ >= despawnCounterMax) NPC.Center = new(0,0);
            return;
        }

        public override bool? CanFallThroughPlatforms() => false;

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.rand.NextBool(3)) target.AddBuff(BuffType<Burning_Sand>(), 100, true);
        }
        
        
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            string a = base.Texture;
            return animationModule.PreDraw(spriteBatch, screenPos, drawColor);       
        }

        
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            animationModule.PostDraw(spriteBatch, screenPos, drawColor);
            base.PostDraw(spriteBatch, screenPos, drawColor);
        }

        
        public override void OnSpawn(IEntitySource source)
        {
            auxiliaryModule.npc = NPC;
            attackModule.npc = NPC;
            attackModule.auxiliaryModule = auxiliaryModule;
            attackModule.animationModule = animationModule;
            movmentModule.npc = NPC;
            introModule.npc = NPC;
            animationModule.npc = NPC;


            introModule.ScreenAnimationTimer = Utils1.FormatTimeToTick(0, 0, 0, 5);
            animationModule.setOriginalSize(NPC.Size);

            spawnGuardians = true;
            NPC.alpha = 255;
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