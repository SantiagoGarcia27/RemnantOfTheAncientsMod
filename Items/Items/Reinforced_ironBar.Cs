using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Items
{
	public class Reinforced_ironBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced iron Bar");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wzmocniony pręt żelazny");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Barre de fer renforcé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lingote de hierro reforzado");
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
			Item.rare = 3;
			Item.createTile = TileType<Tiles.Reinforced_ironBarB>();
			
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(null,"Reinforced_iron_ore",3)
			.AddTile(TileID.Furnaces)
			.Register();
		    
        }
	}
}