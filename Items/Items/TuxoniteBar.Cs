using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Items
{
	public class TuxoniteBar : ModItem
	{
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Tuxonite Bar");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Bar Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Barre de Tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lingote de Tusonita");
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
Item.rare = ItemRarityID.White;
Item.createTile = TileType<Tiles.TuxoniteBarB>();
		}

		public override void AddRecipes()
		{
CreateRecipe()
 .AddRecipeGroup("GoldBar", 1)
.AddIngredient(ItemID.Gel, 3)
.AddIngredient(ItemID.GlowingMushroom, 2)
.AddTile(TileID.Furnaces)
.Register();
		}
	}
}