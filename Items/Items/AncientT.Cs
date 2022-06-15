using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Items
{
	public class AncientT : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient tablet");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Starożytna tablica");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tablette ancienne");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tableta antigua");
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
			Item.createTile = TileType<Tiles.AncientTp>();
			
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Sandstone,2)
			.AddTile(TileID.Anvils)
			.Register();
        }
	}
}