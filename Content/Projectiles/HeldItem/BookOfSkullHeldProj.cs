using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class BookOfSkullHeldProj : HeldCyrcleModel
    {
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
        MagicCircle circle1 = null;
        MagicCircle circle2 = null;
        public override void DrawCirclee(List<MagicCircle> internalCircle, List<MagicCircle> externalCircle)
        {
            Texture2D texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_6", AssetRequestMode.ImmediateLoad).Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_3", AssetRequestMode.ImmediateLoad).Value;

            int maxCharge = (int)(GetMaxCharge() / 4);
            circle1 ??= new(texture, Color.DarkSlateGray, 0.7f,TimeDuration: maxCharge);
            circle2 ??= new(texture2, Color.DarkSlateGray, 1.2f, TimeDuration: maxCharge);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(internalCircle, externalCircle);
        }

        bool shoot = false;
        public override void ShootEffect(ref bool killAfterEnd)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 SubVelocity = new(Projectile.ai[0], Projectile.ai[1]);
            if (!shoot)
            {
                shoot = true;
                Projectile.position += Vector2.Normalize(Projectile.velocity) * 1f;

                int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.position, SubVelocity * 5, ProjectileID.BookOfSkullsSkull, (int)(player.HeldItem.damage * (5f + Projectile.ai[2])), player.HeldItem.knockBack, player.whoAmI);
                Main.projectile[p].stepSpeed = 5f;
                Main.projectile[p].Size *= 2f;
                Main.projectile[p].scale = 5f;
                Main.projectile[p].GetAlpha(Color.Red);
            }
        }
    }
}
