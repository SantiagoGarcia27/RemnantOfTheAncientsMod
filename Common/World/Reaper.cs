using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using RemnantOfTheAncientsMod.Common.Global.Projectiles;
using SangarUtilities.Common.UtilsTweaks;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.World
{
    public class Reaper : ModSystem
    {
        public static bool ReaperMode;
       
        
        public override void OnWorldUnload()
        {
            ReaperMode = false;
            DificultyUtils.ReaperMode = false;

            RemnantGlobalNPC.DamageBonus = 1f;
            RemnantGlobalNPC.LifeBonus = 1f;
        }

        public override void SaveWorldData(TagCompound tag)
        {
           tag["ReaperMode"] = ReaperMode;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            ReaperMode = tag.GetBool("ReaperMode");
            LoadDataExtras();
        }
      
        internal static void LoadDataExtras()
        {
            ApplyReaperState();

            foreach (int id in RemnantGlobalItem.SpearsList)
            {
                ListUtils.AddSecure(ref RemnantGlobalProjectile.Spears, new Item(id).shoot);
            }
        }
        public static void UpdateReaper()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            ReaperMode = !ReaperMode;

            ApplyReaperState();

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData);
            }
        }

        private static void ApplyReaperState()
        {
            DificultyUtils.ReaperMode = ReaperMode;

            RemnantGlobalNPC.DamageBonus = ReaperMode ? 2f : 1f;
            RemnantGlobalNPC.LifeBonus = ReaperMode ? 2f : 1f;
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

            ApplyReaperState();
        }
    }
}

