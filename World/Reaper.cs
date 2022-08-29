using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        public override void OnWorldUnload()
        {
            ReaperMode = false;
        }
        /*public override void SaveWorldData(TagCompound tag)
        {
            if (ReaperMode) tag["ReaperMode"] = false;
            //else tag["ReaperMode"] = false;
        }
        public override void LoadWorldData(TagCompound tag)
        {
            ReaperMode = tag.ContainsKey("ReaperMode");
        }
        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = ReaperMode;
            writer.Write(flags);
        }
        public override void NetReceive(BinaryReader reader)
        {
            // Order of operations is important and has to match that of NetSend
            BitsByte flags = reader.ReadByte();
            ReaperMode = flags[0];
        }*/
    }
}
