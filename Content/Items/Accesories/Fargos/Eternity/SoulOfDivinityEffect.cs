using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using RemnantOfTheAncientsMod.Content.Projectiles.Fargos;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;

[ExtendsFromMod("FargowiltasSouls")]
public class DivineAuraEffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<SoulOfDivinityHeader>();
    public override int ToggleItemType => ModContent.ItemType<SoulOfDivinity>();
    Projectile GodZoneproj = null;
    public override void PostUpdateEquips(Player player)
    {
       
            if (player.ownedProjectileCounts[ModContent.ProjectileType<GodZone>()] <= 0)
                GodZoneproj = Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.position, Vector2.Zero, ModContent.ProjectileType<GodZone>(), 0, 0, player.whoAmI);
       
        else
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<GodZone>()] > 0)
            {
                foreach (var p in Main.projectile)
                {
                    if (p != null && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<GodZone>())
                    {
                        p.Kill();
                    }
                }
            }
            if (GodZoneproj != null)
            {
                GodZoneproj.Kill();
                GodZoneproj = null;
            }
        }
    }
}

[ExtendsFromMod("FargowiltasSouls")]
public class GodModeEffect : AccessoryEffect
{
    int ProjectileSpeedBonus = 10;
    public override Header ToggleHeader => Header.GetHeader<SoulOfDivinityHeader>();
    public override int ToggleItemType => ModContent.ItemType<SoulOfDivinity>();

    public override void PostUpdateEquips(Player player)
    {
        player.GetModPlayer<RemnantPlayer>().Inmortal = true;
    }
}

