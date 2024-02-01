using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;

[ExtendsFromMod("FargowiltasSouls")]
public class NightEffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
    public override int ToggleItemType => ModContent.ItemType<NightEnchant>();
    [JITWhenModsEnabled("FargowiltasSouls")]
    public override void PostUpdateEquips(Player player)
    {
        if (!Main.dayTime)
        {
            player.opacityForAnimation = RemnantFargosSoulsPlayer.NightTpCouldown == 0 ? 0.5f : 1;
            player.aggro -= 500;
            player.GetModPlayer<RemnantFargosSoulsPlayer>().NightTp = true;
        }
        else
        {
            player.opacityForAnimation = 1;
            player.GetModPlayer<RemnantFargosSoulsPlayer>().NightTp = false;
        }
    }
}
