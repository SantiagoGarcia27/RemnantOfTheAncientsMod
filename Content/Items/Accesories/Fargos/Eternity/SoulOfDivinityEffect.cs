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
    int GodZoneproj = 0;
    public override void PostUpdateEquips(Player player)
    {

        if (player.ownedProjectileCounts[ModContent.ProjectileType<GodZone>()] <= 0)
        {
            GodZoneproj = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.Zero, ModContent.ProjectileType<GodZone>(), 0, 0, player.whoAmI);
            Main.projectile[GodZoneproj].timeLeft = 1000;
        }
       
    }
}

[ExtendsFromMod("FargowiltasSouls")]
public class GodModeEffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<SoulOfDivinityHeader>();
    public override int ToggleItemType => ModContent.ItemType<SoulOfDivinity>();

    public override void PostUpdateEquips(Player player)
    {
        player.GetModPlayer<RemnantPlayer>().Inmortal = true;
    }
}

