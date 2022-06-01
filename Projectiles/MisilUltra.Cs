using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace opswordsII.Projectiles
{
    // to investigate: Projectile.Damage, (8843)
    internal class MisilUltra : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // while the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.friendly = true;
            Projectile.penetrate = -1;

            // 5 second fuse.
            Projectile.timeLeft = 300;

            // These 2 help the projectile hitbox be centered on the projectile sprite.
           /* drawOffsetX = 5;
            drawOriginOffsetY = 5;*/
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
            if (Main.expertMode)
            {
                if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
                {
                    damage /= 5;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Die immediately if ai[1] isn't 0 (We set this to 1 for the 5 extra explosives we spawn in Kill)
            if (Projectile.ai[1] != 0)
            {
                return true;
            }
            // OnTileCollide can trigger quite quickly, so using soundDelay helps prevent the sound from overlapping too much.
            if (Projectile.soundDelay == 0)
            {
                // We use WithVolume since the sound is a bit too loud, and WithPitchVariance to give the sound some random pitch variance.
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            Projectile.soundDelay = 10;

            // This code makes the projectile very bouncy.
            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
            {
                Projectile.velocity.X = oldVelocity.X * -0.9f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
            {
                Projectile.velocity.Y = oldVelocity.Y * -0.9f;
            }
            return false;
        }

        public override void AI()
        {
            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
            {
                Projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                Projectile.alpha = 255;
                // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                Projectile.position = Projectile.Center;
                //Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
                //Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
                Projectile.width = 250;
                Projectile.height = 250;
                Projectile.Center = Projectile.position;
                //Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
                //Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
                Projectile.damage = 250;
                Projectile.knockBack = 10f;
            }
            else
            {
                // Smoke and fuse dust spawn.
                if (Main.rand.NextBool())
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
                    Main.dust[dustIndex].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy((double)Projectile.rotation, default(Vector2)) * 1.1f;
                    dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[dustIndex].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2 - 6)).RotatedBy((double)Projectile.rotation, default(Vector2)) * 1.1f;
                }
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 5f)
            {
                Projectile.ai[0] = 10f;
                // Roll speed dampening.
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.97f;
                    //if (Projectile.type == 29 || Projectile.type == 470 || Projectile.type == 637)
                    {
                        Projectile.velocity.X = Projectile.velocity.X * 0.99f;
                    }
                    if ((double)Projectile.velocity.X > -0.01 && (double)Projectile.velocity.X < 0.01)
                    {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            }
            // Rotation increased by velocity.X 
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            return;
        }

        public override void Kill(int timeLeft)
        {
            // If we are the original projectile, spawn the 5 child projectiles
            /*if (Projectile.ai[1] == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    // Random upward vector.
                    Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-10, -8));
                    Projectile.NewProjectile(Projectile.Center, vel, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
                }
            }
            // Play explosion sound
            Main.PlaySound(SoundID.Item15, Projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
            // Large Smoke Gore spawn
            for (int g = 0; g < 2; g++)
            {
                int goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
            // reset size to normal width and height.
            Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);

            // TODO, tmodloader helper method
            {
                int explosionRadius = 5;
                //if (Projectile.type == 29 || Projectile.type == 470 || Projectile.type == 637)
                {
                    explosionRadius = 7;
                }
                int minTileX = (int)(Projectile.position.X / 16f - (float)explosionRadius);
                int maxTileX = (int)(Projectile.position.X / 16f + (float)explosionRadius);
                int minTileY = (int)(Projectile.position.Y / 16f - (float)explosionRadius);
                int maxTileY = (int)(Projectile.position.Y / 16f + (float)explosionRadius);
                if (minTileX < 0)
                {
                    minTileX = 0;
                }
                if (maxTileX > Main.maxTilesX)
                {
                    maxTileX = Main.maxTilesX;
                }
                if (minTileY < 0)
                {
                    minTileY = 0;
                }
                if (maxTileY > Main.maxTilesY)
                {
                    maxTileY = Main.maxTilesY;
                }*/
            }
        }
    }