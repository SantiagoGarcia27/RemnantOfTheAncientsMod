using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DesertAnnihilator;
using RemnantOfTheAncientsMod.Content.NPCs.FakePlayer.Outlaw;
using SteelSeries.GameSense;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Systems
{
    public class OutlawInvasionSystem : ModSystem
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
        public override void PostUpdateWorld()
        {
            if (!EventActive)
                return;

            // Contar cuántos Outlaws del evento hay actualmente
            int currentEnemies = 0;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && npc.type == ModContent.NPCType<Outlaw>())
                    currentEnemies++;
            }

            // Mantener hasta 5 Outlaws vivos
            while (currentEnemies < MaxEnemies && Kills + currentEnemies < RequiredKills)
            {
                SpawnOutlaw();
                currentEnemies++;
            }
        }

        private static void SpawnOutlaw()
        {
            Player player = Main.player[Main.myPlayer];

            if (player.whoAmI == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<Outlaw>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int spawnX = (int)player.Center.X + (Main.rand.NextBool() ? Main.rand.Next(-1000,-800) : Main.rand.Next(800, 1000));
                    int spawnY = (int)player.Center.Y - 100;
                    Vector2 spawnPos = new Vector2(spawnX, spawnY);
                    spawnPos = DistanceUtils.GetSecurePosition(spawnPos);

                    NPC.NewNPC(player.GetSource_Misc("OutlawInvasion"), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<Outlaw>()); 
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }
        }

        public static void StartEvent()
        {
            if (EventActive)
                return;

            EventActive = true;
            Kills = 0;
            FakeMain.ReportInvasionProgress(Kills, RequiredKills, BarIcon, 1, BarText, new Color(165, 160, 155) * 0.5f);
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("¡Los Outlaws han llegado!"), Color.MediumPurple);
        }

        public static void EnemyKilled()
        {
            if (!EventActive)
                return;

            Kills++;

            FakeMain.ReportInvasionProgress(Kills, RequiredKills, BarIcon, 1, BarText, new Color(165, 160, 155) * 0.5f);

            if (Kills >= RequiredKills)
            {
                EndEvent();
            }
        }
        public static void EndEvent()
        {
            EventActive = false;
            Kills = 0;
            if (!RemnantDownedBossSystem.downedOutlawInvasion) RemnantDownedBossSystem.downedOutlawInvasion = true;
            Main.ReportInvasionProgress(0, 0, 0, 0);
            
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("¡Los Outlaws han sufrido la derrota!"), Color.MediumPurple);
        }
    }
}

