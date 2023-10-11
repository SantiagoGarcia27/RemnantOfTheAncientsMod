using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
    public class CustomFlameTrowerProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Flames);
            AIType = ProjectileID.Flames;
            Projectile.timeLeft = 3000;
        }

        public override void Kill(int timeLeft)
        {
            //Vector2 launchVelocity = new Vector2(-4, 0); // Create a velocity moving the left.
            //for (int i = 0; i < 4; i++)
            //{
            //    // Every iteration, rotate the newly spawned projectile by the equivalent 1/4th of a circle (MathHelper.PiOver4)
            //    // (Remember that all rotation in Terraria is based on Radians, NOT Degrees!)
            //    launchVelocity = launchVelocity.RotatedBy(MathHelper.PiOver4);

            //    // Spawn a new projectile with the newly rotated velocity, belonging to the original projectile owner. The new projectile will inherit the spawning source of this projectile.
            //    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, launchVelocity, ProjectileID.Meowmere, Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
            //}
        }
        public override void AI()
        {
            AnimateProjectile();
            base.AI();
        }
        public static List<float> phase = new List<float>()
        {
            0,0,0,0,0,0,0
        };
        public void AnimateProjectile()
        {
            Projectile.frame = PhaseSelector();
        }
        private int PhaseSelector()
        {
            if(Utils1.NumberBetweenExact(Projectile.timeLeft, phase[5], phase[6]))
            {
                return 0;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[4], phase[5]))
            {
                return 1;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[3], phase[4]))
            {
                return 2;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[2], phase[3]))
            {
                return 3;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[1], phase[2]))
            {
                return 4;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[0], phase[1]))
            {
                return 5;
            }

            return Projectile.frame;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 1; i <= 7; i++)
            {
                phase[i-1] = (Projectile.ai[1] / 7) * i;
            }
            base.OnSpawn(source);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {   
           // SoundEngine.PlaySound(Main.rand.NextBool() ? SoundID.Item57 : SoundID.Item58, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }
    }
}
