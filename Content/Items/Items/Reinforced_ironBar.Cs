using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Tiles;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class Reinforced_ironBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Reinforced iron Bar");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Wzmocniony pręt żelazny");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Barre de fer renforcé");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lingote de hierro reforzado");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
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
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 100;
			Item.rare = ItemRarityID.Orange;
			Item.createTile = TileType<Reinforced_ironBarB>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemType<Reinforced_iron_ore>(), 3)
			.AddTile(TileID.Furnaces)
			.Register();

            CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 2)
            .AddTile(TileID.Furnaces)
            .Register();
        }
	}
}