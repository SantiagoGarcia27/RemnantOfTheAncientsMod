using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using opswordsII;
using System.Text;
using Terraria.ModLoader;
using Terraria.ID;

namespace opswordsII
{
	public static class Utils1
	{
		internal static readonly Color ReaperRarityColor = new Color(48, 48, 48); // 
		internal static readonly Color LegendaryRarityColor = new Color(0, 255, 200);

		/*public static bool DrawFishingLine(this Projectile projectile, int fishingRodType, Color poleColor, int xPositionAdditive = 45, float yPositionAdditive = 35f)
		{
			Player player = Main.player[projectile.owner];
			Item heldItem = player.HeldItem;
			if (!projectile.bobber || heldItem.holdStyle <= 0)
			{
				return false;
			}
			float x = player.MountedCenter.X;
			float y = player.MountedCenter.Y;
			y += player.gfxOffY;
			float gravDir = player.gravDir;
			if (heldItem.type == fishingRodType)
			{
				x += xPositionAdditive * player.direction;
				if (player.direction < 0)
				{
					x -= 13f;
				}
				y -= yPositionAdditive * gravDir;
			}
			if (gravDir == -1f)
			{
				y -= 12f;
			}
			Vector2 vector = new Vector2(x, y);
			vector = player.RotatedRelativePoint(vector + new Vector2(8f), true) - new Vector2(8f);
			Vector2 vector2 = projectile.Center - vector;
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
			vector2 = projectile.Center - vector;
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
				vector2 = projectile.Center - vector;
				if (num4 > 12f)
				{
					float num5 = 0.3f;
					float num6 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
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
						num6 = Math.Abs(projectile.velocity.X) / 3f;
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
				Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(vector.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, vector.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), new Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num3), color, rotation, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
			}
			return false;
		}*/

		internal static bool IsABoss()
		{
			throw new NotImplementedException();
		}

		public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
		{
			float amount = (float)((Math.Sin(Math.PI * 2f / seconds * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
			return Color.Lerp(firstColor, secondColor, amount);
		}
		public static Color? GetRarityColor(CustomRarity rare)
		{
			switch (rare)
			{
				default:
					return null;
				case CustomRarity.Reaper:
					return ReaperRarityColor;
				case CustomRarity.Legendary:
					return LegendaryRarityColor;
			}
		}

		public static bool anyBossIsAlive;

		public static bool IsAnyBossAlive()
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && Main.npc[i].boss)
					return true;
			}
			return false;
		}

		public static string ColorMessage(string msg, Color color)
		{
			StringBuilder stringBuilder;
			if (!msg.Contains("\n"))
			{
				stringBuilder = new StringBuilder(msg.Length + 12);
				stringBuilder.Append("[c/").Append(Utils.Hex3(color)).Append(':')
					.Append(msg)
					.Append(']');
			}
			else
			{
				stringBuilder = new StringBuilder();
				string[] array = msg.Split('\n');
				foreach (string value in array)
				{
					stringBuilder.Append("[c/").Append(Utils.Hex3(color)).Append(':')
						.Append(value)
						.Append(']')
						.Append('\n');
				}
			}
			return stringBuilder.ToString();
		}
		public static Rectangle GetCurrentFrame(Item item, ref int frame, ref int frameCounter, int frameDelay, int frameAmt, bool frameCounterUp = true)
		{
			if (frameCounter >= frameDelay)
			{
				frameCounter = -1;
				frame = ((frame != frameAmt - 1) ? (frame + 1) : 0);
			}
			if (frameCounterUp)
			{
				frameCounter++;
			}
			return new Rectangle(0, item.height * frame, item.width, item.height);

		}
	}
}