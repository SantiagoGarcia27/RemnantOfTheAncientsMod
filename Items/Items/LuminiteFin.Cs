using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Items
{
	public class LuminiteFin : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Luminite Fin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Aileron de luminite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aleta de Luminita");
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
			.AddIngredient(ItemID.SharkFin,5)
            .AddIngredient(ItemID.LunarOre, 1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		    
        }
	}
}