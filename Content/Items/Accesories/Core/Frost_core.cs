using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    [AutoloadEquip(EquipType.Balloon)]
    public class Frost_core : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(25);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = 45000;
            Item.rare = ItemRarityID.Red;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += .10f;
            if (player.ZoneSnow) 
            {
                player.moveSpeed += 1.50f;
                player.buffImmune[BuffID.Chilled] = true;
                player.buffImmune[BuffID.Frozen] = true;
            }
        }
    }
}