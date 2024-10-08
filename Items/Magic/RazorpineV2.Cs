using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace opswordsII.Items.Magic
{
    public class RazorpineV2: ModItem
	{
		public override void SetStaticDefaults()
		{
              DisplayName.SetDefault("Pine Fury");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Star Tome");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Pine Fury");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Furia de pino");
            Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
            Item.CloneDefaults(ItemID.Razorpine);
			Item.damage = 35;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 2;
			Item.rare = 10;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 10;
			Item.useStyle = 5;
			Item.shootSpeed = 14f;
			Item.useAnimation = 10;
			Item.shoot = 336;
			Item.value = 500000;
			Item.autoReuse = true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 5 + Main.rand.Next(4); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(4);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(source, position, perturbedSpeed, 336, 40, knockback, player.whoAmI);
				Projectile.NewProjectile(source, position, perturbedSpeed, 206, 30, knockback, player.whoAmI);
			}

			return false; // return false to stop vanilla from calling Projectile.NewProjectile.
		}


		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Razorpine, 1)
			.AddIngredient(ItemID.LeafBlower, 1)
			.AddIngredient(ItemID.FragmentNebula, 10)
			.AddIngredient(ItemID.LunarBar, 5)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}