using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.World
{
	public class RemanntWorld : ModSystem
	{
		//public static bool ReaperMode;
		public static bool OneInMiddleMillion;
		public static bool SpawnTimeWithard;
		public static bool TimeDilocated;
		public static int TimeWizardTimeAcelerationCouldown;
		public bool SandWeapons;

		public override void OnWorldLoad()
		{
			TimeWizardTimeAcelerationCouldown = 0;
			SpawnTimeWithard = false;
			TimeDilocated = false;
		}
		public override void OnWorldUnload()
		{
			TimeWizardTimeAcelerationCouldown = 0;
			SpawnTimeWithard = false;
			TimeDilocated = false;
		}
		public override void SaveWorldData(TagCompound tag)
		{
			if (TimeDilocated) tag["TimeDilocated"] = true;
		}

		public override void LoadWorldData(TagCompound tag)
		{
			TimeDilocated = tag.ContainsKey("TimeDilocated");
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = TimeDilocated;

			writer.Write(flags);
		}
		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			TimeDilocated = flags[0];
		}
		public static bool ExistTileInWorld(int TileId)
		{

			int worldWidth = Main.maxTilesX;
			int worldHeight = Main.maxTilesY;

			for (int x = 0; x < worldWidth; x++)
			{
				for (int y = 0; y < worldHeight; y++)
				{
					if (WorldGen.TileType(x, y) == TileId)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static void KillTombstom()
		{
			int[] tombsID = new int[10]
			{
				201,202,203,204,205,527,528,529,530,531
			};

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
				for (int a = 0; a < 10; a++)
				{
					if (projectile.type == tombsID[a]) 
						projectile.Kill();
				}
            }
		}
	}
}
