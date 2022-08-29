using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Items
{
	public class Terracoin : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Moneta Terra");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce de monnaie Terra");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Moneda terra");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 22;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(platinum: 100);
			Item.createTile = TileType<Tiles.TerracoinB>();
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.PlatinumCoin, 100)
			.Register();
		}
	}
}