using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.ammo
{
	public class Tomb_bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grave Bullet"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Poważna kula");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle grave");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bala de tumba");
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 30;
			Item.rare = 1;
			Item.shoot = ModContent.ProjectileType<Tomb_BulletP>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 5f;                  //The speed of the projectile
			Item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddRecipeGroup("Tumbas")
			.Register();
		}
	}
}