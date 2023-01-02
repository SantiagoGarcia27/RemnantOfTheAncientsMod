using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Items.Items;
using RemnantOfTheAncientsMod.Tiles;

namespace RemnantOfTheAncientsMod.Items.Bloques.Furniture
{
	public class TuxoniteDoor : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 14;
			Item.height = 28;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 150;
			Item.createTile = ModContent.TileType<TuxoniteDoorClosed>();
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