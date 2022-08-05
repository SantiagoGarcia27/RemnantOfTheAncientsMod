using Terraria.Localization;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using opswordsII.Tiles;

namespace opswordsII.Items.Banners
{
	public class ArmoredzombieBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Armored Zombie Banner");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Opancerzony sztandar zombie");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bannière de Zombie Blindé");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Estandarte Zombies Blindado");
		}
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 24;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			//Item.createTile = ModContent.TileType<MonsterBanner>();
			Item.placeStyle = 1;
		}
	}
}