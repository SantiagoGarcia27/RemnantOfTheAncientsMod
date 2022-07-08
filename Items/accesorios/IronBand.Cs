using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.accesorios
{
	public class IronBand : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Band");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Iron Band");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bande de Fer");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Banda de Hierro");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 1; //Cantidad maxima en un espacio de inventario
			Item.value = 500; //Precio
			Item.rare = 1;
			Item.accessory = true;
			Item.defense = 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddRecipeGroup("IronBar", 2)
			.AddTile(TileID.Anvils)
			.Register();
        }
	}
}
