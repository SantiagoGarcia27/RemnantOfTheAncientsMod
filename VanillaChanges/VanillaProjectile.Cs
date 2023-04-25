using RemnantOfTheAncientsMod.World;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace RemnantOfTheAncientsMod.VanillaChanges
{
    public class VanillaProjectile : GlobalProjectile
    {
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.BabySlime)
            {
                projectile.minionSlots = 0.5f;
            }
        }
    }
}