using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Content.Projectiles.Fargos;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;

[ExtendsFromMod("FargowiltasSouls")]
public class DaylightEffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
    public override int ToggleItemType => ModContent.ItemType<DaylightEnchant>();

    Projectile Sunflowerproj = null;
    public override void PostUpdateEquips(Player player)
    {
        RemnantPlayer.DaylightArmorSetBonus = true;
        player.GetModPlayer<RemnantFargosSoulsPlayer>().DaylightEnchantment = true;
        if (Main.dayTime)
        {

            if (player.ownedProjectileCounts[ModContent.ProjectileType<FloatingSunFlowerMinion>()] <= 0)
                Sunflowerproj = Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.position, Vector2.Zero, ModContent.ProjectileType<FloatingSunFlowerMinion>(), 0, 0, player.whoAmI);

            //if (player.ownedProjectileCounts[ModContent.ProjectileType<FloatingSunFlowerMinion>()] > 0)
            //{
            //    foreach (var p in Main.projectile)
            //    {
            //        if (p != null && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<FloatingSunFlowerMinion>())
            //        {
            //            p.Kill();
            //        }
            //    }
            //}
            //if (Sunflowerproj != null)
            //{
            //    Sunflowerproj.Kill();
            //    Sunflowerproj = null;
            //}
        }
        else
        {
            if (Sunflowerproj != null)
            {
                Sunflowerproj.Kill();
                Sunflowerproj = null;
            }
            player.manaCost -= 0.05f;
        }
    }
}
