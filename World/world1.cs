using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using opswordsII;
using opswordsII.VanillaChanges;


namespace opswordsII.World
{
	public class world1 : ModSystem
	{
		public static bool ReaperMode;
		public static bool OneInMiddleMillion;
		public static bool SpawnTimeWithard;
		public bool SandWeapons;

		public override void OnWorldLoad()
		{
			SpawnTimeWithard = false;
			ReaperMode = false;
		}
		public override void OnWorldUnload()
		{
			SpawnTimeWithard = false;
			ReaperMode = false;
		}
		public override void SaveWorldData(TagCompound tag)
		{

			if (ReaperMode)
			{
				tag["ReaperMode"] = true;
			}

		}

		public override void LoadWorldData(TagCompound tag)
		{
			ReaperMode = tag.ContainsKey("ReaperMode");
		}

		public override void NetSend(BinaryWriter writer)
		{
			// Order of operations is important and has to match that of NetReceive
			var flags = new BitsByte();
			flags[0] = ReaperMode;


			writer.Write(flags);
		}
		public override void NetReceive(BinaryReader reader)
		{
			// Order of operations is important and has to match that of NetSend
			BitsByte flags = reader.ReadByte();
			ReaperMode = flags[0];

		}
	}
}
			