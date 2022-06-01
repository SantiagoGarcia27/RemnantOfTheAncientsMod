using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.ammo
{
	public class EndlessVenom : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Venom Quiver");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kompletny kołczan jadu");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Carquois de venin sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Carcaj Venenoso eterno");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = 2;
			Item.shoot = ProjectileID.VenomArrow;   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 26f;                  //The speed of the projectile
			Item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}


        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ItemID.VenomArrow,3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}