using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.DataStructures;

namespace opswordsII.Items.Ranger
{
	public class Paintshotgun: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Paint Shotgun");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Maluj strzelbę");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Fusil à peinture");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Escopeta de pintura");
		}
		public override void SetDefaults()
		{
        Item.damage = 30;
        Item.DamageType = DamageClass.Ranged;
        Item.width =8; //Tamaño de Ancho
        Item.height = 2; //Tamaño de Alto
        Item.useTime = 36; //mientras mas alto sea el useTime mas lenta será el arma. Usa un bajo UseTime para que el arma sea Rapida
        Item.useAnimation = 36;
        Item.useStyle = 5; //Dejar en 5 para que el personaje use el arma de forma normal
        Item.noMelee = true;
        Item.knockBack = 5;
        Item.value = 100000; //Precio
        Item.rare = 4;
		Item.UseSound = SoundID.Item36;
        Item.autoReuse = false;
        Item.shoot = 587;
        Item.shootSpeed = 8f;


		/*Mod OmniSwing = ModLoader.GetMod("OmniSwing");
    		if (OmniSwing != null)
			Item.damage = 21;*/
		}
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const int NumProjectiles = 8;

			for (int i = 0; i < NumProjectiles; i++)
			{
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));
				newVelocity *= 1f - Main.rand.NextFloat(0.3f);
				Projectile.NewProjectileDirect(source, position, newVelocity, ProjectileID.PainterPaintball, damage, knockback, player.whoAmI);
			}

			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.PainterPaintballGun, 1)
			.AddIngredient(ItemID.Shotgun, 1)
			.AddIngredient(ItemID.SoulofLight, 15)
			.AddIngredient(ItemID.RedPaint, 50)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}
