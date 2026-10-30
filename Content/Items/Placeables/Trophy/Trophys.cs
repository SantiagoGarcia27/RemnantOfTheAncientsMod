using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Tiles;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy
{
    
    public class DesertTrophy : TrophyItem { public override int PlaceStyle => 0; }
    public class FrostTrophy : TrophyItem { public override int PlaceStyle => 1; }
    public class InfernalTrophy : TrophyItem { public override int PlaceStyle => 2; }
    public class CrusherTrophy : TrophyItem { public override int PlaceStyle => 3; }

    public abstract class TrophyItem : ModItem
    {
        public virtual int PlaceStyle { get; set; }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Utils1.FormatMoney(Gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.createTile = TileType<BossTrophy>();
            Item.placeStyle = PlaceStyle;
        }
    }
}