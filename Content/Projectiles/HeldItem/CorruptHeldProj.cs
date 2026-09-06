using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class CorruptRayHeldProj : HeldCyrcleModel
    {
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;

        MagicCircle circle1 = null;
        MagicCircle circle2 = null;
        public override void DrawCirclee(ref List<MagicCircle> internalCircle, ref List<MagicCircle> externalCircle)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_4");
            Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_2");

            int maxCharge = (int)(GetMaxCharge() / 4);
            circle1 ??= new(texture, Color.Purple, 1.2f,TimeDuration: maxCharge);
            circle2 ??= new(texture2, Color.Purple, 1.5f,TimeDuration: maxCharge);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(ref internalCircle, ref externalCircle);
        }
        public override float GetMaxCharge(float MaxCharge = 180)
        {
            MaxCharge = 180 / Projectile.ai[2];
            return base.GetMaxCharge(MaxCharge);
        }

        public override void ShootEffect(ref bool killAfterEnd)
        {
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(5);
            Vector2 SubVelocity = new Vector2(Projectile.ai[0], Projectile.ai[1]);
            Player player = Main.player[Projectile.owner];

            Projectile.position += Vector2.Normalize(Projectile.velocity) * 1f;
         
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = SubVelocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.position, perturbedSpeed * 10, ModContent.ProjectileType<CorruptBolt>(), (int)(player.HeldItem.damage * 1.2f), player.HeldItem.knockBack, player.whoAmI);
                Main.projectile[p].stepSpeed = 10f;
                Main.projectile[p].GetAlpha(Color.Purple);
            }
            base.ShootEffect(ref killAfterEnd);
        }
    }
}
