using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Items
{
	public class JungleBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jungle Bar");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Jungle Bar");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bar de la jungle");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lingote de la jungla");
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
			Item.createTile = TileType<Tiles.JungleBarB>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.JungleSpores,2)
            .AddIngredient(ItemID.Stinger, 2)
			.AddTile(TileID.Anvils)
			.Register();
		    
        }
	}
}