using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;

[ExtendsFromMod("FargowiltasSouls")]
public class SandHeraldEffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
    public override int ToggleItemType => ModContent.ItemType<SandHeraldEnchant>();

    Projectile CactusBoulderproj = null;
    public override void PostUpdateEquips(Player player)
    {
        player.GetModPlayer<RemnantPlayer>().MinionsBuffInflict.Add(BuffID.Electrified);
        player.GetModPlayer<RemnantPlayer>().AllClassBuffInflict.Add(BuffID.Electrified);


        CactusBoulderproj = player.GetModPlayer<RemnantPlayer>().SpawnProjectileOnMouse(ProjectileID.RollingCactus, CactusBoulderproj);
        //else
        //{
        //    if (CactusBoulderproj != null)
        //    {
        //        CactusBoulderproj.Kill();
        //        CactusBoulderproj = null;
        //    }
        //}
    }
}
