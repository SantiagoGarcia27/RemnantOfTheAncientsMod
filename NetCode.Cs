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

            SoulsUpgrades,
        }

        public static void HandlePacket(BinaryReader reader, int whoAmI)
        {
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
                            if (owner != whoAmI || mode < 0 || mode > 3)
                                return;

                            if (NPC.AnyNPCs(ModContent.NPCType<SuperDummyNPC>()))
                                return;

                            int i = NPC.NewNPC(new EntitySource_WorldEvent(), x, y, ModContent.NPCType<SuperDummyNPC>(), 0, mode);
                            Main.npc[i].releaseOwner = (short)owner;     
                        }
                        break;

                    case RemnantOfTheAncientsModMessageType.SoulsUpgrades:

                        int playerID = reader.ReadByte();

                        if (playerID < 0 || playerID >= Main.maxPlayers)
                            break;

                        Player player = Main.player[playerID];
                        ReaperSoulsPlayer reaperPlayer = player.GetModPlayer<ReaperSoulsPlayer>();


                        int loadedCount = reader.ReadInt32();
                        for (int i = 0; i < loadedCount; i++)
                        {
                            bool value = reader.ReadBoolean();

                            if (i < reaperPlayer.SoulsUpgradesLoaded.Length)
                                reaperPlayer.SoulsUpgradesLoaded[i] = value;
                        }

                        int activeCount = reader.ReadInt32();
                        for (int i = 0; i < activeCount; i++)
                        {
                            float value = reader.ReadSingle();
                            if (i < reaperPlayer.SoulsUpgradesLoadedActive.Length)
                                reaperPlayer.SoulsUpgradesLoadedActive[i] = value;
                        }

                        int maybeLoadedCount = reader.ReadInt32();
                        for (int i = 0; i < maybeLoadedCount; i++)
                        {
                            int key = reader.ReadInt32();
                            bool value = reader.ReadBoolean();

                            reaperPlayer.SoulsUpgradesMaybeLoaded[key] = value;
                        }

                        int maybeActiveCount = reader.ReadInt32();
                        for (int i = 0; i < maybeActiveCount; i++)
                        {
                            int key = reader.ReadInt32();
                            float value = reader.ReadSingle();

                            reaperPlayer.SoulsUpgradesMaybeLoadedActive[key] = value;
                        }

                        break;
                    default:

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