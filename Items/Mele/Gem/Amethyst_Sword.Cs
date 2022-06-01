using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele.Gem
{
	public class Amethyst_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amethyst Sword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ametystowy miecz");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée d'améthyste");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de Amatista");
		}
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.knockBack = 1;
			Item.value = 1000;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;

			
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.CopperBar,8)
            .AddIngredient(ItemID.Amethyst,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
