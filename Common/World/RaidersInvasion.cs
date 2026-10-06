using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlayerProxyLib.Common.ProxyPlayer;
using RemnantOfTheAncientsMod.Common.TmodClassOverride;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs.FakePlayer.Raider;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Common.Systems
{
    public class RaidersInvasionSystem : ModSystem
    {
        public static bool EventActive;
        public static int Kills;

        public const int RequiredKills = 50;
        public const int MaxEnemies = 5;

        private static int spawnTimer;
        private int MinSpawnDelay = Utils1.FormatTimeToTick(Second: 1.5f);   // 1.5 segundos
        private int MaxSpawnDelay = Utils1.FormatTimeToTick(Second: 4f);  // 4 segundos

        private static int attackDirection;

        public static Texture2D BarIcon
        {
            get
            {
                string path = "RemnantOfTheAncientsMod/Common/UI/InvasionBar/PlayerInvasion_Icon";
                Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(path);
                return texture ?? TextureAssets.Extra[ExtrasID.EventIconPirateInvasion].Value;
            }
        }

        public static string BarText => Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Messages.RaiderInvasions.DisplayName");
        public override void OnWorldLoad()
        {
            EventActive = false;
            Kills = 0;
        }

        public override void OnWorldUnload()
        {
            EventActive = false;
            Kills = 0;
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(EventActive);
            writer.Write(Kills);
        }

        public override void NetReceive(BinaryReader reader)
        {
            bool wasActive = EventActive;
            EventActive = reader.ReadBoolean();
            Kills = reader.ReadInt32();
            if (EventActive)
                ShowProgress();
            else if (wasActive && Main.netMode != NetmodeID.Server)
                Main.ReportInvasionProgress(0, 0, 0, 0);
        }

        public override void PostUpdateWorld()
        {
            if (!EventActive)
                return;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ShowProgress();
                return;
            }

            if (Main.netMode == NetmodeID.SinglePlayer)
                ShowProgress();

            int currentEnemies = 0;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && npc.type == ModContent.NPCType<Raider>())
                    currentEnemies++;
            }

            if (currentEnemies >= MaxEnemies)
                return;

            if (Kills + currentEnemies >= RequiredKills)
                return;

            if (spawnTimer > 0)
            {
                spawnTimer--;
                return;
            }

            if (SpawnRaider())
            {
                spawnTimer = Main.rand.Next(MinSpawnDelay, MaxSpawnDelay + 1);
            }
        }

        private static bool SpawnRaider()
        {
            List<Player> targets = new();

            foreach (Player player in Main.ActivePlayers)
            {
                if (player.dead || player.ghost || player.IsProxyPlayer()) continue;
                targets.Add(player);
            }

            if (targets.Count == 0)
                return false;

            Player target = targets[Main.rand.Next(targets.Count)];

            int distance = Main.rand.Next(800, 1200); //  50 a 75 bloques

            int spawnX = (int)target.Center.X + distance * attackDirection;

            int spawnY = (int)target.Center.Y + Main.rand.Next(-200, 100);

            Vector2 spawnPos = DistanceUtils.GetSecurePosition(new Vector2(spawnX, spawnY));

            int index = NPC.NewNPC(target.GetSource_Misc("RaiderInvasion"), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Raider>());

            if (index < 0 || index >= Main.maxNPCs)
                return false;

            Main.npc[index].netUpdate = true;

            return true;
        }

        public static void StartEvent()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || EventActive)
                return;

            EventActive = true;
            Kills = 0;

            spawnTimer = 30;

            ShowProgress();

            string startMessage = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Messages.RaiderInvasions.StartMessage");
            Announce(startMessage);

            SyncState();
        }

        public static void EnemyKilled()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !EventActive)
                return;

            Kills++;

            ShowProgress();

            if (Kills >= RequiredKills)
                EndEvent();
            else
                SyncState();
        }
        public static void EndEvent()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !EventActive)
                return;

            EventActive = false;
            Kills = 0;
            if (!RemnantDownedBossSystem.downedRaiderInvasion) RemnantDownedBossSystem.downedRaiderInvasion = true;
            if (Main.netMode != NetmodeID.Server)
                Main.ReportInvasionProgress(0, 0, 0, 0);

            string defeatMessage = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Messages.RaiderInvasions.DefeatMessage");
            Announce(defeatMessage);

            SyncState();
        }

        private static void ShowProgress()
        {
            if (Main.netMode != NetmodeID.Server)
                InvasionBarOverride.ReportInvasionProgress(Kills, RequiredKills, BarIcon, 1, BarText, new Color(165, 160, 155) * 0.5f);
        }

        private static void Announce(string message)
        {
            if (Main.netMode == NetmodeID.Server)
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), Color.MediumPurple);
            else
                Main.NewText(message, Color.MediumPurple);
        }

        private static void SyncState()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["RaidersInvasionActive"] = EventActive;
            tag["RaidersInvasionKills"] = Kills;
            base.SaveWorldData(tag);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            EventActive = tag.GetBool("RaidersInvasionActive");
            Kills = tag.GetInt("RaidersInvasionKills");
            base.LoadWorldData(tag);

        }
    }
}

