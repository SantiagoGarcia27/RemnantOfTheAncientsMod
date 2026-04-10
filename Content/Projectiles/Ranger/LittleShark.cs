using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
    public class LittleShark : ModProjectile
    {
        public override void SetStaticDefaults()
        {  
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 400;
            Projectile.light = 1.75f;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.scale = 0.3f;
            Projectile.netImportant = false;
        }
        int targetindex = -1;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(190f);

            if (targetindex == -1)
            {
                if (Projectile.ai[0] >= 0)
                {
                    int targetIndex = (int)Projectile.ai[0];
                    if (targetIndex < 200 && targetIndex >= 0 && Main.npc[targetIndex].active && Main.npc[targetIndex].life > 0)
                    {
                        targetindex = targetIndex;
                    }
                }
                else
                {
                    for (int i = 0; i < 200; i++)
                    {
                        if (Main.npc[i].active && Main.npc[i].life > 0)
                        {
                            targetindex = i;
                            break;
                        }
                    }
                }

            }
            NPC target = null;
            if (targetindex >= 0 && targetindex < 202) target = Main.npc[targetindex];
            if (target != null) Homming(target);
            if (RemnantOfTheAncientsMod.ParticleMeter(3) != 0)
            {
                AnimateTexture();
                generateDust();
            }

        }

        private void Homming(NPC target)
        {
            //If the NPC is hostile
            if (target.active && !target.friendly && !target.dontTakeDamage && target.defense <= 998 && !target.immortal)
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

            for (int i = 0; i <RemnantOfTheAncientsMod.ParticleMeter(5); i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Water, 0f, 0f, 100, default(Color), 1.5f);
            }
        }
         public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(190f);
            base.OnSpawn(source);
        }

    }
}
