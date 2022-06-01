using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.ammo
{
	public class bullettest : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wrath Slime Bullet"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Pocisk Szlamu Gniewu");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Balle Visqueuse de Colère");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bala de Slime Furioso");
		}

		public override void SetDefaults()
		{
			Item.damage = 6;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = 2;
			Item.shoot = ModContent.ProjectileType<bullettestp>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 16f;                  //The speed of the projectile
			Item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}

		// Give each bullet consumed a 20% chance of granting the Wrath buff for 5 seconds
		public override void OnConsumeAmmo(Player player)
		{
			if (Main.rand.NextBool(5))
			{
				player.AddBuff(BuffID.Wrath, 300);
			}
		}

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.MusketBall, 100)
			.AddIngredient(ItemID.Meteorite, 5)
			.AddIngredient(ItemID.PinkGel, 5)
			.AddTile(TileID.Hellforge)
			.Register();
		}
	}
}