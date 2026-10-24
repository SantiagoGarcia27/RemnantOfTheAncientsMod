using RemnantOfTheAncientsMod.Content.Tiles.Master_Relic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Relics
{
    public class Crusher_Relic : BaseRelic
    {
        public override int TileType => ModContent.TileType<Crusher_Relic_Tile>();
    }
    public class Desert_Relic : BaseRelic
    {
        public override int TileType => ModContent.TileType<Desert_Relic_Tile>();
    }
    public class Frozen_Relic : BaseRelic
    {
        public override int TileType => ModContent.TileType<Frozen_Relic_Tile>();
    }
    public class Tyrant_Relic : BaseRelic
    {
        public override int TileType => ModContent.TileType<Tyrant_Relic_Tile>();
    }

    public abstract class BaseRelic : ModItem
    {
        public virtual int TileType { get; set; }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Crusher_Relic_Tile>(), 0);
            Item.width = 30;
            Item.height = 44;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Master;
            Item.master = true;
            Item.value = Item.buyPrice(0, 5);
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
    }
}