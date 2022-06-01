using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Items
{
	public class LuminiteFeathers : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Feathers");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Pióra luminescencyjn");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Plumes de luminite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Plumas de Luminita");
		}

			public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 99; //Cantidad maxima en un espacio de inventario
			Item.value = 10000; //Precio
			Item.rare = 9;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Feather,5)
            .AddIngredient(ItemID.LunarOre, 1)
			.AddTile(TileID.LunarCraftingStation)
            
			.Register();
		    
        }
	}
}
