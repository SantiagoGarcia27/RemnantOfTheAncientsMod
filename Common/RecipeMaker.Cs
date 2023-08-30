using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon.Whips;
using RemnantOfTheAncientsMod.Content.Items.Tools;

namespace RemnantOfTheAncientsMod
{
    internal class RecipeMaker : ModSystem
    {

        public static RecipeGroup RecipeGroup1;

        public override void Unload()
        {
            RecipeGroup1 = null;
        }


#pragma warning disable CS0114 // 'RecipeMaker.AddRecipes()' hides inherited member 'ModSystem.AddRecipes()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
        public static void AddRecipes()
        {
            Uncrafting();
            Swaped();
            Emblemas();
            Accesories();
            Potoions();
            VanillaPotionCrafting();
            Mirrors();
            CraftinStationsCraft();
            Weapons();
            FishingRods();
            LivingFlames();
            BossSummon();
            HardmodeItems();
            CraftingItems();
        }
        private static void CraftingItems()
        {
            Recipe.Create(ItemID.Leather)
              .AddIngredient(ItemID.Vertebrae, 5)
              .AddTile(TileID.WorkBenches)
              .Register();
        }
        private static void HardmodeItems()
        {
            Recipe.Create(ItemID.RodofDiscord)
               .AddIngredient(ItemID.HallowedBar, 15)
               .AddIngredient(ItemID.PixieDust, 80)
               .AddIngredient(ItemID.SoulofLight, 30)
               .AddIngredient(ItemID.SoulofSight, 5)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }

        private static void BossSummon()
        {
            Recipe.Create(ItemID.TruffleWorm)
               .AddIngredient(ItemID.Worm, 1)
               .AddIngredient(ItemID.GlowingMushroom, 15)
               .AddTile(TileID.Autohammer)
               .Register();
        }

        private static void LivingFlames()
        {
            Recipe.Create(ItemID.LivingFireBlock)
               .AddIngredient(ItemID.Torch, 1)
               .AddTile(TileID.LivingLoom)
               .Register();
        }
        private static void Potoions()
        {
            Recipe.Create(ModContent.ItemType<Endless_Supe_Sense_Potion>())
            .AddIngredient(ModContent.ItemType<EndlessHunter>(), 1)
            .AddIngredient(ModContent.ItemType<EndlessDangersense>(), 1)
            .AddTile(TileID.Bottles)
            .Register();

        }  
        private static void Mirrors()
        {
            Recipe.Create(ItemID.MagicMirror)
               .AddRecipeGroup("IronBar", 5)
               .AddIngredient(ItemID.Glass, 3)
               .AddIngredient(ItemID.FallenStar, 3)
               .AddIngredient(ItemID.RecallPotion, 5)
               .AddTile(TileID.Anvils)
               .Register();

            Recipe.Create(ItemID.MagicMirror)
               .AddIngredient(ItemID.IceMirror, 1)
               .AddTile(TileID.Anvils)
               .Register();

            Recipe.Create(ItemID.IceMirror)
               .AddIngredient(ItemID.MagicMirror, 1)
               .AddTile(TileID.Anvils)
               .Register();
        } 
        #region Weapons
        private static void Weapons()
        {
            PreHardmodeWeapons();
            HardmodeWeapons();
        }
        private static void PreHardmodeWeapons()
        {
            PreHardmodeMagicWeapons();
            PreHardmodeMeleeWeapons();
            PreHardmodeRangerWeapons();
            PreHardmodeSummonerWeapons();
            PreHardmodeThrowerWeapons();
        }
        private static void HardmodeWeapons()
        {
            HardmodeRangerWeapon();
        }

        private static void HardmodeRangerWeapon()
        {
            Recipe.Create(ItemID.Flamethrower)
            .AddRecipeGroup("IronBar", 10)
            .AddIngredient(ItemID.IllegalGunParts)
            .AddIngredient(ItemID.SoulofNight, 30)
            .AddTile(TileID.MythrilAnvil)
            .Register();

            Recipe.Create(ItemID.TacticalShotgun)
            .AddIngredient(ItemID.Shotgun)
            .AddIngredient(ItemID.RifleScope)
            .AddIngredient(ItemID.IllegalGunParts, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }

        private static void PreHardmodeMeleeWeapons()
        {
            Recipe.Create(ItemID.WoodenBoomerang).AddRecipeGroup("IronBar", 2).AddIngredient(ItemID.Wood, 10).AddTile(TileID.WorkBenches).Register();
            Recipe.Create(ItemID.Spear).AddRecipeGroup("IronBar", 2).AddRecipeGroup("GoldBar", 1).AddTile(TileID.Anvils).Register();
            Recipe.Create(ItemID.EnchantedSword).AddRecipeGroup("EspH", 1).AddIngredient(ItemID.FallenStar, 10).AddIngredient(ItemID.SoulofLight, 10).AddIngredient(ItemID.LightShard, 1).AddTile(TileID.MythrilAnvil).Register();

            NightEdgeCraft();
        }

        private static void NightEdgeCraft()
        {

            Recipe.Create(ItemID.NightsEdge)
               .AddIngredient(ModContent.ItemType<NightBar>(), 10)
               .AddTile(TileID.DemonAltar)
               .Register();

            Recipe.Create(ItemID.NightsEdge)
            .AddIngredient(ModContent.ItemType<BurningJungle>())
            .AddIngredient(ItemID.Muramasa)
            .AddRecipeGroup("anyCorruptSwords")
            .AddTile(TileID.DemonAltar)
            .Register();

            Recipe.Create(ItemID.NightsEdge)
            .AddRecipeGroup("anyCorruptSwords")
            .AddIngredient(ItemID.Muramasa)
            .AddIngredient(ItemID.BladeofGrass)
            .AddIngredient(ItemID.FieryGreatsword)
            .AddTile(TileID.DemonAltar)
            .Register();
        }


        private static void PreHardmodeRangerWeapons()
        {
            Recipe.Create(ItemID.PainterPaintballGun)
               .AddIngredient(ItemID.DyeVat, 1)
               .AddIngredient(ItemID.FlintlockPistol, 1)
               .AddTile(TileID.Anvils)
               .Register();

            Recipe.Create(ItemID.BoneArrow,10)
              .AddIngredient(ItemID.Bone, 1)
              .AddTile(TileID.BoneWelder)
              .Register();
        }
        private static void PreHardmodeMagicWeapons()
        {
            Recipe.Create(ItemID.WandofSparking)
               .AddIngredient(ItemID.Wood, 30)
               .AddIngredient(ItemID.Torch, 20)
               .AddTile(TileID.WorkBenches)
               .Register();
        }
        private static void PreHardmodeSummonerWeapons()
        {
            Recipe.Create(ModContent.ItemType<Twilight>())
               .AddIngredient(ItemID.ThornWhip, 1)
               .AddIngredient(ItemID.BoneWhip, 1)
               .AddIngredient(ModContent.ItemType<FireStorm>())
               .AddRecipeGroup("anyCorruptWhip")
               .AddTile(TileID.DemonAltar)
               .Register();
        }
        private static void PreHardmodeThrowerWeapons()
        {

            Recipe.Create(ItemID.Shuriken, 50)
               .AddRecipeGroup("IronBar")
               .AddTile(TileID.Anvils)
               .Register();


            Recipe.Create(ItemID.ThrowingKnife, 50)
               .AddRecipeGroup("IronBar")
               .AddTile(TileID.Anvils)
               .Register();

        }
        #endregion
        #region Swap
        private static void Swaped()
        {
            SwapCorruptionLightPet();
            SwapSouls();
            SwapCorruptDebuffMaterial();
            SwapHardmodeBars();
            SwapPreBossBars();
            SwapCorruptBars();
            SwapCorruptMushroom();
            SwapDolls();
            CorruptExpertAccesories();
            SwapFish();
        }
        private static void SwapFish()
        {
            Recipe.Create(ItemID.Hemopiranha).AddIngredient(ItemID.Ebonkoi).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.Ebonkoi).AddIngredient(ItemID.Hemopiranha).AddTile(TileID.AlchemyTable).Register();
        }
        private static void CorruptExpertAccesories()
        {
            Recipe.Create(ItemID.BrainOfConfusion)
            .AddIngredient(ItemID.WormScarf, 1)
            .AddTile(TileID.WorkBenches)
            .Register();

            Recipe.Create(ItemID.WormScarf)
            .AddIngredient(ItemID.BrainOfConfusion, 1)
            .AddTile(TileID.WorkBenches)
            .Register();
        }

        private static void SwapCorruptMushroom()
        {
            Recipe.Create(ItemID.ViciousMushroom).AddIngredient(ItemID.VileMushroom).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.VileMushroom).AddIngredient(ItemID.ViciousMushroom).AddTile(TileID.AlchemyTable).Register();
        }

        private static void SwapDolls()
        {
            Recipe.Create(ItemID.ClothierVoodooDoll).AddIngredient(ItemID.GuideVoodooDoll, 1).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.GuideVoodooDoll).AddIngredient(ItemID.ClothierVoodooDoll, 1).AddTile(TileID.AlchemyTable).Register();
        }

        private static void SwapCorruptBars()
        {
            Recipe.Create(ItemID.CrimtaneBar).AddIngredient(ItemID.DemoniteBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.DemoniteBar).AddIngredient(ItemID.CrimtaneBar).AddTile(TileID.AlchemyTable).Register();
        }

        private static void SwapPreBossBars()
        {
            Recipe.Create(ItemID.PlatinumBar).AddIngredient(ItemID.GoldBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.GoldBar).AddIngredient(ItemID.PlatinumBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.TungstenBar).AddIngredient(ItemID.SilverBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.SilverBar).AddIngredient(ItemID.TungstenBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.LeadBar).AddIngredient(ItemID.IronBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.IronBar).AddIngredient(ItemID.LeadBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.CopperBar).AddIngredient(ItemID.TinBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.TinBar).AddIngredient(ItemID.CopperBar).AddTile(TileID.AlchemyTable).Register();
        }

        private static void SwapCorruptDebuffMaterial()
        {
            Recipe.Create(ItemID.CursedFlame).AddIngredient(ItemID.Ichor).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.Ichor).AddIngredient(ItemID.CursedFlame).AddTile(TileID.AlchemyTable).Register();
        }

        private static void SwapHardmodeBars()
        {
            Recipe.Create(ItemID.AdamantiteBar).AddIngredient(ItemID.TitaniumBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.TitaniumBar).AddIngredient(ItemID.AdamantiteBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.PalladiumBar).AddIngredient(ItemID.CobaltBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.CobaltBar).AddIngredient(ItemID.PalladiumBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.OrichalcumBar).AddIngredient(ItemID.MythrilBar).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.MythrilBar).AddIngredient(ItemID.OrichalcumBar).AddTile(TileID.AlchemyTable).Register();
        }

        private static void SwapSouls()
        {
            Recipe.Create(ItemID.SoulofNight).AddIngredient(ItemID.SoulofLight).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.SoulofLight).AddIngredient(ItemID.SoulofNight).AddTile(TileID.AlchemyTable).Register();
        }

        private static void SwapCorruptionLightPet()
        {
            Recipe.Create(ItemID.CrimsonHeart).AddIngredient(ItemID.ShadowOrb).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.ShadowOrb).AddIngredient(ItemID.CrimsonHeart).AddTile(TileID.AlchemyTable).Register();
        }

        private static void Uncrafting()
        {
            UncraftMolten();
            UncraftJungle();
            UncraftBone();

            Recipe.Create(ModContent.ItemType<JungleBar>(), 6).AddIngredient(ItemID.BladeofGrass, 1).AddTile(TileID.Anvils).Register();
            Recipe.Create(ItemID.PlatinumCoin, 100).AddIngredient(ModContent.ItemType<Terracoin>(), 1).Register();
            Recipe.Create(ItemID.EnchantedSword, 1).AddIngredient(ModContent.ItemType<Enchanted_saber>(), 1).AddTile(TileID.Anvils).Register();
        }

        private static void UncraftBone()
        {
            {
                Recipe.Create(ModContent.ItemType<BoneBar>(), 23)
                 .AddIngredient(ItemID.NecroGreaves, 1)
                 .AddTile(TileID.Anvils)
                 .Register();
            }
            {
                Recipe.Create(ModContent.ItemType<BoneBar>(), 18)
                   .AddIngredient(ItemID.NecroHelmet, 1)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
            {
                Recipe.Create(ModContent.ItemType<BoneBar>(), 29)
                   .AddIngredient(ItemID.NecroBreastplate, 1)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
        }

        private static void UncraftJungle()
        {
            {
                Recipe.Create(ModContent.ItemType<JungleBar>(), 3)
                   .AddIngredient(ItemID.JungleHat, 1)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
            {
                Recipe.Create(ModContent.ItemType<JungleBar>(), 6)
                   .AddIngredient(ItemID.JungleShirt, 1)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
            {
                Recipe.Create(ModContent.ItemType<JungleBar>(), 3)
                   .AddIngredient(ItemID.JunglePants, 1)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
        }

        private static void UncraftMolten()
        {
            {
                Recipe.Create(ItemID.HellstoneBar, 10)
                   .AddIngredient(ItemID.MoltenHelmet, 1)
                   .AddTile(TileID.Hellforge)
                   .Register();
            }
            {
                Recipe.Create(ItemID.HellstoneBar, 20)
                   .AddIngredient(ItemID.MoltenBreastplate, 1)
                   .AddTile(TileID.Hellforge)
                   .Register();
            }
            {
                Recipe.Create(ItemID.HellstoneBar, 15)
                   .AddIngredient(ItemID.MoltenGreaves, 1)
                   .AddTile(TileID.Hellforge)
                   .Register();
            }
        }
        #endregion
        private static void FishingRods()
        {
            Recipe.Create(ItemID.HotlineFishingHook)
               .AddIngredient(ItemID.ReinforcedFishingPole)
               .AddIngredient(ItemID.HellstoneBrick, 30)
               .AddIngredient(ItemID.Obsidian, 30)
               .AddIngredient(ItemID.LivingFireBlock, 50)
               .AddTile(TileID.Hellforge)
               .Register();

            Recipe.Create(ItemID.GoldenFishingRod)
               .AddIngredient(ModContent.ItemType<TuxoniteFishingPole>())
               .AddIngredient(ModContent.ItemType<Fish_Potion>(), 1)
               .AddIngredient(ItemID.GoldDust, 40)
               .AddIngredient(ItemID.SoulofLight, 30)
               .AddTile(TileID.MythrilAnvil)
               .Register();

            Recipe.Create(ItemID.MagicConch)
              .AddIngredient(ItemID.MagicMirror)
              .AddIngredient(ItemID.Seashell, 5)
              .AddTile(TileID.Anvils)
              .Register();

            Recipe.Create(ItemID.DemonConch)
             .AddIngredient(ItemID.MagicConch)
             .AddIngredient(ItemID.HellstoneBar, 3)
             .AddTile(TileID.Hellforge)
             .Register();

            Recipe.Create(ItemID.LavaFishingHook)
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddIngredient(ItemID.MasterBait, 2)
                .AddTile(TileID.Hellforge)
                .Register();
        }
        private static void Emblemas()
        {
            Recipe.Create(ItemID.SummonerEmblem)
               .AddRecipeGroup("Emblemas")
               .AddTile(TileID.AlchemyTable)
               .Register();
            Recipe.Create(ItemID.SorcererEmblem)
               .AddRecipeGroup("Emblemas")
               .AddTile(TileID.AlchemyTable)
               .Register();

            Recipe.Create(ItemID.WarriorEmblem)
               .AddRecipeGroup("Emblemas")
               .AddTile(TileID.AlchemyTable)
               .Register();

            Recipe.Create(ItemID.RangerEmblem)
               .AddRecipeGroup("Emblemas")
               .AddTile(TileID.AlchemyTable)
               .Register();

            Recipe.Create(ModContent.ItemType<TrowerEmblem>())
               .AddRecipeGroup("Emblemas")
               .AddTile(TileID.AlchemyTable)
               .Register();
        }
        private static void CraftinStationsCraft()
        {
            Recipe.Create(ItemID.BoneWelder)
                .AddIngredient(ItemID.Bone, 20)
                .AddRecipeGroup("IronBar", 4)
                .AddTile(TileID.Anvils)
                .Register();
        }
        #region Accesorios
        private static void Accesories()
        {
            SurfaceAccesories();
            JungleAccesories();
            CavernAccesories();
            DesertAccesories();
            DungeonAccesories();
            OceanAccesories();
            UnderworldAccesories();
            CorruptAccesories();
            TravelingMerchantAccesories();
            SkyAccesories();
        }

        private static void SkyAccesories()
        {
            Recipe.Create(ItemID.ShinyRedBalloon)
            .AddRecipeGroup("String", 1)
            .AddIngredient(ItemID.Cloud, 10)
            .AddTile(TileID.SkyMill)
            .Register();
        }

        private static void TravelingMerchantAccesories()
        {
            Recipe.Create(ItemID.CelestialMagnet)
               .AddIngredient(ItemID.LuckyHorseshoe, 1)
               .AddIngredient(ItemID.ManaCrystal, 5)
               .AddRecipeGroup("GoldBar", 5)
               .AddTile(TileID.Anvils)
               .Register();
        }

        private static void DungeonAccesories()
        {
            Recipe.Create(ItemID.CobaltShield)
               .AddIngredient(ItemID.CobaltBar, 5)
               .AddTile(TileID.MythrilAnvil)
               .Register();
        }

        private static void CavernAccesories()
        {
            DobleJumpBottles();
            Boots();
            Recipe.Create(ItemID.PocketMirror)
               .AddIngredient(ItemID.GoldBar, 5)
               .AddIngredient(ItemID.Glass, 3)
               .AddTile(TileID.Anvils)
               .Register();

            Recipe.Create(ItemID.DPSMeter)
               .AddRecipeGroup("IronBar", 3)
               .AddIngredient(ItemID.Wire, 20)
               .AddTile(TileID.TinkerersWorkbench)
               .Register();

            Recipe.Create(ItemID.LuckyHorseshoe)
               .AddRecipeGroup("GoldBar", 10)
               .AddTile(TileID.SkyMill)
               .Register();


            Recipe.Create(ItemID.LifeformAnalyzer)
                .AddRecipeGroup("IronBar", 3)
                .AddIngredient(ItemID.Wire, 20)
                .AddIngredient(ItemID.Gel, 2)
                .AddIngredient(ItemID.Feather, 2)
                .AddIngredient(ItemID.Bone, 2)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

            Recipe.Create(ItemID.MetalDetector)
                .AddRecipeGroup("IronBar", 3)
                .AddRecipeGroup("GoldBar", 3)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            Recipe.Create(ItemID.BandofRegeneration)
               .AddIngredient(ModContent.ItemType<IronBand>(), 1)
               .AddIngredient(ItemID.LifeCrystal, 1)
               .AddTile(TileID.Anvils)
               .Register();
        }

        private static void OceanAccesories()
        {
            Recipe.Create(ItemID.SharkToothNecklace)
               .AddIngredient(ModContent.ItemType<SharkTooth>(), 3)
               .AddIngredient(ItemID.Chain, 1)
               .AddTile(TileID.WorkBenches)
               .Register();

            Recipe.Create(ItemID.DivingHelmet)
                .AddRecipeGroup("IronBar", 10)
                .AddIngredient(ItemID.Glass, 3)
                .AddIngredient(ItemID.SharkFin, 3)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe.Create(ItemID.WaterWalkingBoots)
               .AddIngredient<Boot>()
                .AddIngredient(ItemID.WaterWalkingPotion, 2)
                .AddIngredient(ItemID.SharkFin, 2)
                .AddTile(TileID.Anvils)
                .Register();

        }

        private static void DesertAccesories()
        {
            Recipe.Create(ItemID.FlyingCarpet)
               .AddIngredient(ItemID.AncientCloth, 5)
               .AddTile(TileID.Loom)
               .Register();
        }

        private static void SurfaceAccesories()
        {
            Recipe.Create(ItemID.Shackle)
               .AddIngredient(ItemID.IronBar, 1)
               .AddTile(TileID.Anvils)
               .Register();

            Recipe.Create(ItemID.Aglet)
               .AddRecipeGroup("IronBar", 2)
               .AddTile(TileID.Anvils)
               .Register();

            Recipe.Create(ItemID.Radar)
                .AddRecipeGroup("IronBar", 3)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        private static void JungleAccesories()
        {
            Recipe.Create(ItemID.FeralClaws)
               .AddIngredient(ItemID.JungleSpores, 5)
               .AddIngredient(ItemID.Stinger, 5)
               .AddTile(TileID.Anvils)
               .Register();

            Recipe.Create(ItemID.Bezoar)
              .AddIngredient(ItemID.JungleSpores, 5)
              .AddIngredient(ItemID.Stinger, 5)
              .AddTile(TileID.Anvils)
              .Register();

            Recipe.Create(ItemID.AnkletoftheWind)
               .AddIngredient(ItemID.JungleSpores, 10)
               .AddIngredient(ItemID.Vine, 3)
               .AddIngredient(ItemID.PinkGel, 1)
               .AddTile(TileID.Anvils)
               .Register();

            Recipe.Create(ItemID.NaturesGift)
               .AddIngredient(ItemID.JungleRose, 1)
               .AddIngredient(ItemID.ManaCrystal, 1)
               .AddTile(TileID.Anvils)
               .Register();

        }

        private static void CorruptAccesories()
        {
            Recipe.Create(ItemID.BandofStarpower).AddIngredient(ItemID.PanicNecklace, 1).AddTile(TileID.AlchemyTable).Register();
            Recipe.Create(ItemID.PanicNecklace).AddIngredient(ItemID.BandofStarpower, 1).AddTile(TileID.AlchemyTable).Register();

            Recipe.Create(ItemID.BandofStarpower)
                .AddIngredient(ModContent.ItemType<IronBand>(), 1)
                .AddIngredient(ItemID.ManaCrystal, 2)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe.Create(ItemID.CrimsonHeart)
               .AddIngredient(ItemID.LifeCrystal, 1)
               .AddIngredient(ItemID.TissueSample, 10)
               .AddTile(TileID.DemonAltar)
               .Register();
            Recipe.Create(ItemID.ShadowOrb)
               .AddIngredient(ItemID.ManaCrystal, 1)
               .AddIngredient(ItemID.ShadowScale, 10)
               .AddTile(TileID.DemonAltar)
               .Register();
        }

        private static void UnderworldAccesories()
        {
            {
                Recipe.Create(ItemID.MagmaStone)
                   .AddIngredient(ItemID.StoneBlock, 100)
                   .AddIngredient(ItemID.LavaBucket, 1)
                   .AddTile(TileID.Hellforge)
                   .Register();
            }
            {
                Recipe.Create(ItemID.ObsidianRose)
                   .AddIngredient(ItemID.Obsidian, 10)
                   .AddTile(TileID.Hellforge)
                   .Register();
            }
            {
                Recipe.Create(ItemID.LavaCharm)
                   .AddIngredient(ItemID.IronBar, 10)
                   .AddIngredient(ItemID.LavaBucket, 3)
                   .AddTile(TileID.Hellforge)
                   .Register();
            }
        }

        private static void Boots()
        {
            {
                Recipe.Create(ItemID.HermesBoots)
                   .AddIngredient(ModContent.ItemType<Boot>(), 1)
                   .AddIngredient(ItemID.SwiftnessPotion, 2)
                   .AddIngredient(ItemID.Feather, 2)
                   .AddTile(TileID.Loom)
                   .Register();
            }
            {
                Recipe.Create(ItemID.FlurryBoots)
                   .AddIngredient(ItemID.HermesBoots, 1)
                   .AddIngredient(ItemID.SnowBlock, 10)
                   .AddTile(TileID.Loom)
                   .Register();
            }
            {
                Recipe.Create(ItemID.FlurryBoots)
                   .AddIngredient(ItemID.HermesBoots, 1)
                   .AddIngredient(ItemID.BottledWater, 1)
                   .AddTile(TileID.Loom)
                   .Register();
            }
            {
                Recipe.Create(ItemID.IceSkates)
                   .AddIngredient(ModContent.ItemType<Boot>(), 1)
                   .AddIngredient(ItemID.IronBar, 2)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
        }

        private static void DobleJumpBottles()
        {
            {
                Recipe.Create(ItemID.CloudinaBottle)
                   .AddIngredient(ItemID.Cloud, 15)
                   .AddIngredient(ItemID.RainCloud, 5)
                   .AddIngredient(ItemID.Bottle, 1)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
            {
                Recipe.Create(ItemID.SandstorminaBottle)
                   .AddIngredient(ItemID.CloudinaBottle, 1)
                   .AddIngredient(ItemID.SandBlock, 30)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
            {
                Recipe.Create(ItemID.BlizzardinaBottle)
                   .AddIngredient(ItemID.CloudinaBottle, 1)
                   .AddIngredient(ItemID.SnowBlock, 30)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
            {
                Recipe.Create(ItemID.TsunamiInABottle)
                   .AddIngredient(ItemID.CloudinaBottle, 1)
                   .AddIngredient(ItemID.BottledWater, 2)
                   .AddTile(TileID.Anvils)
                   .Register();
            }
        }

        private static void VanillaPotionCrafting()
        {
            int count = 2;
            for (int i = 288; i <= 305; i++)
            {
                Recipe.Create(i, 2)
                   .AddRecipeGroup("anyStrangePlants")
                   .AddIngredient(ItemID.BottledWater)
                   .AddTile(TileID.AlchemyTable)
                   .Register();

            }
            for (int i = 2322; i <= 2329; i++)
            {
                Recipe.Create(i, 2)
                   .AddRecipeGroup("anyStrangePlants")
                   .AddIngredient(ItemID.BottledWater)
                   .AddTile(TileID.AlchemyTable)
                   .Register();
            }
            for (int i = 2344; i <= 2356; i++)
            {
                count = i == 2353? 10: 2;
                Recipe.Create(i, count)
                   .AddRecipeGroup("anyStrangePlants")
                   .AddIngredient(ItemID.BottledWater)
                   .AddTile(TileID.AlchemyTable)
                   .Register();
            }
            for (int i = 4477; i <= 4478; i++)
            {

                Recipe.Create(i, 2)
                   .AddRecipeGroup("anyStrangePlants")
                   .AddIngredient(ItemID.BottledWater)
                   .AddTile(TileID.AlchemyTable)
                   .Register();
            }
                 Recipe.Create(ItemID.LuckPotionGreater, 4)
                   .AddRecipeGroup("anyStrangePlants")
                   .AddIngredient(ItemID.BottledWater)
                   .AddTile(TileID.AlchemyTable)
                   .Register();

            Recipe.Create(ModContent.ItemType<Commander_Potion>(), 2)
              .AddRecipeGroup("anyStrangePlants")
              .AddIngredient(ItemID.BottledWater)
              .AddTile(TileID.AlchemyTable)
              .Register();

            Recipe.Create(ModContent.ItemType<Money_Collector_Potion>(), 2)
              .AddRecipeGroup("anyStrangePlants")
              .AddIngredient(ItemID.BottledWater)
              .AddTile(TileID.AlchemyTable)
              .Register();

            Recipe.Create(ModContent.ItemType<Fish_Potion>(), 1)
              .AddRecipeGroup("anyStrangePlants")
              .AddIngredient(ItemID.BottledWater)
              .AddTile(TileID.AlchemyTable)
              .Register();

            Recipe.Create(ModContent.ItemType<Gravitation_Control_Potion>(), 1)
              .AddRecipeGroup("anyStrangePlants")
              .AddIngredient(ItemID.BottledWater)
              .AddTile(TileID.AlchemyTable)
              .Register();

            Recipe.Create(ModContent.ItemType<NecroticRestauration>(), 10)
              .AddRecipeGroup("anyStrangePlants")
              .AddIngredient(ItemID.BottledWater)
              .AddTile(TileID.AlchemyTable)
              .Register();
        }
        #endregion

        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Tombstone", new int[]
            {
                ItemID.Tombstone,
                ItemID.RichGravestone1,
                ItemID.RichGravestone2,
                ItemID.RichGravestone3,
                ItemID.RichGravestone4,
                ItemID.RichGravestone5,
                ItemID.Gravestone,
                ItemID.GraveMarker,
                ItemID.CrossGraveMarker,
                ItemID.Obelisk,
                ItemID.Headstone
            });
            RecipeGroup.RegisterGroup("Tumbas", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " IronBroadsword", new int[]
            {
                ItemID.IronBroadsword,
                ItemID.LeadBroadsword
            });
            RecipeGroup.RegisterGroup("EspH", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " WarriorEmblem", new int[]
            {
                ItemID.WarriorEmblem,
                ItemID.SorcererEmblem,
                ItemID.RangerEmblem,
                ItemID.SummonerEmblem,
                ModContent.ItemType<TrowerEmblem>()
            });
            RecipeGroup.RegisterGroup("Emblemas", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " SillyBalloonPink", new int[]
            {
                ItemID.SillyBalloonPink,
                ItemID.SillyBalloonPurple,
                ItemID.SillyBalloonGreen
            });
            RecipeGroup.RegisterGroup("globoB", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " GoldBar", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("GoldBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " DemoniteBar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("CorruptBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "" + Lang.GetItemNameValue(ModContent.ItemType<Crimson_saber>()), new int[]
            {
                ModContent.ItemType<Crimson_saber>(),
                ModContent.ItemType<Corrupted_saber>()
            });
            RecipeGroup.RegisterGroup("CorruptSaber", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " WhiteString", new int[]
            {
                ItemID.RedString,
                ItemID.OrangeString,
                ItemID.YellowString,
                ItemID.LimeString,
                ItemID.GreenString,
                ItemID.TealString,
                ItemID.CyanString,
                ItemID.SkyBlueString,
                ItemID.BlueString,
                ItemID.PurpleString,
                ItemID.VioletString,
                ItemID.PinkString,
                ItemID.BrownString,
                ItemID.WhiteString,
                ItemID.RainbowString,
                ItemID.BlackString

            });
            RecipeGroup.RegisterGroup("String", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " FisherofSouls", new int[]
            {
                ItemID.FisherofSouls,
                ItemID.Fleshcatcher
            });
            RecipeGroup.RegisterGroup("CorruptCaña", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " LightsBane", new int[]
            {
                ItemID.LightsBane,
                ItemID.BloodButcherer
            });
            RecipeGroup.RegisterGroup("anyCorruptSwords", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "StrangePlant1", new int[]
            {
                ItemID.StrangePlant1,
                ItemID.StrangePlant2,
                ItemID.StrangePlant3,
                ItemID.StrangePlant4
            });
            RecipeGroup.RegisterGroup("anyStrangePlants", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "BlueBrick", new int[]
            {
                ItemID.GreenBrick,
                ItemID.BlueBrick,
                ItemID.PinkBrick
            });
            RecipeGroup.RegisterGroup("anyDungeonBrick", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Ale", new int[]
            {
                ItemID.Ale,
                ItemID.Sake
            });
            RecipeGroup.RegisterGroup("anyBeer", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "PumpkinPie", new int[]
            {
                ItemID.PumpkinPie,
                ItemID.BowlofSoup,
                ItemID.CookedMarshmallow,
                ItemID.ChristmasPudding,
                ItemID.SugarCookie,
                ItemID.GingerbreadCookie,
                ItemID.PadThai,
                ItemID.Pho,
                ItemID.CookedFish,
                ItemID.CookedShrimp,
                ItemID.Sashimi,
                ItemID.GrubSoup,
                ItemID.Bacon
            });
            RecipeGroup.RegisterGroup("anyFood", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Intestine", new int[]
            {
                ModContent.ItemType<Intestine>(),
                ModContent.ItemType<SoulHarvester>(),
            });
            RecipeGroup.RegisterGroup("anyCorruptWhip", group);

        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];


                if (recipe.TryGetResult(ItemID.JungleHat, out _))
                {
                    recipe.RemoveIngredient(ItemID.JungleSpores);
                    recipe.AddIngredient(ModContent.ItemType<JungleBar>(), 3);
                }
                if (recipe.TryGetResult(ItemID.JungleShirt, out _))
                {
                    recipe.RemoveIngredient(ItemID.JungleSpores);
                    recipe.RemoveIngredient(ItemID.Stinger);
                    recipe.AddIngredient(ModContent.ItemType<JungleBar>(), 6);
                }
                if (recipe.TryGetResult(ItemID.JunglePants, out _))
                {
                    recipe.RemoveIngredient(ItemID.JungleSpores);
                    recipe.RemoveIngredient(ItemID.Vine);
                    recipe.AddIngredient(ModContent.ItemType<JungleBar>(), 3);
                }
                if (recipe.TryGetResult(ItemID.NecroHelmet, out _))
                {
                    recipe.RemoveIngredient(ItemID.Cobweb);
                    recipe.RemoveIngredient(ItemID.Bone);
                    recipe.AddIngredient(ModContent.ItemType<BoneBar>(), 18);
                }
                if (recipe.TryGetResult(ItemID.NecroBreastplate, out _))
                {
                    recipe.RemoveIngredient(ItemID.Cobweb);
                    recipe.RemoveIngredient(ItemID.Bone);
                    recipe.AddIngredient(ModContent.ItemType<BoneBar>(), 29);
                }
                if (recipe.TryGetResult(ItemID.NecroGreaves, out _))
                {
                    recipe.RemoveIngredient(ItemID.Cobweb);
                    recipe.RemoveIngredient(ItemID.Bone);
                    recipe.AddIngredient(ModContent.ItemType<BoneBar>(), 23);
                }

                if (RemnantOfTheAncientsMod.CalamityMod != null)
                {
                    if (recipe.TryGetResult(ModContent.ItemType<CurcedShotGun>(), out _))
                    {
                        recipe.RemoveTile(TileID.MythrilAnvil);
                        recipe.AddTile(TileID.Anvils);
                    }
                    if (recipe.TryGetResult(ModContent.ItemType<Paintshotgun>(), out _))
                    {
                        recipe.RemoveTile(TileID.MythrilAnvil);
                        recipe.AddTile(TileID.Anvils);
                    }
                    if (recipe.TryGetResult(ModContent.ItemType<Ichor_asault_rifle>(), out _))
                    {
                        recipe.RemoveTile(TileID.MythrilAnvil);
                        recipe.AddTile(TileID.Anvils);
                    }
                }
            }
        }
    }
}
	





		
	