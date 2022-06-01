using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Items
{
	public class Neutrum_Fragment : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Neutrum Fragment");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Fragment neutrum");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Fragment de neutrum");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Fragmento de neutrum");
		}

			public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 99; //Cantidad maxima en un espacio de inventario
			Item.value = 2000; //Precio
			Item.rare = 9;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FragmentSolar,1)
            .AddIngredient(ItemID.FragmentStardust,1)
			.AddIngredient(ItemID.FragmentNebula,1)
			.AddIngredient(ItemID.FragmentVortex,1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		    
        }
	}
}