using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class MechanicalProsthesis : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
      
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(5);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = Item.buyPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.master = true;
            Item.accessory = true;
        }  
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged) *= 1.05f;
            player.GetModPlayer<RemnantPlayer>().ChargeBonus += 1;
            player.GetModPlayer<RemnantPlayer>().AutoCharge = true;
        }
    }
}