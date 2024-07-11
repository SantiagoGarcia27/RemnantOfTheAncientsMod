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
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Relics.Infernum;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Systems;
using System.Collections.Generic;
using System;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Buf;

namespace RemnantOfTheAncientsMod.Common.Drops
{
    public class ModGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            ArmoredZombiesLoot(npc, npcLoot);
            BossLoot(npc, npcLoot);
            CommonLoot(npc, npcLoot);
            PilarEnemiyLoot(npc, npcLoot);
            HallowBiomeLoot(npc, npcLoot);
            CorruptBiomeLoot(npc, npcLoot);
            CrimsomBiomeLoot(npc, npcLoot);
            if (RemnantOfTheAncientsMod.InfernumMod != null)
            {
                InfernumLoot(npc, npcLoot);
            }
            if (RemnantOfTheAncientsMod.FargosSoulMod != null)
            {
                FargosLoot(npc, npcLoot);
            }     
            if (npc.value > 0f)
            {
                if (npc.lifeMax > 100000)
                {
                    npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<The_Legion>(), 100000, ReaperDrop(100000)));
                }
                else
                {
                    npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<The_Legion>(), 500000, ReaperDrop(500000)));
                }
            }
            //  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fake_The_Legion>(), 1 / dificultyDrop(1), 1, 1));
            //SoulLoot(npc, npcLoot, new Player());
        }
        public void CommonLoot(NPC npc, NPCLoot npcLoot)
        {

            if (npc.type == NPCID.Shark) npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<SharkTooth>(), 10, ReaperDrop(10),1,5));
            if (npc.type == NPCID.Tim) npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<magic_stick>(), 2, ReaperDrop(2)));
            if (npc.type == NPCID.DungeonGuardian) npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<TortugaPet>(), 2, ReaperDrop(2)));
            if (npc.type == NPCID.BigMimicCorruption) npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.DownedPlantera(), ItemID.ScourgeoftheCorruptor, 4, 1, 1));
            if (npc.type == NPCID.BigMimicCrimson) npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.DownedPlantera(), ItemID.VampireKnives, 4, 1, 1));
            if (npc.type == NPCID.BigMimicHallow) npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.DownedPlantera(), ItemID.RainbowGun, 4, 1, 1));
            if (npc.type == NPCID.BigMimicJungle) npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.DownedPlantera(), ItemID.PiranhaGun, 4, 1, 1));
            if (npc.type == NPCID.BirdRed || npc.type == NPCID.BirdBlue || npc.type == NPCID.Bird) npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<BirdWings>(), 100, ReaperDrop(100)));
        }
        public static void HallowBiomeLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.IlluminantBat) npcLoot.Add(ItemDropRule.Common(ItemID.LightShard, 50, 1, 1));
            if (npc.type == NPCID.ChaosElemental) npcLoot.Add(ItemDropRule.Common(ItemID.LightShard, 50, 1, 1));
            if (npc.type == NPCID.EnchantedSword) npcLoot.Add(ItemDropRule.Common(ItemID.LightShard, 50, 1, 1));
            if (npc.type == NPCID.PigronHallow) npcLoot.Add(ItemDropRule.Common(ItemID.LightShard, 10, 1, 1));
        }
        public static void CorruptBiomeLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.CursedHammer) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 50, 1, 1));
            if (npc.type == NPCID.PigronCorruption) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 10, 1, 1));
            if (npc.type == NPCID.Clinger) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 50, 1, 1));
        }
        public static void CrimsomBiomeLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.CrimsonAxe) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 100, 1, 1));
            if (npc.type == NPCID.PigronCrimson) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 10, 1, 1));
            if (npc.type == NPCID.FloatyGross) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 50, 1, 1));
            if (npc.type == NPCID.IchorSticker) npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 50, 1, 1));
        }
        public List<int> PilarEnemy =
        [
            NPCID.SolarDrakomireRider, NPCID.SolarSolenian, NPCID.SolarDrakomire, NPCID.SolarCrawltipedeHead, NPCID.SolarCorite,
            NPCID.VortexHornet, NPCID.VortexHornetQueen, NPCID.VortexLarva, NPCID.VortexRifleman, NPCID.VortexSoldier,
            NPCID.StardustCellBig, NPCID.StardustJellyfishBig, NPCID.StardustSoldier, NPCID.StardustSpiderBig, NPCID.StardustWormHead,
            NPCID.NebulaBeast, NPCID.NebulaBrain, NPCID.NebulaHeadcrab, NPCID.NebulaSoldier
        ];
        public List<int> CelestialTower =
        [
            NPCID.LunarTowerSolar,
            NPCID.LunarTowerVortex,
            NPCID.LunarTowerStardust,
            NPCID.LunarTowerNebula
        ];
        public void PilarEnemiyLoot(NPC npc, NPCLoot npcLoot)
        {
            if (PilarEnemy.Contains(npc.type))
            {
                npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<Neutrum_Fragment>(), 3, 1));
            }
            if (CelestialTower.Contains(npc.type))
            {
                npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<Neutrum_Fragment>(), 3, 1, 1, 10));
            }
        }
        //public void LegendaryLoot(NPC npc, NPCLoot npcLoot)
        //{
        //    if (npc.type == NPCID.MoonLordCore) npcLoot.Add(ItemDropRule.Common(ItemType<LaserMachineGun>(), 100 / dificultyDrop(1), 1, 1));
        //    if (npc.type == NPCType<InfernalTyrantHead>()) npcLoot.Add(ItemDropRule.Common(ItemType<TheSpiker>(), 200 / dificultyDrop(1), 1, 1));
        //    if (npc.type == NPCID.Golem) npcLoot.Add(ItemDropRule.Common(ItemType<Legendary_Great_Sword>(), (800 / dificultyDrop(1)), 1, 1));
        //}
        [JITWhenModsEnabled("InfernumMode")]
        public static void InfernumLoot(NPC npc, NPCLoot npcLoot)
        {
            bool Infernum = DificultyUtils.InfernumMode;

            if (npc.type == NPCType<DesertAniquilator>())
                npcLoot.AddIff(() => DificultyUtils.InfernumMode, ItemType<Desert_Relic_Infernum>());
            else if (npc.type == NPCType<FrozenAssaulter>())
                npcLoot.AddIff(() => DificultyUtils.InfernumMode, ItemType<Frozen_Relic_Infernum>());
            else if (npc.type == NPCType<InfernalTyrantHead>())
                npcLoot.AddIff(() => DificultyUtils.InfernumMode, ItemType<Tyrant_Relic_Infernum>());
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public static void FargosLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCType<DesertAniquilator>())
            {
                npcLoot.Add(RemnantDropRules.CommonDropOnAllPlayersWithConditionsNoAllActive([new RemnantConditions.IsEternity(), new RemnantConditions.IsMasochist()], ItemType<DesertMedalion>()));
            }
            if (npc.type == NPCType<FrozenAssaulter>())
            {
                npcLoot.Add(RemnantDropRules.CommonDropOnAllPlayersWithConditionsNoAllActive([new RemnantConditions.IsEternity(), new RemnantConditions.IsMasochist()], ItemType<FrostBarrier>()));
            }
            if (npc.type == NPCType<InfernalTyrantHead>())
            {
                npcLoot.Add(RemnantDropRules.CommonDropOnAllPlayersWithConditionsNoAllActive([new RemnantConditions.IsEternity(), new RemnantConditions.IsMasochist()], ItemType<TyrantsMark>()));
            }
        }
        public void ArmoredZombiesLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCType<Goldzombie>()) ZombieOreLoot(npcLoot, ItemID.GoldOre, ItemID.GoldBar);

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
            if (npc.type == NPCType<MoltenZombie>()) ZombieOreLoot(npcLoot, ItemID.Hellstone, ItemID.HellstoneBar);
            if (npc.type == NPCType<NightZombie>())
            {
                npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemID.JungleSpores, 8, ReaperDrop(8),1,5,1,7));
                npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemID.Stinger, 8, ReaperDrop(8), 1, 5, 1, 7));
                npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemID.Obsidian, 8, ReaperDrop(8), 1, 5, 1, 7));
                npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemID.Hellstone, 8, ReaperDrop(8), 1, 5, 1, 7));
                npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemID.Bone, 8, ReaperDrop(8), 1, 5, 1, 7));
                npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemID.Cobweb, 8, ReaperDrop(8), 1, 5, 1, 7));     
                RemnantDropRules.ReaperModeVsNormal(ItemID.CrimtaneOre, 16, ReaperDrop(16), 1, 5, 1, 7);
                RemnantDropRules.ReaperModeVsNormal(ItemID.DemoniteOre, 16, ReaperDrop(16), 1, 5, 1, 7);
                npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<NightBar>(), 50, ReaperDrop(50), 1, 5, 1, 7));
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
            if (npc.type == NPCType<HallowedZombie>())
            {
                int? ore = RemnantOfTheAncientsMod.CalamityMod != null ? ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "HallowedOre") : null;
                ZombieOreLoot(npcLoot,ore, ItemID.HallowedBar);
            }
        }     
        public static void BossLoot(NPC npc, NPCLoot npcLoot)
        {

            if (npc.boss)
            {
                switch (npc.type)
                {
                    case NPCID.KingSlime:
                        npcLoot.AddIff(() => !Main.LocalPlayer.GetModPlayer<ReaperPlayer>().SoulsUpgrades[0] && DificultyUtils.ReaperMode, ItemType<KingSoul>());
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<KingSlimeScroll>(), 5, 1));
                        break;
                    case NPCID.EyeofCthulhu:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.EyeOfChutuluReaperSoulRule(), ItemType<EyeSoul>(), 1));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<EyeOfCthulhuScroll>(), 5, 1));
                        break;
                    case NPCID.BrainofCthulhu:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.CorruptReaperSoulRule(), ItemType<CorruptSoul>()));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<BrainOfCthulhuScroll>(), 5, 1));
                        break;
                    case NPCID.QueenBee:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.QueenBeeReaperSoulRule(), ItemType<BeeSoul>()));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<QueenBeeScroll>(), 5, 1));
                        break;
                    case NPCID.SkeletronHead:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.SkeletronReaperSoulRule(), ItemType<SkeletonSoul>()));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<SkeletronScroll>(), 2, 1));
                        break;
                    case NPCID.Deerclops:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.DeerclopsReaperSoulRule(), ItemType<DeerclopsSoul>()));
                        break;
                    case NPCID.WallofFlesh:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.WallOfFLeshReaperSoulRule(), ItemType<FleshSoul>()));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<TrowerEmblem>(),3,1));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<WallOfFleshScroll>(), 5, 1));
                        break;
                    case NPCID.QueenSlimeBoss:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.QueenSlimeReaperSoulRule(), ItemType<QueenSoul>()));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<QueenBeeScroll>(), 5, 1));
                        break;
                    case NPCID.Retinazer:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.RetinazorReaperSoulRule(), ItemType<RetinazorEnergyCell>()));
                        break;
                    case NPCID.Spazmatism:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.SpazmatismReaperSoulRule(), ItemType<SpazmatismEnergyCell>()));
                        break;
                    case NPCID.SkeletronPrime:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.SkeletronPrimeReaperSoulRule(), ItemType<SkeletronPrimeEnergyCell>()));
                        npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MechanicalProsthesis>()));
                        break;
                    case NPCID.TheDestroyer:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.DestroyerReaperSoulRule(), ItemType<DestroyerEnergyCell>()));
                        npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MechanicalPersonalLifeSafeSystem>()));
                        break;
                    case NPCID.Plantera:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.PlanteraReaperSoulRule(), ItemType<PlantSoul>()));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemID.StrangePlant1, 1, ReaperDrop(1),1,2,1,5));
                        break;
                    case NPCID.HallowBoss:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.EmpressOfLightReaperSoulRule(), ItemType<EmpressSoul>()));
                        npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.IsDay(), ItemType<StarChunk>(), 1, 3, 5));
                        npcLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.IsNight(), ItemType<StarChunk>(), 1, 1, 2));
                        break;
                    case NPCID.Golem:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.GolemReaperSoulRule(), ItemType<GolemSoul>()));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<Legendary_Great_Sword>(), 800, 800/5));
                        break;
                    case NPCID.DukeFishron:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.DukeFishronReaperSoulRule(), ItemType<FishSoul>()));
                        break;
                    case NPCID.CultistBoss:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.LunaticCultistReaperSoulRule(), ItemType<CultistSoul>()));
                        npcLoot.Add(ItemDropRule.BossBag(ItemID.CultistBossBag));
                        break;
                    case NPCID.MoonLordCore:
                        npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.MoonLordReaperSoulRule(), ItemType<MoonSoul>()));
                        npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<LaserMachineGun>(), 100, 100 / 5));
                        break;
                }
                if (npc.type == NPCType<DesertAniquilator>())
                {
                    npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.DesertAnhilatorReaperSoulRule(), ItemType<DesertSoul>()));
                    BossTrpoyMask(ItemType<DesertTrophy>(), ItemType<DesertAMask>(), npcLoot);
                }
                if (npc.type == NPCType<FrozenAssaulter>())
                {
                    npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.FrozenAssaulterReaperSoulRule(), ItemType<FrozenSoul>()));
                    BossTrpoyMask(ItemType<FrostTrophy>(), ItemType<FrozenMask>(), npcLoot);
                }
                if (npc.type == NPCType<InfernalTyrantHead>())
                {
                    npcLoot.Add(RemnantDropRules.ReaperModeCommonDropOnAllPlayersWithConditions(new RemnantConditions.InfernalTyrantReaperSoulRule(), ItemType<InfernalSoul>()));
                    npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ItemType<TheSpiker>(), 200, 200 / 5));
                    BossTrpoyMask(ItemType<InfernalTrophy>(), ItemType<InfernalMask>(), npcLoot);
                }
            }
        }

        public static void ZombieOreLoot(NPCLoot npcLoot, int ore, int bar)
        {
            npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(ore,1,1,1,4,2,8));
            npcLoot.Add(RemnantDropRules.ReaperModeVsNormal(bar,50, 25,1,1));
        }
        public static void ZombieOreLoot(NPCLoot npcLoot, int? ore, int? bar)
        {
            if (ore != null) npcLoot.Add(RemnantDropRules.ReaperModeVsNormal((int)ore, 1, ReaperDrop(1), 1, 4, 2, 8));
            if (bar != null) npcLoot.Add(RemnantDropRules.ReaperModeVsNormal((int)bar, 50, ReaperDrop(5), 1, 1));
        }  
        public static void BossTrpoyMask(int Tropy, int Mask, NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Tropy, 10, 1, 1, 1));
            npcLoot.Add(new CommonDrop(Mask, 7, 1, 1, 1));
        }
        public static int ReaperDrop(int i)
        {
            return i <= 1? i : i/5 <= 1? 1:i/5;
        }

        public override void OnKill(NPC npc)/* TerracoinDrop(NPC npc, NPCLoot npcLoot)*/
        {
            if (npc.boss)
            {
                if (NPC.downedMoonlord)
                {
                    if (Main.rand.NextBool(50))
                    {
                        Item.NewItem(Item.GetSource_None(), npc.position, npc.Size, ItemType<Terracoin>(), 1, false, 0, false, false);
                    } 
                }
                else
                {
                    Dictionary<int, Func<bool>> npcConditions = new()
                    {
                        { NPCID.KingSlime, () => !NPC.downedSlimeKing },
                        { NPCID.EyeofCthulhu, () => !NPC.downedBoss1 },
                        { NPCID.BrainofCthulhu, () => !NPC.downedBoss2 },
                        { NPCID.QueenBee, () => !NPC.downedQueenBee },
                        { NPCID.Skeleton, () => !NPC.downedBoss3 },
                        { NPCID.Deerclops, () => !NPC.downedDeerclops },
                        { NPCID.QueenSlimeBoss, () => !NPC.downedQueenSlime },
                        { NPCID.TheDestroyer, () => !NPC.downedMechBoss1 },
                        { NPCID.Spazmatism, () => !NPC.downedMechBoss2 },
                        { NPCID.SkeletronPrime, () => !NPC.downedMechBoss3 },
                        { NPCID.Plantera, () => !NPC.downedPlantBoss },
                        { NPCID.HallowBoss, () => !NPC.downedEmpressOfLight },
                        { NPCID.Golem, () => !NPC.downedGolemBoss },
                        { NPCID.CultistBoss, () => !NPC.downedAncientCultist },
                        { NPCID.MoonLordCore, () => !NPC.downedMoonlord },
                        { NPCType<DesertAniquilator>(), () => !RemnantDownedBossSystem.downedDesert },
                        { NPCType<FrozenAssaulter>(), () => !RemnantDownedBossSystem.downedFrozen },
                        { NPCType<InfernalTyrantHead>(), () => !RemnantDownedBossSystem.downedTyrant }
                    };
                    bool condition = false;
                    if (npcConditions.ContainsKey(npc.type))
                    {
                        condition = npcConditions[npc.type]();
                    }      
                    if (condition)
                    {
                        Item.NewItem(Item.GetSource_None(), npc.position, npc.Size, ItemType<Terracoin>(), 1, false, 0, false, false);
                    }
                }
            }
        }
    }
}


        
