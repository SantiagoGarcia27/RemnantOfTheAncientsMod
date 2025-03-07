using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Terraria.ID;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    public class TrowerEmblem : ModItem
    {

        int damageBonus = 15;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(damageBonus);
        public override void SetStaticDefaults()
        {
            string TextUs = ModLoader.TryGetMod("CalamityMod", out _) ? "rogue" : "throwing";
            string TextEs = ModLoader.TryGetMod("CalamityMod", out _) ? "de picaro" : "lanzable";

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = new Item(ItemID.WarriorEmblem).value;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DamageClass damagetype = DamageClass.Throwing;
            player.GetDamage(damagetype) *= 1 + damageBonus / 100f;
        }
    }
}