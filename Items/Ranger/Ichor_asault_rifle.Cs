using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace opswordsII.Items.Ranger
{
public class Ichor_asault_rifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ichor Assault Rifle");
		    DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rifle de asalto Ichor");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Karabin szturmowy Ichor");  
			Tooltip.SetDefault(""
			+ "\nTransform musket bullets into ichor bullets");
			
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),""
			+ "\nTransforma las balas de mosqueton en balas de ichor");

           	Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), ""
			+ "\nPrzekształć kule muszkietowe w kule posokowe"); 
		}
		public override void SetDefaults()
		{
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; //Tamaño de Ancho
            Item.height = 40; //Tamaño de Alto
            Item.useTime = 10; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
            Item.useAnimation = 10;
            Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 100000; //Precio
            Item.rare = 4;
   		    Item.UseSound = SoundID.Item36;
			Item.autoReuse = true;
            Item.shoot = 279; //dejar en 10 para que dispare balas normales
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;
            	}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			
			if (type == ProjectileID.Bullet)
			{
				type = ProjectileID.IchorBullet;
			}
			
		}
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, 5);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Ichor,40)
			.AddIngredient(ItemID.Minishark,1)
			.AddTile(TileID.MythrilAnvil)
			.Register();		
	   }
	}
}
