using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{

    public class SuperFlare : ModProjectile
    {
       //public override void SetStaticDefaults() =>// //DisplayName.SetDefault("Super Flare");
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 400;
            Projectile.light = 0.15f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 50;
            AIType =  ProjectileID.WoodenArrowFriendly;
        }
        double wave = 0.0;
        int dir = -1;
        int Max;
        int Particles = 3;
        public override void AI()
        {
            Max = 200;
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (RemnantOfTheAncientsMod.ParticleMeterChoice())
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 0, default, 1.2f);
                Particles -= 1;
            }
            if (Particles == 0) Particles = 3;



            if (Projectile.ai[2] != 0)
            {
                if (wave >= Max || wave <= -Max) dir *= -1;
                wave += 1f * dir;

                if (Projectile.velocity.X < 2 && Projectile.velocity.X > -2) Projectile.velocity.X = (float)(Projectile.ai[2] * Math.Cos(wave / 10)) * 2;
                else Projectile.velocity.Y += (float)(Projectile.ai[2] * Math.Cos(wave / 5));
            }
        }

        //public override void Kill(int timeLeft)
        //{
        //    base.Kill(timeLeft);
        //}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, (int)Utils1.FormatTimeToTick(0, 0, 0, 10));
        }
    }
}