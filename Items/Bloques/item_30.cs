using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Bloques
{
    internal class item_30 : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.item_30_tile>(), 0);

            Item.width = 30;
            Item.height = 46;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reliquia de prueba N3");
            Tooltip.SetDefault("Esta es una reliquia de prueba n3");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
}
