using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Projectiles;

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
		Item Base = new Item(ItemID.PlatinumCoin);
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.PlatinumCoin);
			Item.damage = 400;
			Item.value = Item.sellPrice(platinum: 100);
			Item.createTile = TileType<Tiles.TerracoinB>();
            Item.shoot = ProjectileType<TerracoinP>();     
        }
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.PlatinumCoin, 100)
			.Register();
		}
	}
}