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
			Item.damage = 400;
            Item.width = 26;
			Item.height = 22;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Ranged;
            Item.consumable = true;
			Item.maxStack = Base.maxStack;
			Item.value = Item.sellPrice(platinum: 100);
			Item.createTile = TileType<Tiles.TerracoinB>();
            Item.shoot = ProjectileType<TerracoinP>();     
			Item.noMelee = true;
			Item.notAmmo = true;
			Item.shootSpeed = Base.shootSpeed;
            Item.ammo = AmmoID.Coin;
			
        }
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(Base.type, 100)
			.Register();
		}
	}
}