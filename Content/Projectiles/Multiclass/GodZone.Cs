using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Fargos
{
    public class GodZone : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Assets/MagicPixel";
        public override void SetStaticDefaults()
        {
        }
        public sealed override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.alpha = 100;
            Projectile.manualDirectionChange = true;
            Projectile.netImportant = true;
            Projectile.timeLeft = 2000;

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public int RangeMax = 20 * 16;
        public int speed = 0;
        public float rotation = 0;
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
            }

            for (int p = 0; p < Main.npc.Length; p++)
            {
                NPC target = Main.npc[p];
                bool OnRange = Projectile.WithinRange(target.Center, RangeMax);

                if (target.active && OnRange && !target.friendly)
                {
                    target.life = 1;
                    target.SimpleStrikeNPC(1, 1);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                       // target.StrikeInstantKill();
                    }
                }
            }
        }
        public float fade = 2.6f;
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.whoAmI == Main.myPlayer)
            {
                var texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_5");
                var texture2 = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_3");
                Vector2 origin = new Vector2(texture.Width() * 0.5f, texture.Height() * 0.5f);//0.5
                Vector2 origin2 = new Vector2(texture2.Width() * 0.5f, texture2.Height() * 0.5f);//0.5

                Color colorBase = Color.Gold;
                if (Projectile.ai[0] <= 170)
                {
                    Color color = new Color(colorBase.R, colorBase.G, colorBase.B, 20);
                    Main.spriteBatch.Draw((Texture2D)texture, player.Center - Main.screenPosition, null, color, rotation, origin, 2, SpriteEffects.None, 0f);

                    Main.spriteBatch.Draw((Texture2D)texture2, player.Center - Main.screenPosition, null, color, -rotation, origin2, 3, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 2;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }
    }
}