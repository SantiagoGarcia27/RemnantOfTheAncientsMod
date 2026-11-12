using RemnantOfTheAncientsMod.Common.RemPlayer;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    public class Infernal_core: CoreItem
    {
        public static readonly int MoveSpeedBonus = 50;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(10, MoveSpeedBonus);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += .10f;
            player.GetModPlayer<RemnantPlayer>().MeleeBuffInflict.Add(BuffID.OnFire3);
            player.buffImmune[BuffID.OnFire] = true;

            if (player.ZoneUnderworldHeight)
            {
                player.moveSpeed += MoveSpeedBonus / 100f;
            }
            base.UpdateAccessory(player, hideVisual);
        }
    }
}