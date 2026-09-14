using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Common.Systems
{
	public class RemnantDownedBossSystem : ModSystem
	{
		public static bool downedCrusher = false;
        public static bool downedDesert = false;
		public static bool downedFrozen = false;
		public static bool downedTyrant = false;


		public static bool downedOutlawInvasion = false;

        public override void OnWorldLoad()
		{
            downedCrusher = false;
            downedDesert = false;
			downedFrozen = false;
			downedTyrant = false;

			downedOutlawInvasion =false;
		}

		public override void OnWorldUnload()
		{
            downedCrusher = false;
            downedDesert = false;
			downedFrozen = false;
			downedTyrant = false;

            downedOutlawInvasion = false;
        }

		public override void SaveWorldData(TagCompound tag)
		{
			if(downedCrusher) tag["downedCrusher"] = true;
			if (downedDesert) tag["downedDesert"] = true;		
			if (downedFrozen) tag["downedFrozen"] = true;	
			if (downedTyrant) tag["downedTyrant"] = true;		

			if(downedOutlawInvasion) tag["downedOutlawInvasion"] = true;
        }

		public override void LoadWorldData(TagCompound tag)
		{
            downedCrusher = tag.ContainsKey("downedCrusher");
            downedDesert = tag.ContainsKey("downedDesert");
			downedFrozen = tag.ContainsKey("downedFrozen");
			downedTyrant = tag.ContainsKey("downedTyrant");

			downedOutlawInvasion = tag.ContainsKey("downedOutlawInvasion");
        }

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();

			flags[0] = downedCrusher;
            flags[1] = downedDesert;
			flags[2] = downedFrozen;
			flags[3] = downedTyrant;

			flags[4] = downedOutlawInvasion;

            writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();

			downedCrusher = flags[0];
            downedDesert = flags[1];
			downedFrozen = flags[2];
			downedTyrant = flags[3];

			downedOutlawInvasion = flags[4];
        }
	}
}
