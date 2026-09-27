using RemnantOfTheAncientsMod.Content.Tiles.Master_Relic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Relics
{

    internal class Tyrant_Relic : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tyrant_Relic_Tile>(), 0);
            Item.width = 30;
            Item.height = 44;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }

        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Infernal Tyrant relic");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Relique de l'Tyran infernal");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Reliquia del Tirano infernal");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
}