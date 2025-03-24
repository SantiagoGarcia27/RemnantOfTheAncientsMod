using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Ranger
{
    public class CustomFlameTrowerProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Flames);
            AIType = ProjectileID.Flames;
            Projectile.timeLeft = 3000;
        }    
        public override void AI()
        {
            AnimateProjectile();
            base.AI();
        }
        public static List<float> phase =
        [
            0,0,0,0,0,0,0
        ];
        public void AnimateProjectile()
        {
            Projectile.frame = PhaseSelector();
        }
        private int PhaseSelector()
        {

            if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[5], phase[6]))
            {
                return 0;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[4], phase[5]))
            {
                return 1;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[3], phase[4]))
            {
                return 2;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[2], phase[3]))
            {
                return 3;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[1], phase[2]))
            {
                return 4;
            }
            else if (Utils1.NumberBetweenExact(Projectile.timeLeft, phase[0], phase[1]))
            {
                return 5;
            }

            return Projectile.frame;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 1; i <= 7; i++)
            {
                phase[i-1] = (Projectile.ai[1] / 7) * i;
            }
            base.OnSpawn(source);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {   
           // SoundEngine.PlaySound(Main.rand.NextBool() ? SoundID.Item57 : SoundID.Item58, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Utils1.GetColorByID((int)Projectile.ai[2]);
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            Color color = Utils1.GetColorByID((int)Projectile.ai[2]);
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            int projectileFrames = Main.projFrames[Projectile.type];
            Vector2 origin = new(texture.Width * 0.5f, (texture.Height * 0.5f / projectileFrames) * Projectile.frame);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, color, 0f, origin, 1f, SpriteEffects.None, 0f);
            base.PostDraw(lightColor);
        }
    }
}
