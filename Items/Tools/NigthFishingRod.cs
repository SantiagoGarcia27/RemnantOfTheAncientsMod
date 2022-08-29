using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Projectiles.Bobbers;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Tools
{
	public class NigthFishingRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Fishing Rod");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Nocna wędka");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Canne à pêche de nuit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Caña de pescar nocturna");
			Tooltip.SetDefault("");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			/* DisplayName.AddTranslation(GameCulture.Russian, "Туксонитовый топор");
			DisplayName.AddTranslation(GameCulture.Chinese, "毒x斧");*/
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.useAnimation = 8;
			Item.useTime = 8;
			Item.useStyle = 1;
			Item.UseSound = SoundID.Item1;
			Item.rare = 4;
			Item.fishingPole = 60;
			Item.shootSpeed = 20f;
			Item.shoot = ModContent.ProjectileType<NigthBobber>();
			Item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddRecipeGroup("CorruptCaña")
			.AddIngredient(ItemID.FiberglassFishingPole,1)
			.AddIngredient(ItemID.MechanicsRod,1)
			.AddIngredient(ItemID.HotlineFishingHook,1)
			.AddIngredient(ItemID.GoldenFishingRod,1)
			.AddTile(TileID.DemonAltar)
			.Register();
		}
	}
}