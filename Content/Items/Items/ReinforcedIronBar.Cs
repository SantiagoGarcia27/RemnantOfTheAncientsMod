using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Tiles;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class ReinforcedIronBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 100;
			Item.rare = ItemRarityID.Orange;
			Item.createTile = TileType<ReinforcedIronBarB>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<ReinforcedIronOre>(), 3)
			.AddTile(TileID.Furnaces)
			.Register();

            CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 2)
            .AddTile(TileID.Furnaces)
            .Register();
        }
	}
}