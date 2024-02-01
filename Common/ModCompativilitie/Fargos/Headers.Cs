using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
[ExtendsFromMod("FargowiltasSouls")]
public class ForceOfRemantsHeader : EnchantHeader
{
    public override int Item => ModContent.ItemType<ForceOfRemants>();

    public override float Priority => 0.3f;
}

[ExtendsFromMod("FargowiltasSouls")]
public class GuardiansShieldHeader : MasoHeader
{
    public override int Item => ModContent.ItemType<GuardiansShield>();

    public override float Priority => 1f;
}


[ExtendsFromMod("FargowiltasSouls")]
public class SoulOfDivinityHeader : SoulHeader
{
    public override int Item => ModContent.ItemType<SoulOfDivinity>();

    public override float Priority => 2.7f;
}
