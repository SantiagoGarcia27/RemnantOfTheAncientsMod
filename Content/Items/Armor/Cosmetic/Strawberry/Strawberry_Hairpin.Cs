using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.Global.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Strawberry
{
    [AutoloadEquip(EquipType.Head)]
    public class Strawberry_Hairpin : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.Size = new(30, 30);
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
            Item.GetGlobalItem<RemnantGlobalItem>().StyleStat = 6;
            if (Main.netMode != NetmodeID.Server)
            {
                ArmorIDs.Head.Sets.DrawFullHair[base.Item.headSlot] = true;
            }
        }
    }
}

