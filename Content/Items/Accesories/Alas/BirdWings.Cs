using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Alas
{
    [AutoloadEquip(EquipType.Wings)]
    public class BirdWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(39, 5f, 0.2f);
        }
        int MovmentSpeedBonus = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MovmentSpeedBonus);
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 0, 7, 0);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.35f;
            ascentWhenRising = 0.05f;
            maxCanAscendMultiplier = 0.2f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed *= 1.10f;
        }
    }
}