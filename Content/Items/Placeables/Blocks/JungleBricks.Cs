using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Tiles;
using Terraria;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Blocks
{
	public class JungleBricks : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
            Item.width = 26;
            Item.height = 22;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
			Item.value = new Item(ItemID.ObsidianBrick).value;
            Item.rare = new Item(ItemID.ObsidianBrick).rare;
            Item.createTile = TileType<JungleBricksT>();
		}

		public override void AddRecipes()
		{
			CreateRecipe(15)
			.AddIngredient(ItemID.StoneBlock, 15)
			.AddIngredient<JungleBar>()
			.AddTile(TileID.Furnaces)
			.Register();
		}
	}
}