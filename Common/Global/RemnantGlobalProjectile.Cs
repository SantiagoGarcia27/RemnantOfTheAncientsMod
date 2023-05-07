using RemnantOfTheAncientsMod.World;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace RemnantOfTheAncientsMod.Common.Global
{
    public class RemnantGlobalProjectile : GlobalProjectile
    {
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.BabySlime)
            {
                projectile.minionSlots = 0.5f;
            }
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Reaper.ReaperMode && projectile.CountsAsClass(DamageClass.Melee) && projectile.friendly)
            {
                projectile.width *= (int)1.5f;
                projectile.height *= (int)1.5f;
                projectile.scale *= 2.5f;
            }
        }
    }
}