using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace opswordsII.Items.Magic
{
	public class StarTome : ModItem
	{
		public override void SetStaticDefaults()
		{
              DisplayName.SetDefault("Star Tome");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Star Tome");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tome étoile");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tomo de estrellas");
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 2;
			Item.rare = 2;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 19;
			Item.useStyle = 5;
			Item.shootSpeed = 14f;
			Item.useAnimation = 19;
			Item.shoot = 12;
			Item.value = 50000;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FallenStar, 30)
			.AddIngredient(ItemID.Book, 1)
			.AddTile(TileID.Bookcases)		
			.Register();
		}
	}
}