using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Items
{
	public class Sand_escense : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sand Essence");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Escense piasku");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Escense de sable");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Esencia de arena");
            
		}
			public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 99; //Cantidad maxima en un espacio de inventario
			Item.value = 100; //Precio
			Item.rare = 2;
        }
	}
}