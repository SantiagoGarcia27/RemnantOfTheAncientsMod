using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{
    public class SkyCutterS : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SkyCutterS"); 
        }
        public override void SetDefaults()
        {
            Projectile.width = 46;     
            Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 20000;
            Projectile.light = 1.75f;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.scale = 0.7f;
            AIType = ProjectileID.InfluxWaver;
        }
        public override void AI()          
        {                                                       
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                
                if (!target.friendly && !target.dontTakeDamage && target.defense <= 998 && !target.immortal)
                {
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
                    }
                }
            }

            if (Projectile.scale < 1)
            {
                Projectile.scale += 0.05f;
            }
            if (Projectile.timeLeft < 20000)
            {
                if (ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(4) != 0)
                {
                    int dust5 = Dust.NewDust(Projectile.position, 1 , Projectile.height, DustID.IceRod);
                    Main.dust[dust5].velocity = Projectile.velocity;
                    Main.dust[dust5].noGravity = true;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 20;
            for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(NUM_DUSTS); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Ice, 0f, 0f, 100, default(Color), 1f);
                Main.dust[p1].velocity = Projectile.velocity;
                Main.dust[p1].noGravity = true;
            }
        } 
    }
}
