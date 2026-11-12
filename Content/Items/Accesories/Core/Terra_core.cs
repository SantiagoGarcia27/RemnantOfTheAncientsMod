using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    public class Terra_core : CoreItem
    {
        private readonly int miningSpeedBonus = 15;
        private readonly int damageReduction = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(miningSpeedBonus, damageReduction);
       

        int timmer = 0;
        private readonly int timmerMax = Utils1.FormatTimeToTick(Second: 5);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pickSpeed -= miningSpeedBonus/100f;
            player.endurance += damageReduction/100f;

            float playerPos = player.Center.Y.ToTilePosition();
            if (playerPos > Main.rockLayer) 
            {
                if (!hideVisual) Lighting.AddLight(player.Center, TorchID.Jungle);
                if (timmer++ > timmerMax)
                {
                    timmer = 0;
                    player.AddBuff(BuffID.Dangersense, Utils1.FormatTimeToTick(Second: 1));
                    player.AddBuff(BuffID.Hunter, Utils1.FormatTimeToTick(Second: 1));

                }       
            }

            base.UpdateAccessory(player, hideVisual);
        }
    }
}