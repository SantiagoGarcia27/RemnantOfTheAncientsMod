using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;

[ExtendsFromMod("FargowiltasSouls")]
public class TuxoniteEffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
    public override int ToggleItemType => ModContent.ItemType<TuxoniteEnchant>();

    int CritBonus = 30;
    public override void PostUpdateEquips(Player player)
    {
        if (RemnantFargosSoulsPlayer.tuxoniteEnchantCritCounter == 0)
        {
            Dust.NewDustDirect(player.Center, 3, 10, DustID.MushroomSpray);
            player.GetCritChance(DamageClass.Generic) += CritBonus;
            if (RemnantFargosSoulsPlayer.tuxoniteEnchantCritDuration < Utils1.FormatTimeToTick(0, 0, 0, 10))
            {
                RemnantFargosSoulsPlayer.tuxoniteEnchantCritDuration++;
            }
            else
            {
                RemnantFargosSoulsPlayer.tuxoniteEnchantCritCounter = (int)Main.rand.NextFloat(Utils1.FormatTimeToTick(0, 0, 0, 30));
                RemnantFargosSoulsPlayer.tuxoniteEnchantCritDuration = 0;
            }
        }
        else
        {
            RemnantFargosSoulsPlayer.tuxoniteEnchantCritCounter--;
        }
    }
}

