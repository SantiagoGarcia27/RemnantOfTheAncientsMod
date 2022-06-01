using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;

namespace opswordsII.Items.Ranger
{
	public class VampireGun: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vampire Gun");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Pistolet Wampirów");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vampire Gun");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Pistola Vampírica");
			Tooltip.SetDefault("healing gun.");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "leczniczy pistolet");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "pistolet guérisseur");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "una pistola curativa");
		}
		public override void SetDefaults()
		{
        Item.damage = 50;
        Item.DamageType = DamageClass.Ranged;
        Item.width =8; //Tamaño de Ancho
        Item.height = 2; //Tamaño de Alto
        Item.useTime = 4; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
        Item.useAnimation = 12;
        Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
        Item.noMelee = true;
        Item.knockBack = 5;
        Item.value = 10000; //Precio
        Item.rare = 8;
		Item.UseSound = SoundID.Item38;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 2f;
        Item.useAmmo = AmmoID.Bullet;
        Item.reuseDelay = 14;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat() <= (float)1 / 2) Projectile.NewProjectile(source, position, velocity*4, ProjectileID.VampireKnife, damage, 1, player.whoAmI);

			return true;
		}

       /* public override bool CanConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < Item.useAnimation - 2);
		}*/


		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.TheUndertaker, 1)
            .AddIngredient(ItemID.VampireKnives, 1)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
 