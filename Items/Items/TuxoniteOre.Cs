using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Items
{
	public class TuxoniteOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tuxonite Ore"); 
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Ruda Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Minerai de tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Mena de Tusonita");
            


			/*
			 DisplayName.AddTranslation(GameCulture.Russian, "Фрагмент брони");
			 DisplayName.AddTranslation(GameCulture.Chinese, "护甲碎片");*/
		}

			public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 99; //Cantidad maxima en un espacio de inventario
			Item.value = 100; //Precio
			Item.rare = 0;

        }
	}
}
