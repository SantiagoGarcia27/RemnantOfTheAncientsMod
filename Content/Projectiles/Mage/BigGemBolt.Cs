using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Mage
{
    public class BigGemBolt : ModProjectile
    {
        public override string Texture => GetTexture();
        public int ProjectileTexture = 0;
        public int CurrentDust = 0;


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
            AIType = -1;
        }
        public string GetTexture()
        {
            int ProjectileTexture = 0;
            switch (Projectile.localAI[0])
            {
                case ProjectileID.AmethystBolt:
                    ProjectileTexture = ItemID.LargeAmethyst;
                    CurrentDust = DustID.GemAmethyst;
                    break;
                case ProjectileID.TopazBolt:
                    ProjectileTexture = ItemID.LargeTopaz;
                    CurrentDust = DustID.GemTopaz;
                    break;
                case ProjectileID.EmeraldBolt:
                    ProjectileTexture = ItemID.LargeEmerald;
                    CurrentDust = DustID.GemEmerald;
                    break;
                case ProjectileID.SapphireBolt:
                    ProjectileTexture = ItemID.LargeSapphire;
                    CurrentDust = DustID.GemSapphire;
                    break;
                case ProjectileID.RubyBolt:
                    ProjectileTexture = ItemID.LargeRuby;
                    CurrentDust = DustID.GemRuby;
                    break;
                case ProjectileID.DiamondBolt:
                    ProjectileTexture = ItemID.LargeDiamond;
                    CurrentDust = DustID.GemDiamond;
                    break;
                case ProjectileID.AmberBolt:
                    ProjectileTexture = ItemID.LargeAmber;
                    CurrentDust = DustID.GemAmber;
                    break;
            }



            string a = "Terraria/Images/Item_" + ProjectileTexture;
            return a;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.00f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

      
            if (Projectile.timeLeft < 20000)
            {
                if (RemnantOfTheAncientsMod.ParticleMeter(4) != 0)
                {
                    int dust5 = Dust.NewDust(Projectile.position, 1, Projectile.height, CurrentDust);
                    Main.dust[dust5].velocity = Projectile.velocity;
                    Main.dust[dust5].noGravity = true;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            const int NUM_DUSTS = 20;
            for (int i = 0; i < RemnantOfTheAncientsMod.ParticleMeter(NUM_DUSTS); i++)
            {
                int p1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (int)Projectile.localAI[1], 0f, 0f, 100, default, 1f);
                Main.dust[p1].velocity = Projectile.velocity;
                Main.dust[p1].noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(GetTexture());
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(sourceRectangle), Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            return base.PreDraw(ref lightColor);
        }
    }
}
