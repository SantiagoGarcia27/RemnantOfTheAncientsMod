using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;

namespace opswordsII.Items.Ranger.star
{
	public class StarCannonV2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Cannon Mk2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Canon étoile Mk2");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cañon de estrellas MK2");
		}
		public override void SetDefaults()
		{
            Item.damage = 105;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; //Tamaño de Ancho
            Item.height = 40; //Tamaño de Alto
            Item.useTime = 9; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
            Item.useAnimation = 9;
            Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 100000; //Precio
            Item.rare = 6;
   		    Item.UseSound = SoundID.Item10;
            Item.autoReuse = true;
           Item.shoot = 728; //dejar en 10 para que dispare balas normales
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.FallenStar;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat() <= (float)1 / 1) Projectile.NewProjectile(source, position, velocity, ProjectileID.Starfury, 300, 1, player.whoAmI);


			return true;
		}   
		  public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}
        /*   public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .30f;
		
		}	*/
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.StarCannon, 1)
			.AddIngredient(ItemID.HallowedBar, 10)
			.AddIngredient(ItemID.SoulofMight, 5)
			.AddIngredient(ItemID.SoulofFright, 5)
			.AddIngredient(ItemID.SoulofSight, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
