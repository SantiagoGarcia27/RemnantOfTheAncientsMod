/*using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace opswordsII
{
	public static class Utils1
	{
        public static bool DrawFishingLine(this Projectile projectile, int fishingRodType, Color poleColor, int xPositionAdditive = 45, float yPositionAdditive = 35f)
		{
			Player val = Main.player[projectile.owner];
			Item heldItem = val.HeldItem;
			if (!projectile.bobber || heldItem.holdStyle <= 0)
			{
				return false;
			}
			float num = val.MountedCenter.X;
			float y = val.MountedCenter.Y;
			y += val.gfxOffY;
			float gravDir = val.gravDir;
			if (heldItem.type == fishingRodType)
			{
				num += (float)(xPositionAdditive * ((Entity)val).direction);
				if (((Entity)val).direction < 0)
				{
					num -= 13f;
				}
				y -= yPositionAdditive * gravDir;
			}
			if (gravDir == -1f)
			{
				y -= 12f;
			}
			Vector2 vector = new Vector2(num, y);
			vector = val.RotatedRelativePoint(vector + new Vector2(8f), true) - new Vector2(8f);
			Vector2 vector2 = ((Entity)projectile).Center - vector;
			bool flag = true;
			if (vector2.X == 0f && vector2.Y == 0f)
			{
				return false;
			}
			float num2 = vector2.Length();
			num2 = 12f / num2;
			vector2.X *= num2;
			vector2.Y *= num2;
			vector -= vector2;
			vector2 = ((Entity)projectile).Center - vector;
			while (flag)
			{
				float num3 = 12f;
				float num4 = vector2.Length();
				if (float.IsNaN(num4) || float.IsNaN(num4))
				{
					break;
				}
				if (num4 < 20f)
				{
					num3 = num4 - 8f;
					flag = false;
				}
				num4 = 12f / num4;
				vector2.X *= num4;
				vector2.Y *= num4;
				vector += vector2;
				vector2 = ((Entity)projectile).Center - vector;
				if (num4 > 12f)
				{
					float num5 = 0.3f;
					float num6 = Math.Abs(((Entity)projectile).velocity.X) + Math.Abs(((Entity)projectile).velocity.Y);
					if (num6 > 16f)
					{
						num6 = 16f;
					}
					num6 = 1f - num6 / 16f;
					num5 *= num6;
					num6 = num4 / 80f;
					if (num6 > 1f)
					{
						num6 = 1f;
					}
					num5 *= num6;
					if (num5 < 0f)
					{
						num5 = 0f;
					}
					num6 = 1f - projectile.localAI[0] / 100f;
					num5 *= num6;
					if (vector2.Y > 0f)
					{
						vector2.Y *= 1f + num5;
						vector2.X *= 1f - num5;
					}
					else
					{
						num6 = Math.Abs(((Entity)projectile).velocity.X) / 3f;
						if (num6 > 1f)
						{
							num6 = 1f;
						}
						num6 -= 0.5f;
						num5 *= num6;
						if (num5 > 0f)
						{
							num5 *= 2f;
						}
						vector2.Y *= 1f + num5;
						vector2.X *= 1f - num5;
					}
				}
				Color color = Lighting.GetColor((int)vector.X / 16, (int)vector.Y / 16, poleColor);
				float rotation = Utils.ToRotation(vector2) - (float)Math.PI / 2f;
				Main.spriteBatch.Draw(Main.FishingLineTexture, new Vector2(vector.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, vector.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), new Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num3), color, rotation, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
			}
			return false;
		}
    }
}*/