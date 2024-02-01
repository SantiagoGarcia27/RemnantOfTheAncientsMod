using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using RemnantOfTheAncientsMod.Content.Projectiles.Summon.Minioms;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;

[ExtendsFromMod("FargowiltasSouls")]
public class ReinforcedIronDREffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
    public override int ToggleItemType => ModContent.ItemType<ReinforcedIronEnchant>();
    float SpeedBonus = -50;
    float DamageReduction = 10;
    public override void PostUpdateEquips(Player player)
    {

        player.endurance += DamageReduction / 100;
        player.moveSpeed += SpeedBonus / 100;
    }
}
[ExtendsFromMod("FargowiltasSouls")]
public class ReinforcedIronBallistaEffect : AccessoryEffect
{
    public override Header ToggleHeader => Header.GetHeader<ForceOfRemantsHeader>();
    public override int ToggleItemType => ModContent.ItemType<ReinforcedIronEnchant>();
   
    Projectile IronBallistaProj = null;
    public override void PostUpdateEquips(Player player)
    {

        player.GetModPlayer<RemnantFargosSoulsPlayer>().IronBallistaEnchantment = true;
        int minionCount = (player.ownedProjectileCounts[ModContent.ProjectileType<IronBallistMinion>()]);
        if (minionCount < 1)
        {
            float damage = 30;

            damage = player.GetTotalDamage(DamageClass.Summon).ApplyTo(damage);
            IronBallistaProj = Projectile.NewProjectileDirect(Projectile.GetSource_None(), player.position, new Vector2(0, 2 * 16) * 10, ModContent.ProjectileType<IronBallistMinion>(), (int)damage, 1, Main.myPlayer);
            IronBallistaProj.minionSlots = 0;
            IronBallistaProj.timeLeft = 300;
            IronBallistaProj.originalDamage = (int)damage;

        }    
    }
}
