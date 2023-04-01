using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Items.Bloques.Trophy
{
	public class FrostTrophy : ModItem
	{
		public override void SetStaticDefaults() {
DisplayName.SetDefault("Frozen Assaulter Trophy");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Frozen Assaulter Trophy");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Trophée d'assaillant gelé");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Trofeo de asaltante congelado");
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
Item.createTile = TileType<Tiles.BossTrophy>();
Item.placeStyle = 1;
		}
	}
}