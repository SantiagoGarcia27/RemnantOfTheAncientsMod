using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class BloodRayHeldProj : HeldCyrcleModel
	{
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;

        MagicCircle circle1 = null;
        MagicCircle circle2 = null;
        public override void DrawCirclee(List<MagicCircle> internalCircle, List<MagicCircle> externalCircle)
        {
            Texture2D texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_1", AssetRequestMode.ImmediateLoad).Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_3", AssetRequestMode.ImmediateLoad).Value;

            int timeDuration = (int)(180 / Projectile.ai[2]) / 4;
            circle1 ??= new(texture, Color.Red, 1.2f,TimeDuration: timeDuration);
            circle2 ??= new(texture2, Color.DarkRed, 1.5f, TimeDuration: timeDuration);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(internalCircle, externalCircle);
        }

        public override float GetMaxCharge(float MaxCharge = 180)
        {
            MaxCharge = 180 / Projectile.ai[2];
            return base.GetMaxCharge(MaxCharge);
        }

        public override void ShootEffect(ref bool killAfterEnd)
        {
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(30);
            Vector2 SubVelocity = new(Projectile.ai[0], Projectile.ai[1]);
            Player player = Main.player[Projectile.owner];

            Projectile.position += Vector2.Normalize(Projectile.velocity) * 1f;
   
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = SubVelocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.position, perturbedSpeed * 3, ModContent.ProjectileType<BloodDart>(), (int)(player.HeldItem.damage * 1.5f), player.HeldItem.knockBack, player.whoAmI);
                Main.projectile[p].stepSpeed = 10f;
                Main.projectile[p].GetAlpha(Color.Red);
            }
            base.ShootEffect(ref killAfterEnd);
        } 
	}
}
