using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Items.Items;
using opswordsII.Items.Pociones;
using opswordsII.Items.Mele.saber;
using Terraria.GameContent.Creative;

namespace opswordsII
{
	internal class RecipeMaker : ModSystem
	{

		/*private static ModRecipe GetNewRecipe()
{

    return new CreateRecipe(ModContent.GetInstance<opswordsII>());
}*/
		
        public static void AddRecipes(Mod mod)
		{

			Uncrafting(mod);
			Swaped(mod);
			Emblemas(mod);
			PreHardmodeAccesories(mod);
			Potoions(mod);
			Ores(mod);
			NecroArmor(mod);
			JungleArmor(mod);

			{
				
					mod.CreateRecipe(ItemID.HotlineFishingHook)
				   .AddIngredient(ItemID.ReinforcedFishingPole)
				   .AddIngredient(ItemID.HellstoneBrick, 30)
				   .AddIngredient(ItemID.Obsidian, 30)
				   .AddIngredient(ItemID.LivingFireBlock, 50)
				   .AddTile(TileID.Hellforge)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.LivingFireBlock)
				   .AddIngredient(ItemID.Torch, 1)
				   .AddTile(TileID.LivingLoom)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.Shackle)
				   .AddIngredient(ItemID.IronBar, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.AnkletoftheWind)
				   .AddIngredient(ItemID.JungleSpores, 10)
				   .AddIngredient(ItemID.Vine, 3)
				   .AddIngredient(ItemID.PinkGel, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.FeralClaws)
				   .AddIngredient(ItemID.JungleSpores, 5)
				   .AddIngredient(ItemID.Stinger, 5)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.LuckyHorseshoe)
				   .AddIngredient(ItemID.GoldBar, 10)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.Aglet)
				   .AddRecipeGroup("IronBar", 2)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.PocketMirror)
				   .AddIngredient(ItemID.GoldBar, 5)
				   .AddIngredient(ItemID.Glass, 3)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.Bezoar)
				   .AddIngredient(ItemID.JungleSpores, 5)
				   .AddIngredient(ItemID.Stinger, 5)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.FlyingCarpet)
				   .AddIngredient(ItemID.AncientCloth, 5)
				   .AddTile(TileID.Loom)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.NaturesGift)
				   .AddIngredient(ItemID.JungleRose, 1)
				   .AddIngredient(ItemID.ManaCrystal, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.CobaltShield)
				   .AddIngredient(ItemID.CobaltBar, 5)
				   .AddTile(TileID.MythrilAnvil)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.SharkToothNecklace)
				   .AddIngredient(null, "SharkTooth", 3)
				   .AddIngredient(ItemID.Chain, 1)
				   .AddTile(TileID.WorkBenches)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.MagmaStone)
				   .AddIngredient(ItemID.StoneBlock, 100)
				   .AddIngredient(ItemID.LavaBucket, 1)
				   .AddTile(TileID.Hellforge)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.ObsidianRose)
				   .AddIngredient(ItemID.Obsidian, 10)
				   .AddTile(TileID.Hellforge)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.LavaCharm)
				   .AddIngredient(ItemID.IronBar, 10)
				   .AddIngredient(ItemID.LavaBucket, 3)
				   .AddTile(TileID.Hellforge)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.BandofStarpower)
				   .AddIngredient(ItemID.PanicNecklace, 1)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.PanicNecklace)
				   .AddIngredient(ItemID.BandofStarpower, 1)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.ClothierVoodooDoll)
				   .AddIngredient(ItemID.GuideVoodooDoll, 1)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.TruffleWorm)
				   .AddIngredient(ItemID.Worm, 1)
				   .AddIngredient(ItemID.GlowingMushroom, 15)
				   .AddTile(TileID.Autohammer)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.GuideVoodooDoll)
				   .AddIngredient(ItemID.ClothierVoodooDoll, 1)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{

				mod.CreateRecipe(ItemID.WandofSparking)
				   .AddIngredient(ItemID.Wood, 30)
				   .AddIngredient(ItemID.Torch, 20)
				   .AddTile(TileID.WorkBenches)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.RodofDiscord)
				   .AddIngredient(ItemID.HallowedBar, 15)
				   .AddIngredient(ItemID.PixieDust, 80)
				   .AddIngredient(ItemID.SoulofLight, 30)
				   .AddIngredient(ItemID.SoulofSight, 5)
				   .AddTile(TileID.MythrilAnvil)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.Shuriken, 50)
				   .AddRecipeGroup("IronBar")
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.ThrowingKnife, 50)
				   .AddRecipeGroup("IronBar")
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.WoodenBoomerang)
				   .AddRecipeGroup("IronBar", 2)
				   .AddIngredient(ItemID.Wood, 10)
				   .AddTile(TileID.WorkBenches)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.Spear)
				   .AddRecipeGroup("IronBar", 2)
				   .AddRecipeGroup("GoldBar", 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.EnchantedSword)
				   .AddRecipeGroup("EspH", 1)
				   .AddIngredient(ItemID.FallenStar, 10)
				   .AddIngredient(ItemID.SoulofLight, 10)
				   .AddIngredient(ItemID.LightShard, 1)
				   .AddTile(TileID.MythrilAnvil)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.DPSMeter)
				   .AddRecipeGroup("IronBar", 3)
				   .AddIngredient(ItemID.Wire, 20)
				   .AddTile(TileID.TinkerersWorkbench)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.PlatinumCoin, 100)
				   .AddIngredient(ModContent.ItemType<Terracoin>(), 1)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.PlatinumCoin, 100)
				   .AddIngredient(ModContent.ItemType<NightBar>(), 10)
				   .AddTile(TileID.DemonAltar)
				   .Register(); }
		}


		private static void NecroArmor(Mod mod)
		{

			{
				mod.CreateRecipe(ItemID.NecroHelmet)
				   .AddIngredient(ModContent.ItemType<BoneBar>(), 18)
				   .AddTile(TileID.WorkBenches)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.NecroBreastplate)
				   .AddIngredient(ModContent.ItemType<BoneBar>(), 29)
				   .AddTile(TileID.WorkBenches)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.NecroGreaves)
				   .AddIngredient(ModContent.ItemType<BoneBar>(), 23)
				   .AddTile(TileID.WorkBenches)
				   .Register(); }
		}

		private static void JungleArmor(Mod mod)
		{
			{
				mod.CreateRecipe(ItemID.JungleHat)
				   .AddIngredient(ModContent.ItemType<JungleBar>(), 3)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.JungleShirt)
				   .AddIngredient(ModContent.ItemType<JungleBar>(), 6)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.JunglePants)
					.AddIngredient(ModContent.ItemType<JungleBar>(), 3)
					.AddTile(TileID.Anvils)
					.Register(); }

		}

		private static void Potoions(Mod mod)
		{
			{
				mod.CreateRecipe(ModContent.ItemType<EndlessCI>())
					.AddIngredient(ModContent.ItemType<EndlessMR>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessRa>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessSu>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessSwif>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessWr>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessEndu>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessIron>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessLF>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessMP>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessRe>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessInf>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessArch>(), 1)
					.AddTile(TileID.Bottles)
					.Register(); }

			{
				mod.CreateRecipe(ModContent.ItemType<EndlessMInf>())
					.AddIngredient(ModContent.ItemType<EndlessSp>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessSh>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessNig>(), 1)
					.AddIngredient(ModContent.ItemType<EndlessMi>(), 1)
					.AddTile(TileID.Bottles)
					.Register(); }
		}

		private static void Mirrors(Mod mod)
		{
			{
				mod.CreateRecipe(ItemID.MagicMirror)
				   .AddRecipeGroup("IronBar", 5)
				   .AddIngredient(ItemID.Glass, 3)
				   .AddIngredient(ItemID.FallenStar, 3)
				   .AddIngredient(ItemID.RecallPotion, 5)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.MagicMirror)
				   .AddIngredient(ItemID.IceMirror, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.IceMirror)
				   .AddIngredient(ItemID.MagicMirror, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
		}




		private static void Ores(Mod mod)
		{
			{
				mod.CreateRecipe(ModContent.ItemType<TuxoniteBar>())
				   .AddIngredient(ModContent.ItemType<TuxoniteOre>(), 4)
				   .AddTile(TileID.Furnaces)
				   .Register(); }
		}

		private static void Swaped(Mod mod)
		{
			{
				mod.CreateRecipe(ItemID.CrimsonHeart)
				   .AddIngredient(ItemID.ShadowOrb)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.ShadowOrb)
				   .AddIngredient(ItemID.CrimsonHeart)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }

			{
				mod.CreateRecipe(ItemID.SoulofNight)
				   .AddIngredient(ItemID.SoulofLight)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.SoulofLight)
				   .AddIngredient(ItemID.SoulofNight)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.CursedFlame)
				   .AddIngredient(ItemID.Ichor)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.Ichor)
				   .AddIngredient(ItemID.CursedFlame)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.AdamantiteBar)
				   .AddIngredient(ItemID.TitaniumBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.TitaniumBar)
				   .AddIngredient(ItemID.AdamantiteBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.PalladiumBar)
				   .AddIngredient(ItemID.CobaltBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.CobaltBar)
				   .AddIngredient(ItemID.PalladiumBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.OrichalcumBar)
				   .AddIngredient(ItemID.MythrilBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.MythrilBar)
				   .AddIngredient(ItemID.OrichalcumBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.PlatinumBar)
				   .AddIngredient(ItemID.GoldBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.GoldBar)
				   .AddIngredient(ItemID.PlatinumBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.TungstenBar)
				   .AddIngredient(ItemID.SilverBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.SilverBar)
				   .AddIngredient(ItemID.TungstenBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.LeadBar)
				   .AddIngredient(ItemID.IronBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.IronBar)
				   .AddIngredient(ItemID.LeadBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.CopperBar)
				   .AddIngredient(ItemID.TinBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.TinBar)
				   .AddIngredient(ItemID.CopperBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.CrimtaneBar)
				   .AddIngredient(ItemID.DemoniteBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.DemoniteBar)
				   .AddIngredient(ItemID.CrimtaneBar)
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }


		}

		private static void Uncrafting(Mod mod) {
			{
				mod.CreateRecipe(ModContent.ItemType<JungleBar>(), 6)
				   .AddIngredient(ItemID.BladeofGrass, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.HellstoneBar, 10)
				   .AddIngredient(ItemID.MoltenHelmet, 1)
				   .AddTile(TileID.Hellforge)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.HellstoneBar, 20)
				   .AddIngredient(ItemID.MoltenBreastplate, 1)
				   .AddTile(TileID.Hellforge)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.HellstoneBar, 15)
				   .AddIngredient(ItemID.MoltenGreaves, 1)
				   .AddTile(TileID.Hellforge)
				   .Register(); }
			{
				mod.CreateRecipe(ModContent.ItemType<JungleBar>(), 3)
				   .AddIngredient(ItemID.JungleHat, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ModContent.ItemType<JungleBar>(), 6)
				   .AddIngredient(ItemID.JungleShirt, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ModContent.ItemType<JungleBar>(), 3)
				   .AddIngredient(ItemID.JunglePants, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ModContent.ItemType<BoneBar>(), 23)
				 .AddIngredient(ItemID.NecroGreaves, 1)
				 .AddTile(TileID.Anvils)
				 .Register(); }
			{
				mod.CreateRecipe(ModContent.ItemType<BoneBar>(), 18)
				   .AddIngredient(ItemID.NecroHelmet, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ModContent.ItemType<BoneBar>(), 29)
				   .AddIngredient(ItemID.NecroBreastplate, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
		}

		private static void Emblemas(Mod mod) {
			{
				mod.CreateRecipe(ItemID.SummonerEmblem)
				   .AddRecipeGroup("Emblemas")
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.SorcererEmblem)
				   .AddRecipeGroup("Emblemas")
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.WarriorEmblem)
				   .AddRecipeGroup("Emblemas")
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.RangerEmblem)
				   .AddRecipeGroup("Emblemas")
				   .AddTile(TileID.AlchemyTable)
				   .Register(); }
		}

		private static void PreHardmodeAccesories(Mod mod) {
			{
				mod.CreateRecipe(ItemID.CloudinaBottle)
				   .AddIngredient(ItemID.Cloud, 15)
				   .AddIngredient(ItemID.RainCloud, 5)
				   .AddIngredient(ItemID.Bottle, 1)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.SandstorminaBottle)
				   .AddIngredient(ItemID.CloudinaBottle, 1)
				   .AddIngredient(ItemID.SandBlock, 30)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.BlizzardinaBottle)
				   .AddIngredient(ItemID.CloudinaBottle, 1)
				   .AddIngredient(ItemID.SnowBlock, 30)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.TsunamiInABottle)
				   .AddIngredient(ItemID.CloudinaBottle, 1)
				   .AddIngredient(ItemID.BottledWater, 2)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.HermesBoots)
				   .AddIngredient(null, "Boot", 1)
				   .AddIngredient(ItemID.SwiftnessPotion, 2)
				   .AddIngredient(ItemID.Feather, 2)
				   .AddTile(TileID.Loom)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.FlurryBoots)
				   .AddIngredient(ItemID.HermesBoots, 1)
				   .AddIngredient(ItemID.SnowBlock, 10)
				   .AddTile(TileID.Loom)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.FlurryBoots)
				   .AddIngredient(ItemID.HermesBoots, 1)
				   .AddIngredient(ItemID.BottledWater, 1)
				   .AddTile(TileID.Loom)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.IceSkates)
				   .AddIngredient(null, "Boot", 1)
				   .AddIngredient(ItemID.IronBar, 2)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.LuckyHorseshoe)
				   .AddRecipeGroup("GoldBar", 5)
				   .AddTile(TileID.Anvils)
				   .Register(); }
			{
				mod.CreateRecipe(ItemID.CelestialMagnet)
				   .AddIngredient(ItemID.LuckyHorseshoe, 1)
				   .AddIngredient(ItemID.ManaCrystal, 5)
				   .AddRecipeGroup("GoldBar", 5)
				   .AddTile(TileID.Anvils)
				   .Register(); }
		}

		public static RecipeGroup RecipeGroup1;
		public override void Unload()
		{
			RecipeGroup1 = null;
		}
		public override void AddRecipeGroups() {
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
					ItemID.SummonerEmblem
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
			}
			/* public static Color GetLightColor(Vector2 position)
			 {
				 return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
			 }*/

		}
	}