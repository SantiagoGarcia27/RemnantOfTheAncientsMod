using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public override void DrawCirclee(ref List<MagicCircle> internalCircle, ref List<MagicCircle> externalCircle)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleCenter_3");
            Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/Effects/MagicCircle/MagicCircleExterior_1");

            MagicCircle circle1 = new(texture, GetColor(), 1.2f);
            MagicCircle circle2 = new(texture2, GetColor(), 1.2f);

            internalCircle.Add(circle1);
            externalCircle.Add(circle2);
            base.DrawCirclee(ref internalCircle, ref externalCircle);
        }
        public override void ShootEffect(ref bool killAfterEnd)
        {
			killAfterEnd = false;
            base.ShootEffect(ref killAfterEnd);
        }
    }
}
