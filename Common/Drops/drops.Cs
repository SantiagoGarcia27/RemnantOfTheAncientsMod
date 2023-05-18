using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.NPCs.zombis;
using RemnantOfTheAncientsMod.Content.NPCs;
using RemnantOfTheAncientsMod.World;
using Terraria.GameContent.ItemDropRules;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Alas;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.frozen_assaulter;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.pets;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using CalamityMod.Projectiles.Pets;

namespace RemnantOfTheAncientsMod.Common.Drops
{
    public class ModGlobalNPC : GlobalNPC
    {

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            PajarosLoot(npc, npcLoot);
            ArmoredZombiesLoot(npc, npcLoot);
            BossLoot(npc, npcLoot);
            LegendaryLoot(npc,npcLoot);
            CommonLoot(npc,npcLoot);
         
            if (npc.lifeMax > 1 && npc.lifeMax < 100000 && npc.value > 0f) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<The_Legion>(), 500000/ dificultyDrop(1), 1, 1));
            if (npc.lifeMax > 100000 && npc.value > 0f) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<The_Legion>(), 100000/ dificultyDrop(1), 1, 1));
          //  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fake_The_Legion>(), 1 / dificultyDrop(1), 1, 1));
            if (Reaper.ReaperMode) SoulLoot(npc,npcLoot,new Player());
        }
        public void CommonLoot(NPC npc ,NPCLoot npcLoot)
        {
            
            if (npc.type == NPCID.Shark) npcLoot.Add (ItemDropRule.Common(ModContent.ItemType<SharkTooth>(), 10/ dificultyDrop(1), 1, 5 ));
            if (npc.type == NPCID.Tim) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<magic_stick>(), 50/ dificultyDrop(5), 1, 1));
            if (npc.type == NPCID.DungeonGuardian) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TortugaPet>(), 10 / dificultyDrop(2), 1, 1));

        }
        public void PilarEnemiyLoot(NPC npc ,NPCLoot npcLoot)
        {
            if (npc.type == NPCID.SolarDrakomireRider) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.SolarSolenian) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.SolarDrakomire) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.SolarCrawltipedeHead) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.SolarCorite) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.LunarTowerSolar) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 10));
            if (npc.type == NPCID.VortexHornet) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.VortexHornetQueen) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.VortexLarva) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.VortexRifleman) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.VortexSoldier) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.LunarTowerVortex) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustCellBig) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustJellyfishBig) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustSoldier) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustSpiderBig) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustWormHead) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.LunarTowerStardust) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.NebulaBeast) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.NebulaBrain) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.NebulaHeadcrab) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.NebulaSoldier) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.LunarTowerNebula) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
        }
        public void LegendaryLoot(NPC npc,NPCLoot npcLoot)
        {
            if (npc.type == NPCID.MoonLordCore) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LaserMachineGun>(), 100/ dificultyDrop(1), 1, 1));
            if (npc.type == ModContent.NPCType<InfernalTyrantHead>()) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheSpiker>(), 200/ dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.Golem) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Legendary_Great_Sword>(), (800 / dificultyDrop(1)), 1, 1));
        }
        public void BossLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == ModContent.NPCType<DesertAniquilator>()) BossTrpoyMask(ModContent.ItemType<DesertTrophy>(), ModContent.ItemType<DesertAMask>(), npcLoot);
            if (npc.type == ModContent.NPCType<frozen_assaulter>()) BossTrpoyMask(ModContent.ItemType<FrostTrophy>(), ModContent.ItemType<FrozenMask>(), npcLoot);
            if (npc.type == ModContent.NPCType<InfernalTyrantHead>()) BossTrpoyMask(ModContent.ItemType<InfernalTrophy>(), ModContent.ItemType<InfernalMask>(), npcLoot);
            if (npc.type == NPCID.WallofFlesh) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TrowerEmblem>(), (3/ dificultyDrop(1)), 1, 1));
            if (npc.type == NPCID.CultistBoss) npcLoot.Add(ItemDropRule.BossBag(ItemID.CultistBossBag));    
        }
        public void PajarosLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.BirdRed || npc.type == NPCID.BirdBlue || npc.type == NPCID.Bird) npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BirdWings>(), 500/ dificultyDrop(1), 1, 1)); //dificultyDrop(1)/500
        }
        public void ArmoredZombiesLoot(NPC npc,NPCLoot npcLoot)
        {
            if (npc.type == ModContent.NPCType<Goldzombie>())  ZombieOreLoot(npcLoot,ItemID.GoldOre, ItemID.GoldBar);  
             
            if (npc.type == ModContent.NPCType<Platinumzombie>()) ZombieOreLoot(npcLoot, ItemID.PlatinumOre, ItemID.PlatinumBar);   

            if (npc.type == ModContent.NPCType<Copperzombie>()) ZombieOreLoot(npcLoot, ItemID.CopperOre, ItemID.CopperBar);

            if (npc.type == ModContent.NPCType<Ironzombie>()) ZombieOreLoot(npcLoot, ItemID.IronOre, ItemID.IronBar);

            if (npc.type == ModContent.NPCType<Leadzombie>()) ZombieOreLoot(npcLoot, ItemID.LeadOre, ItemID.LeadBar);

            if (npc.type == ModContent.NPCType<Silverzombie>()) ZombieOreLoot(npcLoot, ItemID.SilverOre, ItemID.SilverBar);

            if (npc.type == ModContent.NPCType<Tinzombie>()) ZombieOreLoot(npcLoot, ItemID.TinOre, ItemID.TinBar);

            if (npc.type == ModContent.NPCType<Tungstenzombie>()) ZombieOreLoot(npcLoot, ItemID.TungstenOre, ItemID.TungstenBar);
              
            if (npc.type == ModContent.NPCType<Tuxonitezombie>()) ZombieOreLoot(npcLoot, ModContent.ItemType<TuxoniteOre>(), ModContent.ItemType<TuxoniteBar>());
               
            if (npc.type == ModContent.NPCType<Shadowzombie>()) ZombieOreLoot(npcLoot, ItemID.DemoniteOre, ItemID.DemoniteBar);
               
            if (npc.type == ModContent.NPCType<Crimsonzombi>()) ZombieOreLoot(npcLoot, ItemID.CrimtaneOre, ItemID.CrimtaneBar);

            if (npc.type == ModContent.NPCType<ReinforcedIronZombie>()) ZombieOreLoot(npcLoot, ModContent.ItemType<Reinforced_iron_ore>(), ModContent.ItemType<Reinforced_ironBar>());
            if (npc.type == ModContent.NPCType<MoltenZombie>()) ZombieOreLoot(npcLoot,ItemID.Hellstone,ItemID.HellstoneBar);
            if (npc.type == ModContent.NPCType<NightZombie>())
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.JungleSpores, 8 / dificultyDrop(1), 1, 1));
                npcLoot.Add(ItemDropRule.Common(ItemID.Stinger, 8 / dificultyDrop(1), 1, 1));
                npcLoot.Add(ItemDropRule.Common(ItemID.Obsidian, 8 / dificultyDrop(1), 1, 1));
                npcLoot.Add(ItemDropRule.Common(ItemID.Hellstone, 8 / dificultyDrop(1), 1, 1));
                npcLoot.Add(ItemDropRule.Common(ItemID.Bone, 8 / dificultyDrop(1), 1, 1));
                npcLoot.Add(ItemDropRule.Common(ItemID.Cobweb, 8 / dificultyDrop(1), 1, 1));
                npcLoot.Add(ItemDropRule.Common(ItemID.Stinger, 8 / dificultyDrop(1), 1, 1));
                int corruptOre = WorldGen.crimson ? ItemID.DemoniteOre : ItemID.CrimtaneOre;
                npcLoot.Add(ItemDropRule.Common(corruptOre, 8 / dificultyDrop(1), 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NightBar>(), 50 / dificultyDrop(1), 1, 1));
            }
            if (npc.type == ModContent.NPCType<ArmoredSlime>())
            {
                ZombieOreLoot(npcLoot, ModContent.ItemType<Reinforced_iron_ore>(), ModContent.ItemType<Reinforced_ironBar>());
                ItemDropRule.Common(ItemID.Gel, 1, 10, 15);
            }
            if (npc.type == ModContent.NPCType<CobaltZombie>()) ZombieOreLoot(npcLoot, ItemID.CobaltOre, ItemID.CobaltBar);
            if (npc.type == ModContent.NPCType<PaladiumZombie>()) ZombieOreLoot(npcLoot, ItemID.PalladiumOre, ItemID.PalladiumBar);
        }
        public override void OnKill(NPC npc)
        {
        }
        public void SoulLoot(NPC npc,NPCLoot npcLoot,Player player)
        {
            
            if (npc.boss)
            {
                if (npc.type == NPCID.Skeleton && !Main.LocalPlayer.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<SkeletonSoul>(), 1, 1, 1);

                if (npc.type == ModContent.NPCType<DesertAniquilator>() && !Main.LocalPlayer.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<DesertSoul>(), 1, 1, 1);

                if (npc.type == NPCID.WallofFlesh && !Main.LocalPlayer.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<FleshSoul>(), 1, 1, 1);

                if (npc.type == ModContent.NPCType<frozen_assaulter>() && !Main.LocalPlayer.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<FrozenSoul>(), 1, 1, 1);

                if (npc.type == NPCID.TheDestroyer && !Main.LocalPlayer.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<DestroyerEnergyCell>(), 1, 1, 1);

                if (npc.type == NPCID.SkeletronPrime && !Main.LocalPlayer.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<SkeletronPrimeEnergyCell>(), 1, 1, 1);

                if (npc.type == NPCID.Spazmatism && !Main.LocalPlayer.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<SpazmatismEnergyCell>(), 1, 1, 1);

                if (npc.type == NPCID.Retinazer && !Main.LocalPlayer.GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<RetinazorEnergyCell>(), 1, 1, 1);

                if (npc.type == NPCID.Plantera && !Main.LocalPlayer.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<PlantSoul>(), 1, 1, 1);

                if (npc.type == ModContent.NPCType<InfernalTyrantHead>() && !Main.LocalPlayer.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<InfernalSoul>(), 1, 1, 1);

                if (npc.type == NPCID.Golem && !Main.LocalPlayer.GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<GolemSoul>(), 1, 1, 1);

                if (npc.type == NPCID.DukeFishron && !Main.LocalPlayer.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<FishSoul>(), 1, 1, 1);

                if (npc.type == NPCID.CultistBoss && !Main.LocalPlayer.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<CultistSoul>(), 1, 1, 1);

                if (npc.type == NPCID.MoonLordCore && !Main.LocalPlayer.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade) ItemDropRule.Common(ModContent.ItemType<MoonSoul>(), 1, 1, 1);
            }
        }
        
        public void ZombieOreLoot(NPCLoot npcLoot,int ore, int bar)
        {
            npcLoot.Add(ItemDropRule.Common(ore, 1/ dificultyDrop(1), 1, 1));
            npcLoot.Add(ItemDropRule.Common(bar, 50/ dificultyDrop(1), 1, 1));
        }
        public void BossTrpoyMask(int Tropy, int Mask, NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Tropy, 10, 1, 1, dificultyDrop(1)));
            npcLoot.Add(new CommonDrop(Mask, 7, 1, 1, dificultyDrop(1)));
        }
        public int dificultyDrop(int i)
        {
            int a = i;
            if (Reaper.ReaperMode) a = i * 5;
            return a;
        }
     
    }
}  
        
