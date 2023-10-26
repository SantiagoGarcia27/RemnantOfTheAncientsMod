using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Bobbers
{
    public class NigthBobber : ModProjectile
	{
		public static readonly Color[] PossibleLineColors = new Color[] {
		//	new Color(255, 215, 0), // A gold color
		new Color(139, 42, 156, 100) // A blue color
		};

		private bool initialized;
		private int fishingLineColorIndex;

		private Color FishingLineColor => PossibleLineColors[fishingLineColorIndex];
		public override void SetStaticDefaults()
		{
        }

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BobberGolden);
			DrawOriginOffsetY = -8; 
		}

		public override void AI()
		{
			if (!initialized)
			{
				initialized = true;
				fishingLineColorIndex = (byte)Main.rand.Next(PossibleLineColors.Length);
				Projectile.netUpdate = true;
			}
			if (!Main.dedServ)
			{
				Lighting.AddLight(Projectile.Center, FishingLineColor.ToVector3());
			}
		}

		public override void ModifyFishingLine(ref Vector2 lineOriginOffset, ref Color lineColor)
		{
			lineOriginOffset = new Vector2(47, -31);
			lineColor = FishingLineColor;
		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write((byte)fishingLineColorIndex);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			fishingLineColorIndex = reader.ReadByte();
		}
	}
}
