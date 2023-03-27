using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Projectiles
{

    public class SuperFlare : ModProjectile
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Super Flare");
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 100;
            Projectile.light = 0.15f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType =  ProjectileID.WoodenArrowFriendly;
        }
        public override void AI()
        {
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 0, default(Color), 1.2f);


        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Main.rand.NextFloat() <= new RemnantOfTheAncientsMod().ParticleMeter(4) / 4)
            {
                return false;
            }
            else
            {
                base.Kill(1);
                return true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        }
    }
}