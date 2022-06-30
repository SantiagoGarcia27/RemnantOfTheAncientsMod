using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Items
{
	public class SharkTooth : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shark Tooth");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ząb rekina");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Dent de requin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Diente de tiburon");
		}

			public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 99; //Cantidad maxima en un espacio de inventario
			Item.value = 500; //Precio
			Item.rare = 1;
		}

		public override void AddRecipes()
		{
        }
	}
}
