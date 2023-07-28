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
            Projectile.CloneDefaults(ProjectileID.CrystalLeafShot);
            //Projectile.width = 16;     
            //Projectile.height = 16; 
            //Projectile.friendly = true;     
            //Projectile.tileCollide = true;  
            Projectile.penetrate = 2;
            Projectile.timeLeft = 20000;
            Projectile.light = 0.15f;
            //Projectile.extraUpdates = 1;
            //Projectile.ignoreWater = true;
            //AIType = ProjectileID.FrostBeam;
        }
        public override void AI()
        {
        }

        //public override void Kill(int timeLeft)
        //{
        //    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        //    Vector2 usePos = Projectile.position;
        //    Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
        //    usePos += rotVector * 16f;

        //    RemnantOfTheAncientsMod r = ModContent.GetInstance<RemnantOfTheAncientsMod>();
        //    int NUM_DUSTS = r.ParticleMeter(20);

        //    for (int i = 0; i < NUM_DUSTS; i++)
        //    {
        //        Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Tin);
        //        dust.position = (dust.position + Projectile.Center) / 2f;
        //        dust.velocity += rotVector * 2f;
        //        dust.velocity *= 0.5f;
        //        dust.noGravity = true;
        //        usePos -= rotVector * 8f;
        //    }
        //}

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
           // //DisplayName.SetDefault("LeafFriendlyClone");
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

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Vector2 usePos = Projectile.position;
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
            usePos += rotVector * 16f;

            RemnantOfTheAncientsMod r = ModContent.GetInstance<RemnantOfTheAncientsMod>();
            for (int i = 0; i < r.ParticleMeter(20); i++)
            {
                Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Tin);
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.velocity += rotVector * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= rotVector * 8f;
            }
        }
         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
        }
    }
}