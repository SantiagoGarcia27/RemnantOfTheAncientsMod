using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Dusts;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.BossProjectile.Desert
{
    public class CactusBoulderClone : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.RollingCactus;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Boulder);
            AIType = ProjectileID.Boulder;
        }


    }
}