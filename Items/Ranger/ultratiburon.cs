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
	public class ultratiburon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultrashark");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ultrarekin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Ultrarequin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ultratiburón");
			
			
			Tooltip.SetDefault("the older sister of the 4"
			+ "\n70% chance of not spending ammo ");
           Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "starsza siostra 4"
			+ "\n70% szans na nie wydawanie amunicji ");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "la sœur aînée de 4"
			+ "\n70% de chances de ne pas dépenser de munitions ");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "la hermana mayor de las 4"
			+ "\nProbabilidad del 70% de no gastar munición ");

		}
		public override void SetDefaults()
		{
            Item.damage = 130;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80; 
            Item.height = 40;
            Item.useTime = 5; 
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 20, 2, 0);
            Item.rare = 10;
   		    Item.UseSound = SoundID.Item12;
            Item.autoReuse = true;
            Item.shoot = 10; 
            Item.shootSpeed = 100f;
            Item.useAmmo = AmmoID.Bullet;
			
		}
        public override bool CanConsumeAmmo(Item ammo,Player player)
		{
			return Main.rand.NextFloat() >= .70f;
		}
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, 0);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (Main.rand.NextFloat () <= (float) 1 / 100) Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<MisilUltra>(), damage*2, 1, player.whoAmI);

			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"SuperShark",1)
			.AddIngredient(ItemID.VortexBeater, 1)
			.AddIngredient(ItemID.Uzi, 1)
			.AddIngredient(ItemID.SDMG, 1)
			.AddIngredient(null,"CelestialAmalgamate", 20)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
