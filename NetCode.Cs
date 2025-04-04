using RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad;
using RemnantOfTheAncientsMod.Content.NPCs;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod
{
    public class Netcode
    {
        public static void SyncWorld()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }

        public enum RemnantOfTheAncientsModMessageType : byte
        {
            SpawnSuperDummy,
            KillSuperDummy,
        }

        public static void HandlePacket(BinaryReader reader, int whoAmI)
        {
            Main.NewText("a");
            try
             {
                RemnantOfTheAncientsModMessageType msgType = (RemnantOfTheAncientsModMessageType)reader.ReadByte();
                switch (msgType)
                {
                    case RemnantOfTheAncientsModMessageType.KillSuperDummy:
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            SuperDummy.DeleteDummie();
                        break;
                    case RemnantOfTheAncientsModMessageType.SpawnSuperDummy:
                        int x = reader.ReadInt32();
                        int y = reader.ReadInt32();
                        int mode = reader.ReadInt32();
                        int owner = reader.ReadInt32();
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (owner == Main.myPlayer)
                                NPC.NewNPC(new EntitySource_WorldEvent(), x, y, ModContent.NPCType<SuperDummyNPC>(), 0, mode);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Mod MyMod = ModContent.GetInstance<RemnantOfTheAncientsMod>();
                if (e is EndOfStreamException eose)
                    MyMod.Logger.Warn("Failed to parse Remnant Of the ancients packet: Packet was too short, missing data, or otherwise corrupt.", eose);
                else if (e is ObjectDisposedException ode)
                    MyMod.Logger.Warn("Failed to parse Remnant Of the ancients packet: Packet reader disposed or destroyed.", ode);
                else if (e is IOException ioe)
                    MyMod.Logger.Warn("Failed to parse Remnant Of the ancients packet: An unknown I/O error occurred.", ioe);
                else
                    throw;
            }
        }
    }
}