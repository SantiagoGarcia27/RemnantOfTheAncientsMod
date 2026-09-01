using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.BossProjectiles.Desert
{
    public class SandnadoMarkClone : ModProjectile
    {
        public float DurationToSandadoOnTicks
        {
            get =>Utils1.FormatTimeToTick(Second: Projectile.ai[0]);
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 900;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.hostile = true;
        }
        public override void AI()
        {
            Vector2 val2 = new();
            Color newColor4 = Color.White;
            
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = -1;
                SoundEngine.PlaySound(in SoundID.Item60, Projectile.Center);
            }
            if (Projectile.localAI[1] < 30f)
            {
                Vector2 vector169 = default;
                Vector2 vector170 = default;
                
                float value24 = -0.5f;
                float value25 = 0.9f;
                float amount = Main.rand.NextFloat();
                vector169 = new(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value24, value25, amount));
                vector169.X *= MathHelper.Lerp(2.2f, 0.6f, amount);
                vector169.X *= -1f;
                vector170 = new(2f, 10f);
                Vector2 vector171 = Projectile.Center + new Vector2(60f, 200f) * vector169 * 0.5f + vector170;

                Dust dustSand = Main.dust[Dust.NewDust(vector171, 0, 0, DustID.Sandnado)];
                dustSand.position = vector171;
                dustSand.customData = Projectile.Center + vector170;
                dustSand.fadeIn = 1f;
                dustSand.scale = 0.3f;
                if (vector169.X > -1.2f) dustSand.velocity.X = 1f + Main.rand.NextFloat();  
                dustSand.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
            }
            
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 2;//0.8f;
                Projectile.direction = 1;
                Point point8 = Projectile.Center.ToTileCoordinates();
                Projectile.Center = new Vector2(point8.X * 16 + 8, point8.Y * 16 + 8);
            }

           // Projectile.rotation = Projectile.localAI[1] / 40f * ((float)Math.PI * 2f) * (float)Projectile.direction;

            if (Projectile.localAI[1] < 33f)
            {
                if (Projectile.alpha > 0) Projectile.alpha -= 8;
                if (Projectile.alpha < 0) Projectile.alpha = 0;           
            }
            if (Projectile.localAI[1] > 103f)
            {
                if (Projectile.alpha < 255) Projectile.alpha += 16;
                if (Projectile.alpha > 255) Projectile.alpha = 255;    
            }
            if (Projectile.alpha == 0)
            {
                Lighting.AddLight(Projectile.Center,  newColor4.ToVector3() * 0.5f);
            }
            for (int i = 0; i < 2; i++)
            {
                if (Main.rand.NextBool(10))
                {
                    Vector2 unitY = Vector2.UnitY;
                    double radians = i * (float)Math.PI;
                    val2 = default;
                    Vector2 spinningpoint76 = unitY.RotatedBy(radians, val2);
                    double radians64 = Projectile.rotation;
                    val2 = default;
                    Vector2 vector172 = spinningpoint76.RotatedBy(radians64, val2);
                    Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.RainbowMk2, 0f, 0f, 225, newColor4, 1.5f);
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.scale = Projectile.Opacity * Projectile.localAI[0];
                    dust.position = Projectile.Center;
                    dust.velocity = vector172 * 2.5f;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                if (Main.rand.NextBool(10))
                {
                    Vector2 unitY = Vector2.UnitY;
                    double radians = i * (float)Math.PI;
                    val2 = default;
                    Vector2 vector173 = unitY.RotatedBy(radians, val2);
                    Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.RainbowMk2, 0f, 0f, 225, newColor4, 1.5f);
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.scale = Projectile.Opacity * Projectile.localAI[0];
                    dust.position = Projectile.Center;
                    dust.velocity = vector173 * 2.5f;
                }
            }
            if (Projectile.localAI[1] < 33f || Projectile.localAI[1] > 87f)
            {
                float scale = Projectile.Opacity / 2f * Projectile.localAI[0];
                Projectile.scale = scale;
            }

            Projectile.velocity = Vector2.Zero;
            Projectile.localAI[1]++;

            if (Projectile.localAI[1] == DurationToSandadoOnTicks && Projectile.owner == Main.myPlayer) //60f
            {
                int num1057 = 30;
                if (Main.expertMode)
                {
                    num1057 = 22;
                }
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.SandnadoHostile, num1057, 3f, Projectile.owner);
            }
            if (Projectile.localAI[1] >= DurationToSandadoOnTicks + 60f)//120f
            {
                Projectile.Kill();
            }
            
  
            base.AI();
        }
        public override void OnKill(int timeLeft)
        {
            for (int num184 = 0; num184 < 10; num184++)
            {
                int num185 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sandnado, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, default, 0.5f);
                Dust dust;
                Dust dust3;
                if (Main.rand.NextBool(3))
                {
                    Main.dust[num185].fadeIn = 0.75f + Main.rand.Next(-10, 11) * 0.01f;
                    Main.dust[num185].scale = 0.25f + Main.rand.Next(-10, 11) * 0.005f;
                    dust = Main.dust[num185];
                    dust3 = dust;
                    dust3.type++;
                }
                else
                {
                    Main.dust[num185].scale = 1f + Main.rand.Next(-10, 11) * 0.01f;
                }
                Main.dust[num185].noGravity = true;
                dust = Main.dust[num185];
                dust3 = dust;
                dust3.velocity *= 1.25f;
                dust = Main.dust[num185];
                dust3 = dust;
                dust3.velocity -= Projectile.oldVelocity / 10f;
            }
            base.OnKill(timeLeft);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - lightColor.A, 255 - lightColor.A, 255 - lightColor.A, 0);

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 origin = texture.Size() / 2f;

            Vector2 scale = new Vector2(Projectile.scale,Projectile.scale * 4f);

            Color color = Color.Lerp(Color.LightGoldenrodYellow,Color.Yellow,0.1f) * Projectile.Opacity;
            Main.EntitySpriteDraw(
                texture,
                Projectile.Center - Main.screenPosition,
                null,
                color,
                Projectile.rotation,
                origin,
                scale,
                SpriteEffects.None
            );

            return false;
        }

    }
}