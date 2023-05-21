using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Furniture
{ 
    public class TuxoniteToilet : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Tuxonite Toilet");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Toilette en tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inodoro de tusonita");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults() 
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Tuxonite.TuxoniteToilet>());
			Item.value = 150;
			Item.maxStack = 99;
			Item.width = 16;
			Item.height = 24;
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.Toilet)
				.AddIngredient<TuxoniteBar>(2)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
