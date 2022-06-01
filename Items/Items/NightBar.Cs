using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Items
{
	public class NightBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Bar");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Sztabka nocna");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Lingot de nuit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lingote de la noche");
		}

			public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 99; //Cantidad maxima en un espacio de inventario
			Item.value = 100; //Precio
			Item.rare = 3;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = TileType<Tiles.NightBarB>();
		}

		public override void AddRecipes()
		{
			 CreateRecipe()
			.AddIngredient(ItemID.HellstoneBar,1)
            .AddRecipeGroup("CorruptBar",1)
			.AddIngredient(null,"JungleBar",1)
			.AddIngredient(null,"BoneBar",1)
			.AddTile(TileID.DemonAltar)
			.Register();
		    
        }
	}
}