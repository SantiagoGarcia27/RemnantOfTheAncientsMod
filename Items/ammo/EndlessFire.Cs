using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.ammo
{
	public class EndlessFire : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Flaming Pouch"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Endless Flaming Bullet");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle enflammée sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Saco Infinito de Bala llameante ");
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = 2;
			Item.shoot = ModContent.ProjectileType<Fibullet>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 7f;                  //The speed of the projectile
			Item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}

		// Give each bullet consumed a 20% chance of granting the Wrath buff for 5 seconds
		

        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(null,"FireBullet",3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}