using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    [AutoloadEquip(EquipType.Balloon)]
    public class Infernal_core: ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public static readonly int MoveSpeedBonus = 50;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(10, MoveSpeedBonus);
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
            player.GetDamage(DamageClass.Melee) += .10f;
            player.GetModPlayer<RemnantPlayer>().MeleeBuffInflict.Add(ModContent.BuffType<Hell_Fire>());
            player.moveSpeed += MoveSpeedBonus/100f;
            player.buffImmune[BuffID.OnFire] = true;
        }
    }
}