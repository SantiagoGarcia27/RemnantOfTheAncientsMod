using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Projectiles.Melee
{

    public class UltraBladeS : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("UltraBladeS"); //projectile name
        }
        public override void SetDefaults()
        {
            Projectile.width = 100;       //projectile width 36
            Projectile.height = 30;  //projectile height 36
            Projectile.friendly = true;      //make that the projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;          // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 200;      //how many NPC will penetrate
            Projectile.timeLeft = 20000;   //how many time this projectile has before disepire
            Projectile.light = 1.75f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.scale = 1.5f;
            
            AIType = ProjectileID.InfluxWaver;

        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            //  Lighting.AddLight(Projectile.position, 230, 230,0);
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    //If the NPC is hostile
                    if (!target.friendly)
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
                        }
                    }
                }
            }
            Lighting.AddLight(Projectile.position, 1, 0, 0);
            if (Projectile.scale < 1)
            {
                Projectile.scale += 0.05f;
            }
            if (Projectile.timeLeft < 20000)
            {
                GenerateParticle();
            }
        }
        public void GenerateParticle()
        {
            if (ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(4) != 0)
            {
                int dust5 = Dust.NewDust(Projectile.position, Projectile.width + 20, Projectile.height, DustID.IchorTorch);
                Main.dust[dust5].velocity = Projectile.velocity;
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 20;
            for (int i = 0; i < ModContent.GetInstance<RemnantOfTheAncientsMod>().ParticleMeter(NUM_DUSTS); i++)
            {
                int p1 = Dust.NewDust(Projectile.position,Projectile.width, Projectile.height,DustID.GemTopaz,0f,0f,100,default(Color),3f);
                Main.dust[p1].velocity = Projectile.velocity;
                Main.dust[p1].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod) && CalamityMod.TryFind("HolyFlames", out ModProjectile HolyFlames))
            {
                target.AddBuff(HolyFlames.Type, 400);
            }
            Projectile.Kill();
        }
    }
}
