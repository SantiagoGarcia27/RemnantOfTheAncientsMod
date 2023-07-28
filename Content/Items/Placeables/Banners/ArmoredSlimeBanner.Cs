using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Banners
{
	public class ArmoredSlimeBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Armored Slime Banner");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Opancerzony Szlamowy Sztandar");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bannière de Slime blindée");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Estandarte Slime Blindado");
		}
		// The //Tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 24;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.scale = 2.0f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(0, 0, 10, 0);
			//Item.createTile = ModContent.TileType<MonsterBanner>();
			Item.placeStyle = 0;
		}
	}
}