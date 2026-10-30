using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Masks
{
    [AutoloadEquip(EquipType.Head)]
    public class CrusherMask : MaskItem {}

    [AutoloadEquip(EquipType.Head)]
    public class DesertAMask : MaskItem {}

    [AutoloadEquip(EquipType.Head)]
    public class FrozenMask : MaskItem {}

    [AutoloadEquip(EquipType.Head)]
    public class InfernalMask : MaskItem {}

    public abstract class MaskItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
        }

        public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            color = drawPlayer.GetImmuneAlphaPure(Color.White, shadow);
        }
    }
}