using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.ammo
{
	public class Endless_Tomb_bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Grave Pouch"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Pocisk Endless Grave");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle tombale sans fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bala de tumba eterna");
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 30;
			Item.rare = 2;
			Item.shoot = ModContent.ProjectileType<Tomb_BulletP>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 5f;                  //The speed of the projectile
			Item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"Tomb_bullet",3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}