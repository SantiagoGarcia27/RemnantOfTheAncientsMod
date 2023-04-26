using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Tiles;

namespace RemnantOfTheAncientsMod.Items.Bloques.Trophy
{
	public class DesertTrophy : ModItem
	{
		public override void SetStaticDefaults() {
//DisplayName.SetDefault("Desert Annihilator Trophy");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Desert Aniquilator Trophy");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Trophée d'aniquilateur du désert");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Trofeo Aniquilador del desierto");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
Item.width = 30;
Item.height = 30;
Item.maxStack = 99;
Item.useTurn = true;
Item.autoReuse = true;
Item.useAnimation = 15;
Item.useTime = 10;
Item.useStyle = ItemUseStyleID.Swing;
Item.consumable = true;
Item.value = 50000;
Item.rare = ItemRarityID.Blue;
Item.createTile = TileType<BossTrophy>();
Item.placeStyle = 0;
		}
	}
}