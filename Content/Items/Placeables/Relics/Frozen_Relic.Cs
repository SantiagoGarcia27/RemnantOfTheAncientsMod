using RemnantOfTheAncientsMod.Content.Tiles.Master_Relic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Relics
{
    internal class Frozen_Relic : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Frozen_Relic_Tile>(), 0);
            Item.width = 30;
            Item.height = 44;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }

        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Frozen Assaulter relic");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Relique de l'Agresseur gelé");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Reliquia del Asaltante Congelado");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
}