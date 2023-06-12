using System;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
    [JITWhenModsEnabled("CalamityMod")]
    public class LittleDevourer : ModProjectile
    {
        //public override string Texture => "Terraria/Images/NPC_" + NPCID.Shark;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Little Devourer"); //projectile name
            Main.projFrames[Projectile.type] = 4;
        }      
        public override void SetDefaults()
        {
            Projectile.width = 40;       //projectile width
            Projectile.height = 40;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 400;   //how many time this projectile has before disepire
            Projectile.light = 1.75f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.scale = 0.3f;
            //AIType = ProjectileID.InfluxWaver;

        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
           Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
           Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(190f);


            if (Projectile.ai[1] >= 80)
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
                        }
                    }
                }
            }
            else
            {
                Projectile.ai[1]++;
            }
            // generateDust();
            if (new RemnantOfTheAncientsMod().ParticleMeter(3) != 0)
            {
                AnimateTexture();
            }

        }
        
        private void generateDust()
        {
            Vector2 position = Projectile.position + new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));
            Dust dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Water);

            // Configura los valores de la partícula
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
            dust.scale = 1f;
        }
        public void AnimateTexture()
        {
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        //}

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Vector2 usePos = Projectile.position; // Position to use for dusts

            // Please note the usage of MathHelper, please use this!
            // We subtract 90 degrees as radians to the rotation vector to offset the sprite as its default rotation in the sprite isn't aligned properly.
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(45f)).ToRotationVector2(); // rotation vector to use for dust velocity
            usePos += rotVector * 16f;

            // Declaring a constant in-line is fine as it will be optimized by the compiler
            // It is however recommended to define it outside method scope if used elswhere as well
            // They are useful to make numbers that don't change more descriptive

            for (int i = 0; i < new RemnantOfTheAncientsMod().ParticleMeter(20); i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,ExternalModCallUtils.GetDustFromMod(RemnantOfTheAncientsMod.CalamityMod,"CosmiliteBarDust"), 0f, 0f, 100, default(Color), 1.5f);
            }
        }
        [JITWhenModsEnabled("CalamityMod")]
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ExternalModCallUtils.GetBuffFromMod(RemnantOfTheAncientsMod.CalamityMod,"GodSlayerInferno"),100);
            Projectile.Kill();
        }
    }
}
