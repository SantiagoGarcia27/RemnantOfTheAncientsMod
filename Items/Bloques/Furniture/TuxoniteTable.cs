using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Items.Bloques.Furniture
{
	public class TuxoniteTable : ModItem
	{
		public override void SetStaticDefaults() {
//DisplayName.SetDefault("Tuxonite Table");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tableau en tuxonite");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mesa de tusonita");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.TuxoniteTable>());
Item.value = 150;
Item.maxStack = 99;
Item.width = 38;
Item.height = 24;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
CreateRecipe()
	.AddIngredient<TuxoniteBar>(2)
	.AddTile(TileID.Anvils)
	.Register();
		}
	}
}
