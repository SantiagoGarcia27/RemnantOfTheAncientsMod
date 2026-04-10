using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
    public class UltraBladeProjectileSub : ModProjectile
    {
        public override void SetDefaults()   
        {    
            Projectile.width = 72;      
            Projectile.height = 72;
            Projectile.friendly = true;     
            Projectile.DamageType = DamageClass.Melee;        
            Projectile.tileCollide = false;  
            Projectile.penetrate = 5;   
            Projectile.timeLeft = 100; 
            Projectile.light = 1.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.TerraBeam;  
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            Projectile.localAI[1]++;


            if (Projectile.scale < 1)
            {
                Projectile.scale += 0.05f;
            }
            if (Projectile.localAI[1] < 20) 
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    if (!target.friendly && !target.dontTakeDamage && target.defense <= 998 && !target.immortal)
                    {
                        float shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                        float shootToY = target.position.Y - Projectile.Center.Y;
                        float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                        if (distance < 480f && !target.friendly && target.active)
                        {
                            distance = 3f / distance;

                            shootToX *= distance * 5;
                            shootToY *= distance * 5;

                            Projectile.velocity.X = shootToX;
                            Projectile.velocity.Y = shootToY;
                            Projectile.velocity.Normalize();
                            Projectile.velocity *= 2f;
                        }
                    }
                }
            }

            GenerateParticle();
        }
        public void GenerateParticle()
        {
            if (RemnantOfTheAncientsMod.ParticleMeter(4) != 0)
            {
                int dust5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IchorTorch);
                Main.dust[dust5].velocity = Projectile.velocity;
            }
        }
        public override void OnKill(int timeLeft) 
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(20); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz, 0f, 0f, 100, default, 3f);
                Main.dust[p1].velocity = Projectile.velocity * 0f;
                Main.dust[p1].noGravity = true;
            }
        }  
    }
}