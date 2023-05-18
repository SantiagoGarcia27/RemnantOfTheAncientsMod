using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Tiles;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class TuxoniteBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Bar");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Barre de Tuxonite");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lingote de Tusonita");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
            Item.CloneDefaults(ItemID.PlatinumBar);
            Item.createTile = TileType<TuxoniteBarB>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddRecipeGroup("GoldBar", 1)
			.AddIngredient(ItemID.Gel, 3)
			.AddIngredient(ItemID.GlowingMushroom, 2)
			.AddTile(TileID.Solidifier)
			.Register();

			CreateRecipe()
			.AddIngredient(ItemType<TuxoniteOre>(), 4)
			.AddTile(TileID.Solidifier)
			.Register();
		}
	}
}