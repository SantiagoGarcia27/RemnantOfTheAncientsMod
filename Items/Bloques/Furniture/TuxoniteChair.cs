using RemnantOfTheAncientsMod.Items.Items;
using RemnantOfTheAncientsMod.Tiles;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Items.Bloques.Furniture
{
	public class TuxoniteChair : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Tuxonite chair.");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			//Item.DefaultToPlaceableTile(ModContent.TileType<TuxoniteChair>());
			Item.value = 150;
			Item.maxStack = 99;
			Item.width = 12;
			Item.height = 30;
            Item.createTile = ModContent.TileType<Tiles.TuxoniteChair>();
            Item.placeStyle = 0;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<TuxoniteBar>()
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
