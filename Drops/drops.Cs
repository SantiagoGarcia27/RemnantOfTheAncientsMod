using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.NPCs.ITyrant;
using RemnantOfTheAncientsMod.NPCs.frozen_assaulter;
using RemnantOfTheAncientsMod.NPCs.DAniquilator;
using RemnantOfTheAncientsMod.NPCs.zombis;
using RemnantOfTheAncientsMod.NPCs;
using RemnantOfTheAncientsMod.World;
using Terraria.GameContent.ItemDropRules;
using RemnantOfTheAncientsMod.Items.Bloques;
using RemnantOfTheAncientsMod.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Items.accesorios.Alas;
using RemnantOfTheAncientsMod.Items.Items;
using RemnantOfTheAncientsMod.Items.Fmode;
using RemnantOfTheAncientsMod.Items.accesorios;
using RemnantOfTheAncientsMod.Items.pets;
using RemnantOfTheAncientsMod.Items.Magic;
using RemnantOfTheAncientsMod.Items.Mele;


namespace opswordsII.Drops
{
    public class ModGlobalNPC : GlobalNPC
    {
     
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            PajarosLoot(npc);
            ArmoredZombiesLoot(npc);
            BossLoot(npc);
            LegendaryLoot(npc);
            CommonLoot(npc);

            if (npc.lifeMax > 1 && npc.lifeMax < 100000 && npc.value > 0f) new CommonDrop(ModContent.ItemType<The_Legion>(), 500000, 1, 1, dificultyDrop(1));
            if (npc.lifeMax > 100000 && npc.value > 0f) new CommonDrop(ModContent.ItemType<The_Legion>(), 100000, 1, 1, dificultyDrop(1));
            if (Reaper.ReaperMode) SoulLoot(npc);
        }
        public void CommonLoot(NPC npc)
        {
            if (npc.type == NPCID.Shark) new CommonDrop(ModContent.ItemType<SharkTooth>(), 10, 1, 5, dificultyDrop(1));
            if (npc.type == NPCID.Tim) new CommonDrop(ModContent.ItemType<magic_stick>(), 109, 1, 1, dificultyDrop(50));
            if (npc.type == NPCID.DungeonGuardian) new CommonDrop(ModContent.ItemType<TortugaPet>(), 10, 1, 1, dificultyDrop(2));
        }
        public void LegendaryLoot(NPC npc)
        {
            if (npc.type == NPCID.MoonLordCore) new CommonDrop(ModContent.ItemType<LaserMachineGun>(), 100, 1, 1, dificultyDrop(1));
            if (npc.type == ModContent.NPCType<InfernalTyrantHead>()) new CommonDrop(ModContent.ItemType<TheSpiker>(), 200, 1, 1, dificultyDrop(1));
            if (npc.type == NPCID.Golem) new CommonDrop(ModContent.ItemType<Legendary_Great_Sword>(), 800, 1, 1, dificultyDrop(1));
        }
        public void BossLoot(NPC npc)
        {
            if (npc.type == ModContent.NPCType<DesertAniquilator>()) BossTrpoyMask(ModContent.ItemType<DesertTrophy>(), ModContent.ItemType<DesertAMask>());
            if (npc.type == ModContent.NPCType<frozen_assaulter>()) BossTrpoyMask(ModContent.ItemType<FrostTrophy>(), ModContent.ItemType<FrozenMask>());
            if (npc.type == ModContent.NPCType<InfernalTyrantHead>()) BossTrpoyMask(ModContent.ItemType<InfernalTrophy>(), ModContent.ItemType<InfernalMask>());
            if (npc.type == NPCID.WallofFlesh) new CommonDrop(ModContent.ItemType<TrowerEmblem>(), 3, 1, 1, dificultyDrop(1));
        }
        public void PajarosLoot(NPC npc)
        {
            if (npc.type == NPCID.BirdRed || npc.type == NPCID.BirdBlue || npc.type == NPCID.Bird) new CommonDrop(ModContent.ItemType<BirdWings>(), 500, 1, 1, dificultyDrop(1)); //dificultyDrop(1)/500
        }
        public void ArmoredZombiesLoot(NPC npc)
        {
            if (npc.type == ModContent.NPCType<Goldzombie>()) ZombieOreLoot(ItemID.GoldOre, ItemID.GoldBar); 
             
            if (npc.type == ModContent.NPCType<Platinumzombie>()) ZombieOreLoot(ItemID.PlatinumOre, ItemID.PlatinumBar);   

            if (npc.type == ModContent.NPCType<Copperzombie>()) ZombieOreLoot(ItemID.CopperOre, ItemID.CopperBar);

            if (npc.type == ModContent.NPCType<Ironzombie>()) ZombieOreLoot(ItemID.IronOre, ItemID.IronBar);

            if (npc.type == ModContent.NPCType<Leadzombie>()) ZombieOreLoot(ItemID.LeadOre, ItemID.LeadBar);

            if (npc.type == ModContent.NPCType<Silverzombie>()) ZombieOreLoot(ItemID.SilverOre, ItemID.SilverBar);

            if (npc.type == ModContent.NPCType<Tinzombie>()) ZombieOreLoot(ItemID.TinOre, ItemID.TinBar);

            if (npc.type == ModContent.NPCType<Tungstenzombie>()) ZombieOreLoot(ItemID.TungstenOre, ItemID.TungstenBar);
              
            if (npc.type == ModContent.NPCType<Tuxonitezombie>()) ZombieOreLoot(ModContent.ItemType<TuxoniteOre>(), ModContent.ItemType<TuxoniteBar>());
               
            if (npc.type == ModContent.NPCType<Shadowzombie>()) ZombieOreLoot(ItemID.DemoniteOre, ItemID.DemoniteBar);
               
            if (npc.type == ModContent.NPCType<Crimsonzombi>()) ZombieOreLoot(ItemID.CrimtaneOre, ItemID.CrimtaneBar);

            if (npc.type == ModContent.NPCType<Armoredzombie>()) ZombieOreLoot(ModContent.ItemType<Reinforced_iron_ore>(), ModContent.ItemType<Reinforced_ironBar>());
             
            if (npc.type == ModContent.NPCType<ArmoredSlime>())
            {
                ZombieOreLoot(ModContent.ItemType<Reinforced_iron_ore>(), ModContent.ItemType<Reinforced_ironBar>());
                ItemDropRule.Common(ItemID.Gel, 1, 10, 15);
            }
        }
        public void SoulLoot(NPC npc)
        {
            if (npc.boss)
            {
                if (npc.type == NPCID.KingSlime && !new Player().GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<KingSoul>(), 1, 1, 1);

                if (npc.type == NPCID.EyeofCthulhu && !new Player().GetModPlayer<EyeReaperSoulPlayer>().EyeeReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<EyeSoul>(), 1, 1, 1);

                if ((npc.type == NPCID.BrainofCthulhu || (npc.type >= NPCID.EaterofWorldsHead && npc.type <= NPCID.EaterofWorldsTail)) && !new Player().GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<CorruptSoul>(), 1, 1, 1);

                if (npc.type == NPCID.QueenBee && !new Player().GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<BeeSoul>(), 1, 1, 1);

                if (npc.type == NPCID.Skeleton && !new Player().GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<SkeletonSoul>(), 1, 1, 1);

                if (npc.type == ModContent.NPCType<DesertAniquilator>() && !new Player().GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<DesertSoul>(), 1, 1, 1);

                if (npc.type == NPCID.WallofFlesh && !new Player().GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<FleshSoul>(), 1, 1, 1);

                if (npc.type == ModContent.NPCType<frozen_assaulter>() && !new Player().GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<FrozenSoul>(), 1, 1, 1);

                if (npc.type == NPCID.TheDestroyer && !new Player().GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<DestroyerEnergyCell>(), 1, 1, 1);

                if (npc.type == NPCID.SkeletronPrime && !new Player().GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<SkeletronPrimeEnergyCell>(), 1, 1, 1);

                if (npc.type == NPCID.Spazmatism && !new Player().GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<SpazmatismEnergyCell>(), 1, 1, 1);

                if (npc.type == NPCID.Retinazer && !new Player().GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<RetinazorEnergyCell>(), 1, 1, 1);

                if (npc.type == NPCID.Plantera && !new Player().GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<PlantSoul>(), 1, 1, 1);

                if (npc.type == ModContent.NPCType<InfernalTyrantHead>() && !new Player().GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<InfernalSoul>(), 1, 1, 1);

                if (npc.type == NPCID.Golem && !new Player().GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<GolemSoul>(), 1, 1, 1);

                if (npc.type == NPCID.DukeFishron && !new Player().GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<FishSoul>(), 1, 1, 1);

                if (npc.type == NPCID.CultistBoss && !new Player().GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<CultistSoul>(), 1, 1, 1);

                if (npc.type == NPCID.MoonLordCore && !new Player().GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<MoonSoul>(), 1, 1, 1);
            }
        }
        
        public void ZombieOreLoot(int ore, int bar)
        {
            new CommonDrop(ore, 1, 1, 1, dificultyDrop(1));
            new CommonDrop(bar, 50, 1, 1, dificultyDrop(1));
        }
        public void BossTrpoyMask(int Tropy, int Mask)
        {
            new CommonDrop(Tropy, 10, 1, 1, dificultyDrop(1));
            new CommonDrop(Mask, 7, 1, 1, dificultyDrop(1));
        }
        public int dificultyDrop(int i)
        {
            int a = i;
            if (Reaper.ReaperMode) a = i * 5;
            return a;
        }
     
    }
}  
        
