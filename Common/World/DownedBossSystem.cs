using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Common.Systems
{
	public class RemnantDownedBossSystem : ModSystem
	{
		public static bool downedDesert = false;
		public static bool downedFrozen = false;
		public static bool downedTyrant = false;

		public override void OnWorldLoad()
		{
			downedDesert = false;
			downedFrozen = false;
			downedTyrant = false;
		}

		public override void OnWorldUnload()
		{
			downedDesert = false;
			downedFrozen = false;
			downedTyrant = false;
		}

		public override void SaveWorldData(TagCompound tag)
		{
			if (downedDesert) tag["downedDesert"] = true;		
			if (downedFrozen) tag["downedFrozen"] = true;	
			if (downedTyrant) tag["downedTyrant"] = true;		
		}

		public override void LoadWorldData(TagCompound tag)
		{
			downedDesert = tag.ContainsKey("downedDesert");
			downedFrozen = tag.ContainsKey("downedFrozen");
			downedTyrant = tag.ContainsKey("downedTyrant");
		}

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = downedDesert;
			flags[1] = downedFrozen;
			flags[2] = downedTyrant;

			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedDesert = flags[0];
			downedFrozen = flags[1];
			downedTyrant = flags[2];
		}
	}
}
