using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using RemnantOfTheAncientsMod;
using System.Text;
using Terraria.ModLoader;
using Terraria.ID;

namespace RemnantOfTheAncientsMod
{
	public static class Utils1
	{
		internal static readonly Color ReaperRarityColor = new Color(48, 48, 48); // 
		internal static readonly Color LegendaryRarityColor = new Color(0, 255, 200);

		
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
		public static Player1 RemnantOfTheAncientsMod(this Player player)
		{
			return player.GetModPlayer<Player1>();
		}
		public static Item ActiveItem(this Player player)
		{
			return Main.mouseItem;	
		}
	}
}