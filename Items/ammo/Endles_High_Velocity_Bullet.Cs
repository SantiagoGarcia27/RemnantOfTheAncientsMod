using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.ammo
{
	public class Endles_High_Velocity_Bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless High Velocity Pouch");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Kieszeń Endless High Velocity");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pochette à haute vitesse sans Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Saco Infinito de Balas de Alta Velocidad");
		}

		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = 4;
			Item.shoot = ProjectileID.BulletHighVelocity;//The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 16f;                 //The speed of the projectile
			Item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		
	
		}

        public override void AddRecipes()
		{
			CreateRecipe()
		    .AddIngredient(ItemID.HighVelocityBullet,3996)
			.AddTile(TileID.CrystalBall)
			.Register();
		}
	}
}