using RemnantOfTheAncientsMod.Content.Tiles.Bars;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class TuxoniteBar : ModItem
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
			Item.value = new Item(ItemID.PlatinumBar).value;
            Item.rare = ItemRarityID.White;
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