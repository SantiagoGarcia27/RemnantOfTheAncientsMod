using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlayerProxyLib.Common;
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

namespace RemnantOfTheAncientsMod.Common.Systems
{
    public class RaidersInvasionSystem : ModSystem
    {
        public static bool EventActive;
        public static int Kills;

        public const int RequiredKills = 50;
        public const int MaxEnemies = 5;

        public static Texture2D BarIcon
        {
            get
            {
                string path = "RemnantOfTheAncientsMod/Common/UI/InvasionBar/PlayerInvasion_Icon";
                Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(path);
                return texture ?? TextureAssets.Extra[ExtrasID.EventIconPirateInvasion].Value;
            }
        }

        public static string BarText => "Player Invasion";
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

            // Contar cuántos Outlaws del evento hay actualmente
            int currentEnemies = 0;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && npc.type == ModContent.NPCType<Raider>())
                    currentEnemies++;
            }

            // Mantener hasta 5 Outlaws vivos
            while (currentEnemies < MaxEnemies && Kills + currentEnemies < RequiredKills)
            {
                if (!SpawnOutlaw())
                    break;
                currentEnemies++;
            }
        }

        private static bool SpawnOutlaw()
        {
            List<Player> targets = new();
            foreach (Player player in Main.ActivePlayers)
            {
                if (player.active && !player.dead && !player.ghost && !player.IsProxyPlayer())
                    targets.Add(player);
            }

            if (targets.Count == 0)
                return false;

            Player target = targets[Main.rand.Next(targets.Count)];
            int spawnX = (int)target.Center.X + (Main.rand.NextBool() ? Main.rand.Next(-1000, -800) : Main.rand.Next(800, 1000));
            int spawnY = (int)target.Center.Y - 100;
            Vector2 spawnPos = DistanceUtils.GetSecurePosition(new Vector2(spawnX, spawnY));
            int index = NPC.NewNPC(target.GetSource_Misc("OutlawInvasion"), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Raider>());
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
            ShowProgress();
            Announce("¡Los Outlaws han llegado!");
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
            if (!RemnantDownedBossSystem.downedOutlawInvasion) RemnantDownedBossSystem.downedOutlawInvasion = true;
            if (Main.netMode != NetmodeID.Server)
                Main.ReportInvasionProgress(0, 0, 0, 0);

            Announce("¡Los Outlaws han sufrido la derrota!");
            SyncState();
        }

        private static void ShowProgress()
        {
            if (Main.netMode != NetmodeID.Server)
                FakeMain.ReportInvasionProgress(Kills, RequiredKills, BarIcon, 1, BarText, new Color(165, 160, 155) * 0.5f);
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
    }
}

