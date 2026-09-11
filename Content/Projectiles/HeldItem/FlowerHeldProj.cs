using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.HeldItem
{

    public class FlowerHeldProj : HeldCyrcleModel
    {
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;

        public override void SetDefaults()
        {
            Projectile.timeLeft = 9999;
            base.SetDefaults();
        }
        public override void AI()
        {
            base.AI();
        }
		public Color GetColor()
		{
			switch (Projectile.ai[0])
			{
				case 1: return Color.Orange;
				case 2: return Color.Cyan;
				case 3: return Color.Green;
				default: return Color.White;
			}		
		}
        MagicCircle circle1 = null;
        MagicCircle circle2 = null;
        public override void DrawCirclee(List<MagicCircle> internalCircle, List<MagicCircle> externalCircle)
        {
            Texture2D texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_3", AssetRequestMode.ImmediateLoad).Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_1", AssetRequestMode.ImmediateLoad).Value;

            int maxCharge = (int)(GetMaxCharge() / 4);
            circle1 ??= new(texture, GetColor(), 1.2f, TimeDuration: maxCharge);
            circle2 ??= new(texture2, GetColor(), 1.2f, TimeDuration: maxCharge);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(internalCircle, externalCircle);
        }
        public override void ShootEffect(ref bool killAfterEnd)
        {
			killAfterEnd = false;
            base.ShootEffect(ref killAfterEnd);
        }
    }
}
