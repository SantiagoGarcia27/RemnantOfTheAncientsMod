using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{
    public class MagicCircle
    {
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public float Scale = 1;

        public MagicCircle()
        {

        }

        public MagicCircle(Texture2D texture, Color color, float Scale = 1)
        {
            this.Texture = texture;
            this.Color = color;
            this.Scale = Scale;
        }
    }



    public abstract class HeldCyrcleModel : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Default;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 210;
            Projectile.light = 0f;
            Projectile.extraUpdates = 1;
            Main.projFrames[Projectile.type] = 3;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            base.SetDefaults();
        }

        public int speed = 0;
        public float rotation = 0;
        public int Charge = 0;
        public virtual float GetMaxCharge(float MaxCharge = 180) => MaxCharge;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            if (player.whoAmI == Main.myPlayer)
            {
                if (++speed >= 10)
                {
                    if (rotation++ >= 360)
                    {
                        rotation = 0;
                    }
                    speed = 0;
                }
                Charge++;
                float maxCharge = GetMaxCharge();
                if (Charge >= maxCharge)
                {
                    bool killAfterEnd = true;
                    ShootEffect(ref killAfterEnd);
                }
                base.AI();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            List<MagicCircle> internalCircle = [];
            List<MagicCircle> externalCircle = [];
            DrawCirclee(ref internalCircle, ref externalCircle);
            return base.PreDraw(ref lightColor);
        }
        public virtual void ShootEffect(ref bool killAfterEnd) 
        {
             if(killAfterEnd)
                Projectile.Kill();
        }
        public virtual void DrawCirclee(ref List<MagicCircle> internalCircle, ref List<MagicCircle> externalCircle)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.whoAmI == Main.myPlayer)
            {
                if (internalCircle.Count > 0)
                {
 
                    foreach (MagicCircle circle in internalCircle)
                    {
                        Color colorBase = circle.Color;
                        Vector2 origin = new(circle.Texture.Width * 0.5f, circle.Texture.Height * 0.5f);
                        if (Projectile.ai[0] <= 170)
                        {
                            Color color = new Color(colorBase.R, colorBase.G, colorBase.B, 20);
                            Main.spriteBatch.Draw(circle.Texture, Projectile.Center - Main.screenPosition, null, color, rotation, origin, circle.Scale, SpriteEffects.None, 0f);
                        }
                    }
                }
                if (externalCircle.Count > 0)
                {
                    foreach (MagicCircle circle in externalCircle)
                    {
                        Color colorBase = circle.Color;
                        Vector2 origin = new(circle.Texture.Width * 0.5f, circle.Texture.Height * 0.5f);
                        if (Projectile.ai[0] <= 170)
                        {
                            Color color = new Color(colorBase.R, colorBase.G, colorBase.B, 20);
                            Main.spriteBatch.Draw(circle.Texture, Projectile.Center - Main.screenPosition, null, color, rotation, origin, circle.Scale, SpriteEffects.None, 0f);
                        }
                    }
                }
            }
        }


    }
}
