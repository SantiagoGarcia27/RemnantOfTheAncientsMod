using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele.Gem
{
	public class Ruby_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruby Sword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Épée rubis");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de rubí");
		}
		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.knockBack = 1;
			Item.value = 1400;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;

	
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.GoldBar,8)
            .AddIngredient(ItemID.Ruby,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}