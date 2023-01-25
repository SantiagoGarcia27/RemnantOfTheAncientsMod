using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{
    // to investigate: Projectile.Damage, (8843)
    internal class MisilUltra : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RocketIII);
            AIType = ProjectileID.RocketIII;
            Projectile.penetrate = 0;
        }
        public override void Kill(int timeLeft)
        {
            Vector2 launchVelocity = new Vector2(-4, 0); // Create a velocity moving the left.
            for (int i = 0; i < 4; i++)
            {
                launchVelocity = launchVelocity.RotatedBy(MathHelper.PiOver4);
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, launchVelocity, ModContent.ProjectileType<LittleShark>(), Projectile.damage / 4, Projectile.knockBack, Projectile.owner);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //base.OnHitNPC(target, damage, knockback, crit);
            Projectile.Kill();
        }
    }
}