using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.ammo
{
	public class EndlesSilverC : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Silver Coin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pièce d'argent sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "moneda de plata infinita");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = 7;
			Item.shoot = ProjectileID.SilverCoin;//The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 16f;                  //The speed of the projectile
			Item.ammo = AmmoID.Coin;              //The ammo class this ammo belongs to.
		
	
		}

        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ItemID.SilverCoin,3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}