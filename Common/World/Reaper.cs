using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common;
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
            if (RemnantGlobalNPC.DamageBonus == 1 || RemnantGlobalNPC.LifeBonus == 1 && ReaperMode)
            {
                RemnantGlobalNPC.setStatBonus(2f, 2f);
            }
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
           tag["ReaperMode"] = ReaperMode;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            ReaperMode = tag.GetBool("ReaperMode");
        }
        public static void UpdateReaper()
        {
            ReaperMode = !ReaperMode;
            DificultyUtils.ReaperMode = ReaperMode;

            if (ReaperMode) 
                RemnantGlobalNPC.setStatBonus(2f, 2f);
            else
                RemnantGlobalNPC.setStatBonus(1f, 1f,'-');
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

