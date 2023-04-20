using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using RemnantOfTheAncientsMod;
using RemnantOfTheAncientsMod.VanillaChanges;
using System.Linq;

namespace RemnantOfTheAncientsMod.World
{
	public class world1 : ModSystem
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
			//ReaperMode = false;
			TimeDilocated = false;
		}
		public override void OnWorldUnload()
		{
			TimeWizardTimeAcelerationCouldown = 0;
			SpawnTimeWithard = false;
			//ReaperMode = false;
			TimeDilocated = false;
		}
		public override void SaveWorldData(TagCompound tag)
		{

			//if (ReaperMode)tag["ReaperMode"] = true;
			if (TimeDilocated) tag["TimeDilocated"] = true;

		}

		public override void LoadWorldData(TagCompound tag)
		{
			//ReaperMode = tag.ContainsKey("ReaperMode");
			TimeDilocated = tag.ContainsKey("TimeDilocated");
		}

		public override void NetSend(BinaryWriter writer)
		{
			// Order of operations is important and has to match that of NetReceive
			var flags = new BitsByte();
			//flags[0] = ReaperMode;
			flags[1] = TimeDilocated;

			writer.Write(flags);
		}
		public override void NetReceive(BinaryReader reader)
		{
			// Order of operations is important and has to match that of NetSend
			BitsByte flags = reader.ReadByte();
			//ReaperMode = flags[0];
			TimeDilocated = flags[1];
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

