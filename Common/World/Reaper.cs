using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.World
{
    public class Reaper : ModSystem
    {
        public static bool ReaperMode;
        public override void OnWorldLoad()
        {
            ReaperMode = false;
 
            foreach (int id in RemnantGlobalItem.SpearsList)
            {
                Utils1.AddSecure(RemnantGlobalProjectile.Spears, new Item(id).shoot);
            }
        }
        public override void OnWorldUnload()
        {
            ReaperMode = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
           tag["ReaperMode"] = Reaper.ReaperMode;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            ReaperMode = tag.ContainsKey("ReaperMode");
      
            if (ReaperMode)
                ReaperMode = true;

        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = ReaperMode;
            writer.Write(flags);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ReaperMode = flags[0];
        }
    }
}

