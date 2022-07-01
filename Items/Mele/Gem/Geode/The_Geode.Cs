using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Mele.Gem.Geode
{
	public class The_Geode : ModItem
	{
		public override void SetStaticDefaults()
		{
			
	        DisplayName.SetDefault("The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "The Geode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "La géode");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "La geoda");
		}
		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1200;
			Item.rare = 3;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"Amethyst_Sword",1)
            .AddIngredient(null,"Topaz_Sword",1)
			.AddIngredient(null,"Sapphire_Sword",1)
			.AddIngredient(null,"Ruby_Sword",1)
			.AddIngredient(null,"Emerald_Sword",1)
			.AddIngredient(null,"Diamond_Sword",1)
			.AddIngredient(null,"BoneBar",2)
			.AddIngredient(ItemID.StoneBlock,10)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
