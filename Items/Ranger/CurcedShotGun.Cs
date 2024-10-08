using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;

namespace opswordsII.Items.Ranger
{
public class CurcedShotGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Shotgun");
		    DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Escopeta Maldita");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Cholerna strzelba");  
			Tooltip.SetDefault(""
			+ "\n5% chance of not spending ammo ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), ""
			+ "\n5% szans na nie wydawanie amunicji ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), ""
			+ "\n5% de chances de ne pas dépenser de munitions ");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), ""
			+ "\nProbabilidad del 5% de no gastar munición ");
		}
		public override void SetDefaults()
		{
            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; //Tamaño de Ancho
            Item.height = 40; //Tamaño de Alto
            Item.useTime = 46; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
            Item.useAnimation = 46;
            Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 100000; //Precio
            Item.rare = 4;
   		    Item.UseSound = SoundID.Item36;
            Item.autoReuse = true;
            Item.shoot = 9; //dejar en 10 para que dispare balas normales
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;
			
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const int NumProjectiles = 8; // The humber of projectiles that this gun will shoot.

			for (int i = 0; i < NumProjectiles; i++)
			{
				// Rotate the velocity randomly by 30 degrees at max.
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

				// Decrease velocity randomly for nicer visuals.
				newVelocity *= 1f - Main.rand.NextFloat(0.3f);

				// Create a projectile.
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
			}

			return false;
		}
		 /*  public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .05f;
		}*/
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.CursedFlame,40)
			.AddIngredient(ItemID.Shotgun,1)
			.AddTile(TileID.MythrilAnvil)
			
			.Register();		
	   }
	}
}
