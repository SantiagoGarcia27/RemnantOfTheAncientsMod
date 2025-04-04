using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class AquaScepterHeldProj : HeldCyrcleModel
    {
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
       
        public override void ShootEffect(ref bool killAfterEnd)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 SubVelocity = new(Projectile.ai[0], Projectile.ai[1]);
            Projectile.position += Vector2.Normalize(Projectile.velocity) * 1f;

            int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.position, SubVelocity * 3, ProjectileID.WaterStream, (int)(player.HeldItem.damage * 3), player.HeldItem.knockBack, player.whoAmI);
            Main.projectile[p].stepSpeed = 5f;
            Main.projectile[p].Size *= 2f;
            Main.projectile[p].scale = 5f;
            Main.projectile[p].tileCollide = false;
            Main.projectile[p].GetAlpha(Color.Red);

            base.ShootEffect(ref killAfterEnd);
        }
        public override void DrawCirclee(ref List<MagicCircle> internalCircle, ref List<MagicCircle> externalCircle)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_2");
            Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_2");

            MagicCircle circle1 = new(texture, Color.Blue, 1.2f);
            MagicCircle circle2 = new(texture2, Color.Blue, 1.2f);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(ref internalCircle,ref externalCircle);
        }
	}
}
