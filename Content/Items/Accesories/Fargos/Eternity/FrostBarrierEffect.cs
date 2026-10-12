using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.RemPlayer;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;

[ExtendsFromMod("FargowiltasSouls")]
public class FrostBarrierEffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<GuardiansShieldHeader>();
    public override int ToggleItemType => ModContent.ItemType<FrostBarrier>();

    public override void PostUpdateEquips(Player player)
    {
        ModContent.GetInstance<RemnantFargosSoulsPlayer>().FrostBarrier = true;
    }   
}

