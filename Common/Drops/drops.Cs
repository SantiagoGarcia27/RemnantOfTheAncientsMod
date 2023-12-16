using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.NPCs.zombis;
using RemnantOfTheAncientsMod.Content.NPCs;
using Terraria.GameContent.ItemDropRules;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Alas;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.pets;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag;
//using CalamityMod;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics.Infernum;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using CalamityMod;

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
            PilarEnemiyLoot(npc, npcLoot);
            HallowBiomeLoot(npc, npcLoot);
            CorruptBiomeLoot(npc, npcLoot);
            CrimsomBiomeLoot(npc,npcLoot);
            if (RemnantOfTheAncientsMod.InfernumMod != null)
            {
                InfernumLoot(npc, npcLoot);
            }
            if(RemnantOfTheAncientsMod.FargosSoulMod != null)
            {
                FargosLoot(npc, npcLoot);
            }
            if (npc.value > 0f)
            {
                if (npc.lifeMax > 100000)
                {
                    npcLoot.Add(ItemDropRule.Common(ItemType<The_Legion>(), 100000 / dificultyDrop(1), 1, 1));
                }
                else
                {
                    npcLoot.Add(ItemDropRule.Common(ItemType<The_Legion>(), 500000 / dificultyDrop(1), 1, 1));
                }
            }
          //  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fake_The_Legion>(), 1 / dificultyDrop(1), 1, 1));
            SoulLoot(npc,npcLoot,new Player());
        }
        public void CommonLoot(NPC npc ,NPCLoot npcLoot)
        {
            
            if (npc.type == NPCID.Shark) npcLoot.Add (ItemDropRule.Common(ItemType<SharkTooth>(), 10/ dificultyDrop(1), 1, 5 ));
            if (npc.type == NPCID.Tim) npcLoot.Add(ItemDropRule.Common(ItemType<magic_stick>(), 50/ dificultyDrop(5), 1, 1));
            if (npc.type == NPCID.DungeonGuardian) npcLoot.Add(ItemDropRule.Common(ItemType<TortugaPet>(), 10 / dificultyDrop(2), 1, 1));
            if (npc.type == NPCID.BigMimicCorruption) npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.DownedPlantera(), ItemID.ScourgeoftheCorruptor, 4, 1, 1));
            if (npc.type == NPCID.BigMimicCrimson) npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.DownedPlantera(), ItemID.VampireKnives, 4, 1, 1));
            if (npc.type == NPCID.BigMimicHallow) npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.DownedPlantera(), ItemID.RainbowGun, 4, 1, 1));
            if (npc.type == NPCID.BigMimicJungle) npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.DownedPlantera(), ItemID.PiranhaGun, 4, 1, 1));
        }
        public void HallowBiomeLoot(NPC npc, NPCLoot npcLoot)
        {
            if(npc.type == NPCID.IlluminantBat) npcLoot.Add(ItemDropRule.Common(ItemID.LightShard, 50, 1,1));
            if (npc.type == NPCID.ChaosElemental) npcLoot.Add(ItemDropRule.Common(ItemID.LightShard, 50, 1, 1));
            if (npc.type == NPCID.EnchantedSword) npcLoot.Add(ItemDropRule.Common(ItemID.LightShard, 50, 1, 1));
            if (npc.type == NPCID.PigronHallow) npcLoot.Add(ItemDropRule.Common(ItemID.LightShard, 10, 1, 1));
        }
        public void CorruptBiomeLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.CursedHammer) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 50, 1, 1));
            if (npc.type == NPCID.PigronCorruption) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 10, 1, 1));
            if (npc.type == NPCID.Clinger) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 50, 1, 1));
        }
        public void CrimsomBiomeLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.CrimsonAxe) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 100, 1, 1));
            if (npc.type == NPCID.PigronCrimson) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 10, 1, 1));
            if (npc.type == NPCID.FloatyGross) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 50, 1, 1));
            if (npc.type == NPCID.IchorSticker) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 50, 1, 1));
        }
        public void PilarEnemiyLoot(NPC npc ,NPCLoot npcLoot)
        {
            if (npc.type == NPCID.SolarDrakomireRider) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.SolarSolenian) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.SolarDrakomire) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.SolarCrawltipedeHead) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.SolarCorite) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.LunarTowerSolar) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 10));
            if (npc.type == NPCID.VortexHornet) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.VortexHornetQueen) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.VortexLarva) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.VortexRifleman) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.VortexSoldier) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.LunarTowerVortex) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustCellBig) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustJellyfishBig) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustSoldier) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustSpiderBig) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.StardustWormHead) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.LunarTowerStardust) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.NebulaBeast) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.NebulaBrain) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.NebulaHeadcrab) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.NebulaSoldier) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.LunarTowerNebula) npcLoot.Add(ItemDropRule.Common(ItemType<Neutrum_Fragment>(), 3 / dificultyDrop(1), 1, 1));
        }
        public void LegendaryLoot(NPC npc,NPCLoot npcLoot)
        {
            if (npc.type == NPCID.MoonLordCore) npcLoot.Add(ItemDropRule.Common(ItemType<LaserMachineGun>(), 100/ dificultyDrop(1), 1, 1));
            if (npc.type == NPCType<InfernalTyrantHead>()) npcLoot.Add(ItemDropRule.Common(ItemType<TheSpiker>(), 200/ dificultyDrop(1), 1, 1));
            if (npc.type == NPCID.Golem) npcLoot.Add(ItemDropRule.Common(ItemType<Legendary_Great_Sword>(), (800 / dificultyDrop(1)), 1, 1));
        }
        public void BossLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCType<DesertAniquilator>()) BossTrpoyMask(ItemType<DesertTrophy>(), ItemType<DesertAMask>(), npcLoot);
            if (npc.type == NPCType<FrozenAssaulter>()) BossTrpoyMask(ItemType<FrostTrophy>(), ItemType<FrozenMask>(), npcLoot);
            if (npc.type == NPCType<InfernalTyrantHead>()) BossTrpoyMask(ItemType<InfernalTrophy>(), ItemType<InfernalMask>(), npcLoot);
            if (npc.type == NPCID.WallofFlesh) npcLoot.Add(ItemDropRule.Common(ItemType<TrowerEmblem>(), (3/ dificultyDrop(1)), 1, 1));
            if (npc.type == NPCID.CultistBoss) npcLoot.Add(ItemDropRule.BossBag(ItemID.CultistBossBag));
            if (npc.type == NPCID.HallowBoss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.IsDay(), ModContent.ItemType<StarChunk>(), 1, 3, 5));
                npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.IsNight(), ItemType<StarChunk>(), 1, 1, 2));
            }
            if (npc.type == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.StrangePlant1,1, dificultyDrop(1), dificultyDrop(1)));
            }
        }
        public void PajarosLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.BirdRed || npc.type == NPCID.BirdBlue || npc.type == NPCID.Bird) npcLoot.Add(ItemDropRule.Common(ItemType<BirdWings>(), 100/ dificultyDrop(1), 1, 1)); //dificultyDrop(1)/500
        }
        [JITWhenModsEnabled("InfernumMode")]
        public void InfernumLoot(NPC npc,NPCLoot npcLoot)
        {
            bool Infernum = DificultyUtils.InfernumMode;

            if (npc.type == ModContent.NPCType<DesertAniquilator>())
                npcLoot.AddIf(() => DificultyUtils.InfernumMode, ModContent.ItemType<Desert_Relic_Infernum>());
            else if (npc.type == ModContent.NPCType<FrozenAssaulter>())
                npcLoot.AddIf(() => DificultyUtils.InfernumMode, ModContent.ItemType<Frozen_Relic_Infernum>());
            else if (npc.type == ModContent.NPCType<InfernalTyrantHead>())
                npcLoot.AddIf(() => DificultyUtils.InfernumMode, ModContent.ItemType<Tyrant_Relic_Infernum>());
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public void FargosLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == ModContent.NPCType<DesertAniquilator>())
            {
                npcLoot.Add(RemnantDropRules.CommonDropOnAllPlayersWithConditionsNoAllActive(new() { new RemnantConditions.IsEternity(), new RemnantConditions.IsMasochist() }, ModContent.ItemType<DesertMedalion>()));
            }
            if (npc.type == ModContent.NPCType<FrozenAssaulter>())
            {
                npcLoot.Add(RemnantDropRules.CommonDropOnAllPlayersWithConditionsNoAllActive(new() { new RemnantConditions.IsEternity(), new RemnantConditions.IsMasochist() }, ModContent.ItemType<FrostBarrier>()));
            }
        }
        public void ArmoredZombiesLoot(NPC npc,NPCLoot npcLoot)
        {
            if (npc.type == NPCType<Goldzombie>())  ZombieOreLoot(npcLoot,ItemID.GoldOre, ItemID.GoldBar);  
             
            if (npc.type == NPCType<Platinumzombie>()) ZombieOreLoot(npcLoot, ItemID.PlatinumOre, ItemID.PlatinumBar);   

            if (npc.type == NPCType<Copperzombie>()) ZombieOreLoot(npcLoot, ItemID.CopperOre, ItemID.CopperBar);

            if (npc.type == NPCType<Ironzombie>()) ZombieOreLoot(npcLoot, ItemID.IronOre, ItemID.IronBar);

            if (npc.type == NPCType<Leadzombie>()) ZombieOreLoot(npcLoot, ItemID.LeadOre, ItemID.LeadBar);

            if (npc.type == NPCType<Silverzombie>()) ZombieOreLoot(npcLoot, ItemID.SilverOre, ItemID.SilverBar);

            if (npc.type == NPCType<Tinzombie>()) ZombieOreLoot(npcLoot, ItemID.TinOre, ItemID.TinBar);

            if (npc.type == NPCType<Tungstenzombie>()) ZombieOreLoot(npcLoot, ItemID.TungstenOre, ItemID.TungstenBar);
              
            if (npc.type == NPCType<Tuxonitezombie>()) ZombieOreLoot(npcLoot, ItemType<TuxoniteOre>(), ItemType<TuxoniteBar>());
               
            if (npc.type == NPCType<Shadowzombie>()) ZombieOreLoot(npcLoot, ItemID.DemoniteOre, ItemID.DemoniteBar);
               
            if (npc.type == NPCType<Crimsonzombi>()) ZombieOreLoot(npcLoot, ItemID.CrimtaneOre, ItemID.CrimtaneBar);

            if (npc.type == NPCType<ReinforcedIronZombie>()) ZombieOreLoot(npcLoot, ItemType<Reinforced_iron_ore>(), ItemType<Reinforced_ironBar>());
            if (npc.type == NPCType<MoltenZombie>()) ZombieOreLoot(npcLoot,ItemID.Hellstone,ItemID.HellstoneBar);
            if (npc.type == NPCType<NightZombie>())
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.JungleSpores, 8 / dificultyDrop(1), 1, 5));
                npcLoot.Add(ItemDropRule.Common(ItemID.Stinger, 8 / dificultyDrop(1), 1, 5));
                npcLoot.Add(ItemDropRule.Common(ItemID.Obsidian, 8 / dificultyDrop(1), 1, 5));
                npcLoot.Add(ItemDropRule.Common(ItemID.Hellstone, 8 / dificultyDrop(1), 1, 5));
                npcLoot.Add(ItemDropRule.Common(ItemID.Bone, 8 / dificultyDrop(1), 1, 5));
                npcLoot.Add(ItemDropRule.Common(ItemID.Cobweb, 8 / dificultyDrop(1), 1, 5));
                npcLoot.Add(ItemDropRule.Common(ItemID.Stinger, 8 / dificultyDrop(1), 1, 5));
                int corruptOre = WorldGen.crimson ? ItemID.DemoniteOre : ItemID.CrimtaneOre;
                npcLoot.Add(ItemDropRule.Common(corruptOre, 8 / dificultyDrop(1), 1, 5));
                npcLoot.Add(ItemDropRule.Common(ItemType<NightBar>(), 50 / dificultyDrop(1), 1, 5));
            }
            if (npc.type == NPCType<ArmoredSlime>())
            {
                ZombieOreLoot(npcLoot, ItemType<Reinforced_iron_ore>(), ItemType<Reinforced_ironBar>());
                ItemDropRule.Common(ItemID.Gel, 1, 10, 15);
            }
            if (npc.type == NPCType<CobaltZombie>()) ZombieOreLoot(npcLoot, ItemID.CobaltOre, ItemID.CobaltBar);
            if (npc.type == NPCType<PaladiumZombie>()) ZombieOreLoot(npcLoot, ItemID.PalladiumOre, ItemID.PalladiumBar);
            if (npc.type == NPCType<MythrilZombie>()) ZombieOreLoot(npcLoot, ItemID.MythrilOre, ItemID.MythrilBar);
            if (npc.type == NPCType<OrichalcumZombie>()) ZombieOreLoot(npcLoot, ItemID.OrichalcumOre, ItemID.OrichalcumBar);
            if (npc.type == NPCType<AdamantiteZombie>()) ZombieOreLoot(npcLoot, ItemID.AdamantiteOre, ItemID.AdamantiteBar);
            if (npc.type == NPCType<TitaniumZombie>()) ZombieOreLoot(npcLoot, ItemID.TitaniumOre, ItemID.TitaniumBar);
        }
        public override void OnKill(NPC npc)
        {
        }
        public void SoulLoot(NPC npc,NPCLoot npcLoot,Player player)
        {
            
            if (npc.boss)
            {
                switch (npc.type)
                {
                    case NPCID.KingSlime:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.SlimeReaperSoulRule(), ItemType<KingSoul>()));
                        break;
                    case NPCID.EyeofCthulhu:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.EyeOfChutuluReaperSoulRule(),ItemType<EyeSoul>(),1));
                        break;
                    case NPCID.BrainofCthulhu:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.CorruptReaperSoulRule(), ItemType<CorruptSoul>()));
                        break;
                    case NPCID.QueenBee:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.QueenBeeReaperSoulRule(), ItemType<BeeSoul>()));
                        break;
                    case NPCID.SkeletronHead:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.SkeletronReaperSoulRule(), ItemType<SkeletonSoul>()));
                        break;
                    case NPCID.Deerclops:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.DeerclopsReaperSoulRule(), ItemType<DeerclopsSoul>()));
                        break;
                    case NPCID.WallofFlesh:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.WallOfFLeshReaperSoulRule(), ItemType<FleshSoul>()));
                        break;
                    case NPCID.QueenSlimeBoss:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.QueenSlimeReaperSoulRule(), ItemType<QueenSoul>()));
                        break;
                    case NPCID.Retinazer:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.RetinazorReaperSoulRule(), ItemType<RetinazorEnergyCell>()));
                        break;
                    case NPCID.Spazmatism:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.SpazmatismReaperSoulRule(), ItemType<SpazmatismEnergyCell>()));
                        break;
                    case NPCID.SkeletronPrime:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.SkeletronPrimeReaperSoulRule(), ItemType<SkeletronPrimeEnergyCell>()));
                        break;
                    case NPCID.TheDestroyer:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.DestroyerReaperSoulRule(), ItemType<DestroyerEnergyCell>()));
                        break;
                    case NPCID.Plantera:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.PlanteraReaperSoulRule(), ItemType<PlantSoul>()));
                        break;
                    case NPCID.HallowBoss:             
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.EmpressOfLightReaperSoulRule(), ItemType<EmpressSoul>()));
                        break;
                    case NPCID.Golem:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.GolemReaperSoulRule(), ItemType<GolemSoul>()));
                        break;
                    case NPCID.DukeFishron:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.DukeFishronReaperSoulRule(), ItemType<FishSoul>()));
                        break;
                    case NPCID.CultistBoss:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.LunaticCultistReaperSoulRule(), ItemType<CultistSoul>()));
                        break;
                    case NPCID.MoonLordCore:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.MoonLordReaperSoulRule(), ItemType<MoonSoul>()));
                        break;
                }
                //if (npc.boss && System.Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1 )
                //{
             //   bool j = Utils1.debug(npc);

                  //  npcLoot.Add(ItemDropRule.ByCondition(new CorruptReaperSoulRule(), ItemType<CorruptSoul>()));
               // }
                if(npc.type == NPCType<DesertAniquilator>())
                {
                    npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.DesertAnhilatorReaperSoulRule(), ItemType<DesertSoul>()));
                }
                if (npc.type == NPCType<FrozenAssaulter>())
                {
                    npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.FrozenAssaulterReaperSoulRule(), ItemType<FrozenSoul>()));
                }
                if (npc.type == NPCType<InfernalTyrantHead>())
                {
                    npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.InfernalTyrantReaperSoulRule(), ItemType<InfernalSoul>()));
                }

                //if (npc.type == NPCID.Skeleton && !Main.LocalPlayer.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade) ItemDropRule.Common(ItemType<SkeletonSoul>(), 1, 1, 1);

                //if (npc.type == NPCType<DesertAniquilator>() && !Main.LocalPlayer.GetModPlayer<DesertReaperSoulPlayer>().DesertReaperUpgrade) ItemDropRule.Common(ItemType<DesertSoul>(), 1, 1, 1);

                //if (npc.type == NPCID.WallofFlesh && !Main.LocalPlayer.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade) ItemDropRule.Common(ItemType<FleshSoul>(), 1, 1, 1);

                //if (npc.type == NPCType<FrozenAssaulter>() && !Main.LocalPlayer.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade) ItemDropRule.Common(ItemType<FrozenSoul>(), 1, 1, 1);

                //if (npc.type == NPCID.TheDestroyer && !Main.LocalPlayer.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade) ItemDropRule.Common(ItemType<DestroyerEnergyCell>(), 1, 1, 1);

                //if (npc.type == NPCID.SkeletronPrime && !Main.LocalPlayer.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade) ItemDropRule.Common(ItemType<SkeletronPrimeEnergyCell>(), 1, 1, 1);

                //if (npc.type == NPCID.Spazmatism && !Main.LocalPlayer.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade) ItemDropRule.Common(ItemType<SpazmatismEnergyCell>(), 1, 1, 1);

                //if (npc.type == NPCID.Retinazer && !Main.LocalPlayer.GetModPlayer<RetinazorReaperSoulPlayer>().RetinazorReaperUpgrade) ItemDropRule.Common(ItemType<RetinazorEnergyCell>(), 1, 1, 1);

                //if (npc.type == NPCID.Plantera && !Main.LocalPlayer.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade) ItemDropRule.Common(ItemType<PlantSoul>(), 1, 1, 1);

                //if (npc.type == NPCType<InfernalTyrantHead>() && !Main.LocalPlayer.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade) ItemDropRule.Common(ItemType<InfernalSoul>(), 1, 1, 1);

                //if (npc.type == NPCID.Golem && !Main.LocalPlayer.GetModPlayer<GolemReaperSoulPlayer>().GolemReaperUpgrade) ItemDropRule.Common(ItemType<GolemSoul>(), 1, 1, 1);

                //if (npc.type == NPCID.DukeFishron && !Main.LocalPlayer.GetModPlayer<DukeReaperSoulPlayer>().DukeReaperUpgrade) ItemDropRule.Common(ItemType<FishSoul>(), 1, 1, 1);

                //if (npc.type == NPCID.CultistBoss && !Main.LocalPlayer.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade) ItemDropRule.Common(ItemType<CultistSoul>(), 1, 1, 1);

                //if (npc.type == NPCID.MoonLordCore && !Main.LocalPlayer.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade) ItemDropRule.Common(ItemType<MoonSoul>(), 1, 1, 1);
            }
        }
        
        public void ZombieOreLoot(NPCLoot npcLoot,int ore, int bar)
        {
            npcLoot.Add(ItemDropRule.Common(ore, 1/ dificultyDrop(1), 1, 4));
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
            if (DificultyUtils.ReaperMode) a = i * 5;
            return a;
        }
     
    }
}  
        
