using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class BloodThornHeldProj : HeldCyrcleModel
    {
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
   
        public override void DrawCirclee(ref List<MagicCircle> internalCircle, ref List<MagicCircle> externalCircle)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_7");
            Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_2");

            MagicCircle circle1 = new(texture, Color.DarkRed, 1.2f);
            MagicCircle circle2 = new(texture2, Color.DarkRed, 1.9f);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(ref internalCircle, ref externalCircle);
        }
        public override void ShootEffect(ref bool killAfterEnd)
        {
            Player player = Main.player[Projectile.owner];

            Projectile.position += Vector2.Normalize(Projectile.velocity) * 1f;

            for (int i = 0; i < 20; i++)
            {
                Vector2 pos1 = player.position + new Vector2((i + 2 * i) * 16, 3 * 16);
                Vector2 pos2 = player.position - new Vector2((i + 2 * i) * 16, -3 * 16);
                pos1 = DistanceUtils.SetPositionOnSolidFloor(pos1);
                pos2 = DistanceUtils.SetPositionOnSolidFloor(pos2);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos1, new Vector2(0, -10f), ProjectileID.SharpTears, player.HeldItem.damage, player.HeldItem.knockBack, player.whoAmI, 1, 1);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos2, new Vector2(0, -10f), ProjectileID.SharpTears, player.HeldItem.damage, player.HeldItem.knockBack, player.whoAmI, 1, 1);
            }
            base.ShootEffect(ref killAfterEnd);
        }
    }
}
