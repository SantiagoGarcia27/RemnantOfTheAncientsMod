using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using opswordsII.Projectiles;
using Terraria.DataStructures;

namespace opswordsII.Items.Ranger
{
	public class FrostShark : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Shark");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Frozen Shark");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Requin congelé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tiburón Congelado");
		}
		public override void SetDefaults()
		{
            Item.damage = 22;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; //Tamaño de Ancho
            Item.height = 40; //Tamaño de Alto
            Item.useTime = 9; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
            Item.useAnimation = 9;
            Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 1000; //Precio
            Item.rare = 5;
   		    Item.UseSound = SoundID.Item31;
            Item.autoReuse = true;
            Item.shoot = 5; //dejar en 10 para que dispare balas normales
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;

			/*Mod CalamityMod = ModLoader.TryGetMod("CalamityMod");
    		if (CalamityMod != null)
			Item.damage = 24;*/
			
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			
			if (Main.rand.NextFloat () <= (float) 1 / 10) Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ice_bulletP>(), damage, 1, player.whoAmI);

			/*Mod CalamityMod = ModLoader.TryGetMod("CalamityMod");
    		if (CalamityMod != null)

			if (Main.rand.NextFloat () <= (float) 1 / 1) Projectile.NewProjectile(position.X, position.Y, speedX, speedY,ModContent.ProjectileType<frozen_p_f>(), 8, 1, player.whoAmI);
			if (Main.rand.NextFloat () <= (float) 1 / 20) Projectile.NewProjectile(position.X, position.Y, speedX, speedY,ModContent.ProjectileType<FrostBeamF>(), 30, 1, player.whoAmI);*/
			return true;
        }
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
       /*    public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .10f;
		
		}*/
	}
}
