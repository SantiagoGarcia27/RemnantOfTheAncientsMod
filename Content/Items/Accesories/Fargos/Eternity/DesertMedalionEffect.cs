using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;

[ExtendsFromMod("FargowiltasSouls")]
public class DesertMedalionEffect : AccessoryEffect
{                                                           
    public override Header ToggleHeader => Header.GetHeader<GuardiansShieldHeader>();
    public override int ToggleItemType => ModContent.ItemType<DesertMedalion>();

    public override void PostUpdateEquips(Player player)
    {
        player.GetModPlayer<RemnantPlayer>().MinionsBuffInflict.Add(ModContent.BuffType<Burning_Sand>());

        player.GetModPlayer<RemnantFargosSoulsPlayer>().DesertMedalion = true;

        player.buffImmune[ModContent.BuffType<Burning_Sand>()] = true;
    }
}

