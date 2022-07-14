using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

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
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 99;
			Item.value = 100;
			Item.rare = ItemRarityID.Orange;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = TileType<Tiles.NightBarB>();
		}

		public override void AddRecipes()
		{
			CreateRecipe(4)
			.AddIngredient(ItemID.HellstoneBar)
			.AddRecipeGroup("CorruptBar")
			.AddIngredient(ItemType<JungleBar>())
			.AddIngredient(ItemType<BoneBar>())
			.AddTile(TileID.DemonAltar)
			.Register();

		}
	}
}