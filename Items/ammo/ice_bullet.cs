using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.ammo
{
	public class ice_bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Bullet"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Frozen Bullet");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle gelée");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bala Helada");
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 30;
			Item.rare = 2;
			Item.shoot = ModContent.ProjectileType<ice_bulletP>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 10f;                  //The speed of the projectile
			Item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.MusketBall, 100)
			.AddIngredient(ItemID.LivingFrostFireBlock, 1)
			.AddTile(TileID.IceMachine)
			.Register();
		}
	}
}