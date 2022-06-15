using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace opswordsII
{
	public class TeleportClass : GlobalItem
	{
		public static void HandleTeleport(int teleportType = 0, bool forceHandle = false, int whoAmI = 0)
		{
			if (forceHandle || Main.netMode == 0)
			{
				TeleportPlayer(teleportType, forceHandle, whoAmI);
			}
			else
			{
				SyncTeleport(teleportType);
			}
		}

		private static void SyncTeleport(int teleportType = 0)
		{
			/*ModPacket packet = ((Mod)AlchemistNPC.instance).GetPacket(256);
			((BinaryWriter)(object)packet).Write((byte)1);
			((BinaryWriter)(object)packet).Write(teleportType);
			packet.Send(-1, -1);*/
		}

		private static void TeleportPlayer(int teleportType = 0, bool syncData = false, int whoAmI = 0)
		{
			Player player = Main.player[whoAmI];
			switch (teleportType)
			{
				case 0:
					HandleDungeonTeleport(player, syncData);
					break;
				case 1:
					HandleOceanTeleport(player, syncData);
					break;
				case 2:
					HandleOceanTeleportLeft(player, syncData);
					break;
				case 3:
					HandleBeachTeleportRight(player, syncData);
					break;
				case 4:
					HandleBeachTeleportLeft(player, syncData);
					break;
				case 5:
					HandleHellTeleport(player, syncData);
					break;
				case 6:
					HandleHellTeleportLeft(player, syncData);
					break;
				case 7:
					//HandleTempleTeleport(player, syncData);
					break;
				case 8:
					//HandleBeaconTeleport(player, syncData);
					break;
				case 9:
					//HandleJungleTeleport(player, syncData);
					break;
				case 10:
					//HandleJungleTeleportLeft(player, syncData);
					break;
			}
		}

		private static void HandleDungeonTeleport(Player player, bool syncData = false)
		{
			RunTeleport(player, new Vector2(Main.dungeonX, Main.dungeonY), syncData, convertFromTiles: true);
		}

		private static void HandleOceanTeleport(Player player, bool syncData = false)
		{
			Vector2 position = player.position;
			Vector2 vector = position;
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].TileType == 81)
					{
						vector = new Vector2((i + 1) * 16, (j - 16) * 16);
						break;
					}
				}
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}

		private static void HandleOceanTeleportLeft(Player player, bool syncData = false)
		{
			Vector2 position = player.position;
			Vector2 vector = position;
			for (int num = 8400; num > 0; num--)
			{
				for (int i = 0; i < Main.maxTilesX; i++)
				{
					if (Main.tile[num, i] != null && Main.tile[num, i].TileType == 81)
					{
						vector = new Vector2((num - 1) * 16, (i - 16) * 16);
						break;
					}
				}
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}

		private static void HandleHellTeleport(Player player, bool syncData = false)
		{
			Vector2 position = player.position;
			Vector2 vector = position;
			for (int i = 0; i < Main.maxTilesY; i++)
			{
				for (int j = 0; j < Main.maxTilesX; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].TileType == 75)
					{
						vector = new Vector2((i - 3) * 16, (j + 2) * 16);
						break;
					}
				}
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}

		private static void HandleHellTeleportLeft(Player player, bool syncData = false)
		{
			Vector2 position = player.position;
			Vector2 vector = position;
			for (int num = 8400; num > 0; num--)
			{
				for (int i = 0; i < Main.maxTilesX; i++)
				{
					if (Main.tile[num, i] != null && Main.tile[num, i].TileType == 75)
					{
						vector = new Vector2((num + 3) * 16, (i + 2) * 16);
						break;
					}
				}
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}

		private static void HandleBeachTeleportRight(Player player, bool syncData = false)
		{
			Vector2 position = player.position;
			Vector2 vector = position;
			int num = Main.maxTilesX - 32;
			for (int i = 0; i < Main.maxTilesY; i += 16)
			{
				if (Main.tile[num, i] == null || Main.tile[num, i].LiquidAmount != byte.MaxValue)
				{
					continue;
				}
				if (Main.tile[num, i].LiquidAmount == byte.MaxValue)
				{
					do
					{
						num -= 16;
					}
					while (Main.tile[num, i].LiquidAmount == byte.MaxValue);
				}
				if (Main.tile[num, i] != null && Main.tile[num, i].LiquidAmount == 0 && Main.tile[num, i].HasUnactuatedTile)
				{
					do
					{
						i -= 16;
					}
					while (Main.tile[num, i] != null && Main.tile[num, i].LiquidAmount == 0 && Main.tile[num, i].HasUnactuatedTile);
				}
				vector = new Vector2(num * 16, (i - 2) * 16);
				break;
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}

		private static void HandleBeachTeleportLeft(Player player, bool syncData = false)
		{
			Vector2 position = player.position;
			Vector2 vector = position;
			int num = 16;
			for (int i = 0; i < Main.maxTilesY; i += 16)
			{
				if (Main.tile[num, i] == null || Main.tile[num, i].LiquidAmount != byte.MaxValue)
				{
					continue;
				}
				if (Main.tile[num, i].LiquidAmount == byte.MaxValue)
				{
					do
					{
						num += 16;
					}
					while (Main.tile[num, i].LiquidAmount == byte.MaxValue);
				}
				if (Main.tile[num, i] != null && Main.tile[num, i].LiquidAmount == 0 && Main.tile[num, i].HasUnactuatedTile)
				{
					do
					{
						i -= 16;
					}
					while (Main.tile[num, i] != null && Main.tile[num, i].LiquidAmount == 0 && Main.tile[num, i].HasUnactuatedTile);
				}
				vector = new Vector2(num * 16, (i - 2) * 16);
				break;
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}

		/*private static void HandleJungleTeleport(Player player, bool syncData = false)
		{
			Vector2 position = player.position;
			Vector2 vector = position;
			int num = Main.maxTilesY;
			while ((double)num > Main.worldSurface - 150.0)
			{
				for (int i = 0; i < Main.maxTilesX; i++)
				{
					if (Main.tile[i, num] != null && Main.tile[i, num].type == 233)
					{
						vector = new Vector2(i * 16, (num - 2) * 16);
						break;
					}
				}
				num--;
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}*/

		/*private static void HandleJungleTeleportLeft(Player player, bool syncData = false)
		{
			Vector2 position = player.position;
			Vector2 vector = position;
			for (int num = Main.maxTilesY; num > 0; num--)
			{
				for (int num2 = Main.maxTilesX; num2 > 0; num2--)
				{
					if (Main.tile[num2, num] != null && Main.tile[num2, num].type == 384)
					{
						vector = new Vector2(num2 * 16, (num - 2) * 16);
						break;
					}
				}
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}*/

		/*private static void HandleTempleTeleport(Player player, bool syncData = false)
		{
			Vector2 position = player.position;
			Vector2 vector = position;
			for (int i = 0; i < Main.tile.GetLength(0); i++)
			{
				for (int j = 0; j < Main.tile.GetLength(1); j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].TileType == 237)
					{
						vector = new Vector2((i + 2) * 16, j * 16);
						break;
					}
				}
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}*/

		/*private static void HandleBeaconTeleport(Player player, bool syncData = false)
		{
			Mod mod = ModLoader.GetMod("AlchemistNPC");
			Vector2 position = player.position;
			Vector2 vector = position;
			for (int i = 0; i < Main.tile.GetLength(0); i++)
			{
				for (int j = 0; j < Main.tile.GetLength(1); j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].type == mod.TileType("Beacon"))
					{
						vector = new Vector2((i - 1) * 16, (j - 2) * 16);
						break;
					}
				}
			}
			if (vector != position)
			{
				RunTeleport(player, new Vector2(vector.X, vector.Y), syncData);
			}
		}*/

		private static void RunTeleport(Player player, Vector2 pos, bool syncData = false, bool convertFromTiles = false)
		{
			bool immune = player.immune;
			int immuneTime = player.immuneTime;
			if (convertFromTiles)
			{
				pos = new Vector2(pos.X * 16f + 8f - (float)(player.width / 2), pos.Y * 16f - (float)player.height);
			}
			LeaveDust(player);
			player.grappling[0] = -1;
			player.grapCount = 0;
			for (int i = 0; i < 1000; i++)
			{
				if (((Entity)Main.projectile[i]).active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].aiStyle == 7)
				{
					Main.projectile[i].Kill();
				}
			}
			player.Teleport(pos, 2, 0);
			player.velocity = Vector2.Zero;
			player.immune = immune;
			player.immuneTime = immuneTime;
			LeaveDust(player);
			if (Main.netMode == 2 && syncData)
			{
				RemoteClient.CheckSection(player.whoAmI, player.position, 1);
				NetMessage.SendData(65, -1, -1, (NetworkText)null, 0, (float)player.whoAmI, pos.X, pos.Y, 3, 0, 0);
			}
		}

		private static void LeaveDust(Player player)
		{
			for (int i = 0; i < 70; i++)
			{
				Dust obj = Main.dust[Dust.NewDust(player.position, player.width, player.height, 15, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 150, Color.Cyan, 1.2f)];
				obj.velocity *= 0.5f;
			}
			Main.TeleportEffect(player.getRect(), 1, 0, 1f);
			Main.TeleportEffect(player.getRect(), 3, 0, 1f);
		}
	}
}
