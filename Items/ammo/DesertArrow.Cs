using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Projectiles;

namespace opswordsII.Items.ammo
{
	public class DesertArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Arrow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Desert Arrow");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Flèche du désert");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Flecha Desertica");
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;   
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 3.5f;
			Item.value = 10;
			Item.rare = 2;
			Item.shoot = ModContent.ProjectileType<DesertArrowP>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 26f;                  //The speed of the projectile
			Item.ammo = AmmoID.Arrow;              //The ammo class this ammo belongs to.
		}

		// Give each bullet consumed a 20% chance of granting the Wrath buff for 5 seconds

        public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.WoodenArrow, 200)
			.AddIngredient(null,"Sand_escense", 1)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}