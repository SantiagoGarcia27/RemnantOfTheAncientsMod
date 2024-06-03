using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using RemnantOfTheAncientsMod.Content.Dusts;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Melee
{
    public class UltraBladeProjectileSub : ModProjectile
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("DesertS");
        }
        public override void SetDefaults()   
        {    
            Projectile.width = 36;      
            Projectile.height = 10;
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
                    //If the NPC is hostile
                    if (!target.friendly && !target.dontTakeDamage && target.defense <= 998 && !target.immortal)
                    {
                        //Get the shoot trajectory from the projectile and target
                        float shootToX = target.position.X + target.width * 0.5f - Projectile.Center.X;
                        float shootToY = target.position.Y - Projectile.Center.Y;
                        float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                        //If the distance between the live targeted NPC and the projectile is less than 480 pixels
                        if (distance < 480f && !target.friendly && target.active)
                        {
                            //Divide the factor, 3f, which is the desired velocity
                            distance = 3f / distance;

                            //Multiply the distance by a multiplier if you wish the projectile to have go faster
                            shootToX *= distance * 5;
                            shootToY *= distance * 5;

                            //Set the velocities to the shoot values
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
            if (GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(4) != 0)
            {
                int dust5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.IchorTorch);
                Main.dust[dust5].velocity = Projectile.velocity;
            }
        }
        public override void OnKill(int timeLeft) 
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int i = 0; i < GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(20); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemTopaz, 0f, 0f, 100, default, 3f);
                Main.dust[p1].velocity = Projectile.velocity * 0f;
                Main.dust[p1].noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // if (Main.rand.NextBool()) target.AddBuff(BuffType<Burning_Sand>(), 300);
        }
    }
}