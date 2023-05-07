using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Tiles;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class AncientT : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient tablet");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Starożytna tablica");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Tablette ancienne");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Tableta antigua");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
			Item.createTile = TileType<AncientTableTile>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Sandstone, 2)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}