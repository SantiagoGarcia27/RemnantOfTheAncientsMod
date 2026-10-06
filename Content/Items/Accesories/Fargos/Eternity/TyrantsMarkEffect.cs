using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;

[ExtendsFromMod("FargowiltasSouls")]
public class TyrantMarkEnemyProjectileSizeEffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<GuardiansShieldHeader>();
    public override int ToggleItemType => ModContent.ItemType<TyrantsMark>();
    //int ProjectileSizeBonus = 50;
    public override void PostUpdateEquips(Player player)
    {
        RemnantFargosSoulsPlayer remnantFargosSoulsPlayer = player.GetModPlayer<RemnantFargosSoulsPlayer>();
        player.GetModPlayer<RemnantPlayer>().EnemyProjectilesScaleBouns -= remnantFargosSoulsPlayer.ProjectileSizeBonus / 100;
    }
}

[ExtendsFromMod("FargowiltasSouls")]
public class TyrantMarkEnemyProjectileSpeedEffect : AccessoryEffect
{
    //int ProjectileSpeedBonus = 10;
    public override Header ToggleHeader => Header.GetHeader<GuardiansShieldHeader>();
    public override int ToggleItemType => ModContent.ItemType<TyrantsMark>();

    public override void PostUpdateEquips(Player player)
    {
        RemnantFargosSoulsPlayer remnantFargosSoulsPlayer = player.GetModPlayer<RemnantFargosSoulsPlayer>();
        player.GetModPlayer<RemnantPlayer>().EnemyProjectilesSpeedScaleBouns -= remnantFargosSoulsPlayer.ProjectileSpeedBonus / 100f;
    }
}

