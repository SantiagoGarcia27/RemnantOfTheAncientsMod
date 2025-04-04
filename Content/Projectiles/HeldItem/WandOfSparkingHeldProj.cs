using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class WandOfSparkingHeldProj : HeldCyrcleModel
	{
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
      
        public override void DrawCirclee(ref List<MagicCircle> internalCircle, ref List<MagicCircle> externalCircle)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_2");
            Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_2");

            MagicCircle circle1 = new(texture, Color.DarkRed, 1.2f);
            MagicCircle circle2 = new(texture2, Color.DarkRed, 1.2f);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(ref internalCircle, ref externalCircle);
        }
        public override void ShootEffect(ref bool killAfterEnd)
        {
            Player player = Main.player[Projectile.owner];

            int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.position - (new Vector2(10 * -player.direction, 30) * 16), new Vector2(0, 8), ModContent.ProjectileType<SparkBomb>(), (int)(player.HeldItem.damage * 3), player.HeldItem.knockBack, player.whoAmI);
            Main.projectile[p].stepSpeed = 5f;
            Main.projectile[p].Size *= 2f;
            Main.projectile[p].scale = 5f;
            Main.projectile[p].localAI[0] = (player.HeldItem.type == ItemID.WandofSparking) ? ProjectileID.WandOfSparkingSpark : ProjectileID.WandOfFrostingFrost;
            Main.projectile[p].GetAlpha(Color.Red);

            base.ShootEffect(ref killAfterEnd);
        }

    }
}
