using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele.Gem
{
	public class Emerald_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Emerald Sword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Szmaragdowy miecz");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée d'émeraude");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de esmeralda");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			Item.damage = 21;
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
			.AddIngredient(ItemID.TungstenBar,8)
            .AddIngredient(ItemID.Emerald,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}