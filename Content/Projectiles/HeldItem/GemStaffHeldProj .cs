using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class GemStaffHeldProj : HeldCyrcleModel
    {
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;

        Vector2 SubVelocity = new(0, 0);

        public override void ShootEffect(ref bool killAfterEnd)
        {
            if (Projectile.velocity.X != 0 || Projectile.velocity.X != 0)
            {
                SubVelocity = Projectile.velocity;
            }

            Player player = Main.player[Projectile.owner];
            int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.position - new Vector2(0f, 1.5f) * 16, SubVelocity * 2, ModContent.ProjectileType<BigGemBolt>(), (int)(player.HeldItem.damage * 4f), player.HeldItem.knockBack, player.whoAmI);
            Main.projectile[p].Size *= 2f;
            Main.projectile[p].scale = 3f;
            Main.projectile[p].stepSpeed = 10f;
            Main.projectile[p].localAI[0] = Projectile.ai[0];

            base.ShootEffect(ref killAfterEnd);
        }

        MagicCircle circle1 = null;
        MagicCircle circle2 = null;
        public override void DrawCirclee(ref List<MagicCircle> internalCircle, ref List<MagicCircle> externalCircle)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_5");
            Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_1");

            int maxCharge = (int)(GetMaxCharge() / 4);
            circle1 ??= new(texture, GetColor(), 1.2f, TimeDuration: maxCharge);
            circle2 ??= new(texture2, GetColor(), 1.2f, TimeDuration: maxCharge);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(ref internalCircle, ref externalCircle);
        }
        public Color GetColor()
        {
            switch (Projectile.ai[1])
            {
                case 0: return Color.Magenta;
                case 1: return Color.Yellow;
                case 2: return Color.Green;
                case 3: return Color.Blue;
                case 4: return Color.Red;
                case 5: return Color.White;
                case 6: return Color.SandyBrown;
                default: return Color.Black;
            }
        }

	}
}
