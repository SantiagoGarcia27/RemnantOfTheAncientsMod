using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;

namespace opswordsII.Items.Ranger
{
	public class CorrupterRifle: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Corruptor Musket");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Muszkiet skaziciela");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Mousquet de corruption");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mosquete corruptor");
		
		}
		public override void SetDefaults()
		{
         Item.damage = 70;
        Item.DamageType = DamageClass.Ranged;
        Item.width =2; //Tamaño de Ancho
        Item.height = 2; //Tamaño de Alto
        Item.useTime = 30; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
        Item.useAnimation = 30;
        Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
        Item.noMelee = true;
        Item.knockBack = 5;
        Item.value = 20000; //Precio
        Item.rare = 8;
		Item.UseSound = SoundID.Item38;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 2f;
        Item.useAmmo = AmmoID.Bullet;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat() <= (float)1 / 1) Projectile.NewProjectile(source, position, velocity, ProjectileID.EatersBite, damage*2, 1, player.whoAmI);

			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Musket, 1)
            .AddIngredient(ItemID.ScourgeoftheCorruptor, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
 