using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    public class Frost_core : CoreItem
    {
        private float MagicDamageBonus = 10;
        private float MoveSpeedBonus = 150;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(25);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += MagicDamageBonus/100f;
            if (player.ZoneSnow) 
            {
                player.moveSpeed += MoveSpeedBonus/100f;

                player.buffImmune[BuffID.Chilled] = true;
                player.buffImmune[BuffID.Frozen] = true;
            }

            base.UpdateAccessory(player, hideVisual);
        }
    }
}