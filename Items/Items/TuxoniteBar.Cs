using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Items
{
	public class TuxoniteBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Bar");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Bar Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Barre de Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lingote de Tusonita");
		}

			public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 22;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true; //Cantidad maxima en un espacio de inventario
			Item.value = 100; //Precio
			Item.rare = 0;
			Item.createTile = TileType<Tiles.TuxoniteBarB>();
			
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			 .AddRecipeGroup("GoldBar",1)
            .AddIngredient(ItemID.Gel, 3)
			.AddIngredient(ItemID.GlowingMushroom, 2)
			.AddTile(TileID.Solidifier)           
			.Register();
		    
        }
	}
}