﻿using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.World
{
    public class Reaper : ModSystem
    {
        public static bool ReaperMode;
        //public static bool CanReaper;
        public override void OnWorldLoad()
        {
            ReaperMode = false;
            DificultyUtils.ReaperMode = false;
        }
        public override void OnWorldUnload()
        {
            ReaperMode = false;
            DificultyUtils.ReaperMode = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (ReaperMode) tag["ReaperMode"] = true;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            ReaperMode = tag.ContainsKey("ReaperMode");
            DificultyUtils.ReaperMode = tag.ContainsKey("ReaperMode");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = ReaperMode;
            flags[1] = DificultyUtils.ReaperMode; 
            writer.Write(flags);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ReaperMode = flags[0];
            DificultyUtils.ReaperMode = flags[1];

        }
    }
}

