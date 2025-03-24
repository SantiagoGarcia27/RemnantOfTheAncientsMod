using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{

    public class EmeraldAndScarletHeldProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // //DisplayName.SetDefault("Tomb_BulletP");
        }
      
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Default;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.light = 0.0f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            AIType = -1;
        }

        public Vector2 armPosition;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.position = player.position;
            armPosition = player.RotatedRelativePoint(player.MountedCenter, true); 
        }
        public override string Texture => "RemnantOfTheAncientsMod/Content/Items/Weapons/Ranger/Bows/EmeraldAndScarlet";


        public override bool PreDraw(ref Color lightColor)
        {
            string path = "RemnantOfTheAncientsMod/Content/Items/Weapons/Ranger/Bows/";
            Player player = Main.player[Projectile.owner];
            string currentBow;

            if (!(Main.mouseLeft && Main.mouseRight))
            {
                currentBow = "Emerald";
                if (Main.LocalPlayer.altFunctionUse == 2)
                {
                    currentBow = "Scarlet";
                }
            }
            else
            {
                currentBow = "EmeraldAndScarlet";
            }


            if (Main.myPlayer == Projectile.owner)
            {
                float interpolant = Utils.GetLerpValue(5f, 25f, player.Distance(Main.MouseWorld), true);
                Vector2 oldVelocity = Projectile.velocity;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, player.DirectionTo(Main.MouseWorld), interpolant);
                if (Projectile.velocity != oldVelocity)
                {
                    Projectile.netSpam = 0;
                    Projectile.netUpdate = true;
                }
            }

            Projectile.position = armPosition - Projectile.Size * 0.5f + Projectile.velocity.SafeNormalize(Vector2.Zero) * 35f;
            Projectile.rotation = Projectile.velocity.ToRotation();
            int oldDirection = Projectile.spriteDirection;
            if (oldDirection == -1)
                Projectile.rotation += MathHelper.Pi;

            Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0).ToDirectionInt();
            if (Projectile.spriteDirection != oldDirection)
                Projectile.rotation -= MathHelper.Pi;





            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(path + currentBow);
          
            SpriteEffects effect = Projectile.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            int scale = 1;

            Vector2 position = Projectile.Center - Main.screenPosition /*(new Vector2(25,0) * player.direction)*/;
            Main.spriteBatch.Draw(texture, position/*.RotatedBy(Projectile.rotation, player.Center - Main.screenPosition)*/, null, Color.White, Projectile.rotation, Utils.Size(texture) * 0.5f, scale, effect, 1f);
            return false;
        }
    }
}