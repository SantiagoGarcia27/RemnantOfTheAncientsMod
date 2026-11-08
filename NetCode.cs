using RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger;
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
            RequestToggleReaper
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

                        if (Main.netMode == NetmodeID.Server && playerID != whoAmI)
                            break;

                        if (playerID < 0 || playerID >= Main.maxPlayers)
                            break;

                        Player player_ = Main.player[playerID];
                        ReaperSoulsPlayer reaperPlayer = player_.GetModPlayer<ReaperSoulsPlayer>();

                        int count = reader.ReadInt32();

                        if (count < 0 || count > reaperPlayer.SoulsUpgradesLoaded.Count)
                            break;

                        for (int i = 0; i < count; i++)
                        {
                            string key = reader.ReadString();
                            bool loaded = reader.ReadBoolean();
                            float active = reader.ReadSingle();

                            if (!reaperPlayer.SoulsUpgradesLoaded.ContainsKey(key))
                                continue;

                            if (!float.IsFinite(active))
                                continue;

                            reaperPlayer.SoulsUpgradesLoaded[key] = loaded;
                            reaperPlayer.SoulsUpgradesLoadedActive[key] = active;
                        }

                        break;

                    case RemnantOfTheAncientsModMessageType.RequestToggleReaper:
                    {
                        if (Main.netMode != NetmodeID.Server)
                            break;

                        if (whoAmI < 0 || whoAmI >= Main.maxPlayers)
                            break;

                        Player player = Main.player[whoAmI];

                        ReaperToggler.TryToggleReaper(player);
                        break;
                    }
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