using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles
{

    public class AcornLeaftShoot : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.CrystalLeafShot;
       // public override void SetStaticDefaults() =>// //DisplayName.SetDefault("Acorn Leaft Shoot");
        public override void SetDefaults()
        {
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
            Projectile.CloneDefaults(ProjectileID.CrystalLeafShot);
            Projectile.penetrate = 2;
            Projectile.timeLeft = 20000;
            Projectile.light = 0.15f;
        }
        public override void AI()
        {
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
        }
    }


    public class LeafFriendlyClone : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Leaf;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 20000;
            Projectile.light = 0.15f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.FrostBeam;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                if (!target.friendly && !target.dontTakeDamage && target.defense <= 998 && !target.immortal)
                {
                    float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                    float shootToY = target.position.Y - Projectile.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                    if (distance < 480f && !target.friendly && target.active)
                    {
                        distance = 3f / distance;
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;
                        Projectile.velocity.X = shootToX;
                        Projectile.velocity.Y = shootToY;
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
  
            for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(20); i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, 1, 1, DustID.Grass);
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.noGravity = true;            
            }
        }
         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
        }
    }
}