using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    public class Desert_Core: CoreItem
    {
        private readonly int MinionBonus = 1;
        private readonly float DamageBonus = 8;
        private readonly int LifeBonus = 50;
        private readonly int LifeRegenBonus = 2;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(1,8, LifeBonus);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += MinionBonus;
            if (player.ZoneDesert)
            {
                player.GetDamage(DamageClass.Generic) += DamageBonus/100f;
                player.lifeRegen += LifeRegenBonus;
                if (player.ZoneSandstorm) player.statLifeMax2 += LifeBonus;
            }

            base.UpdateAccessory(player, hideVisual);
        }
    }
}