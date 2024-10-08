using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele.Gem
{
	public class Diamond_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Diamond Sword");	
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Diamentowy miecz");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "épée de diamant");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada de Diamante");
            
			
		}
		public override void SetDefaults()
		{
			Item.damage = 23;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.knockBack = 1;
			Item.value = 1900;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;

	
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.PlatinumBar,8)
            .AddIngredient(ItemID.Diamond,10)
			.AddTile(TileID.Anvils)	
			.Register();
		}
	}
}