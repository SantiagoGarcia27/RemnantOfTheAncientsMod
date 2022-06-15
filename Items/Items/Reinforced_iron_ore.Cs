using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Items
{
	public class Reinforced_iron_ore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reinforced iron ore");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wzmocniona ruda żelaza");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Minerai de fer renforcé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mineral de hierro reforzado");
		}

			public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 99; //Cantidad maxima en un espacio de inventario
			Item.value = 100; //Precio
			Item.rare = 2;
			Item.scale = 12.50f;

        }
	}
}
