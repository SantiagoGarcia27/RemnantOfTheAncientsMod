using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Content.Projectiles.Bobbers
{
    public class TrueNigthBobber : ModProjectile
	{
		public static readonly Color[] PossibleLineColors = [
		new Color(142, 201, 0, 100),
		new Color(238,134,222,100),
        new Color(100,90,176,100)
        ];

		private int fishingLineColorIndex;

        public Color FishingLineColor => PossibleLineColors[fishingLineColorIndex];
		public override void SetStaticDefaults()
		{
        }

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BobberGolden);
			DrawOriginOffsetY = -8; 
		}
        public override void OnSpawn(IEntitySource source)
        {
            fishingLineColorIndex = (byte)Main.rand.Next(PossibleLineColors.Length);
        }
        public override void AI()
		{
			if (!Main.dedServ)
			{
				Lighting.AddLight(Projectile.Center, FishingLineColor.ToVector3());
			}
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
