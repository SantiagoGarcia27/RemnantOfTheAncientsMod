using RemnantOfTheAncientsMod.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{
    internal class GlobalProjectile1 : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Reaper.ReaperMode && projectile.CountsAsClass(DamageClass.Melee) && projectile.friendly)
            {
                projectile.width *= 2;
                projectile.height *= 2;
                projectile.scale *= 2.5f;
            }

        }
    }
}
