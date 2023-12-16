using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class Utils1
    {
        internal static readonly Color ReaperRarityColor = new Color(48, 48, 48); // 
        internal static readonly Color LegendaryRarityColor = new Color(0, 255, 200);
        internal static readonly Color CompletistRarityColor = new Color(242, 231, 15);

        internal static bool IsABoss()
        {
            throw new NotImplementedException();
        }

        public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
        {
            float amount = (float)((Math.Sin(Math.PI * 2f / seconds * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
            return Color.Lerp(firstColor, secondColor, amount);
        }
        public static Color? GetRarityColor(CustomRarity rare)
        {
            switch (rare)
            {
                default:
                    return null;
                case CustomRarity.Reaper:
                    return ReaperRarityColor;
                case CustomRarity.Legendary:
                    return LegendaryRarityColor;
            }
        }

        public static bool anyBossIsAlive;

        public static bool IsAnyBossAlive()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && Main.npc[i].boss)
                {
                    NPC test = Main.npc[i];
                    return true;
                }
            }
            return false;
        }
        public static bool IsItemOnPlayerInventory(int id, Player player)
        {
            return player.inventory.Any(item => !item.IsAir && item.type == id);
        }
        public static string ColorMessage(string msg, Color color)
        {

            StringBuilder stringBuilder;
            if (!msg.Contains("\n"))
            {
                stringBuilder = new StringBuilder(msg.Length + 12);
                stringBuilder.Append("[c/").Append(Utils.Hex3(color)).Append(':')
                    .Append(msg)
                    .Append(']');
            }
            else
            {
                stringBuilder = new StringBuilder();
                string[] array = msg.Split('\n');
                foreach (string value in array)
                {
                    stringBuilder.Append("[c/").Append(Utils.Hex3(color)).Append(':')
            .Append(value)
            .Append(']')
            .Append('\n');
                }
            }
            return stringBuilder.ToString();
        }
        internal static Color GetRarityColor(Color rarityColorOne, Color rarityColorTwo)
        {
            return ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        }
        public static Color GetReaperColor(int x)
        {
            //Color color;// = new Color(100, 100, 100);
            if (x == 1) return new Color(46, 45, 45);
            else if (x == 2) return new Color(212, 210, 210);//191,187,187
            else return new Color(255, 255, 255);
            //return color;
        }
        public static void DroppItem(Player player, Item item, int drop, int numerator, int denominator)
        {
            int choice = Main.rand.Next(denominator);
            if (choice == 0) choice++;
            if (choice == numerator) player.QuickSpawnItem(item.GetSource_DropAsItem(), drop, 1);
        }
        public static int GetReaperNumber(int common, int reaper)
        {
            if (DificultyUtils.ReaperMode)
            {
                return reaper;
            }
            return common;
        }
        public static Rectangle GetCurrentFrame(Item item, ref int frame, ref int frameCounter, int frameDelay, int frameAmt, bool frameCounterUp = true)
        {
            if (frameCounter >= frameDelay)
            {
                frameCounter = -1;
                frame = frame != frameAmt - 1 ? frame + 1 : 0;
            }
            if (frameCounterUp)
            {
                frameCounter++;
            }
            return new Rectangle(0, item.height * frame, item.width, item.height);
        }

        public static RemnantPlayer RemnantOfTheAncientsMod(this Player player)
        {
            return player.GetModPlayer<RemnantPlayer>();
        }
        public static Item ActiveItem(this Player player)
        {
            return Main.mouseItem;
        }
        public static float GetValueFromPorcentage(float number, float porcentage)
        {
            return porcentage * number / 100;
        }
        public static float GetPorcentage(float intitialNumber, float total)
        {
            return intitialNumber * 100 / total;
        }
        public static int GetSign(float number)
        {
            return number < 0 ? -1 : 1;
        }
        public static int FormatTime(int Day, int Hour, int Minute, int Second)
        {
            int sec = Second;
            int min = 0;
            int hour = 0;
            int day = 0;
            if (Minute > 0)
            {
                min = Minute * 60;
            }
            if (Hour > 0)
            {
                hour = Hour * 60 * 60;
            }
            if (Day > 0)
            {
                day = Day * 60 * 60 * 24;
            }

            return day + hour + min + sec;
        }
        public static float FormatTimeToTick(float Day, float Hour, float Minute, float Second)
        {
            return Second * 60 + Minute * 60 * 60 + Hour * 60 * 60 * 60 + Day * 60 * 60 * 60 * 60;
        }
        public static Vector2 FormatToPositive(Vector2 imput)
        {
            Vector2 Output;
            Output.X = imput.X < 0 ? -imput.X : imput.X;
            Output.Y = imput.Y < 0 ? -imput.Y : imput.Y;
            return Output;
        }
        public static float FormatToPositive(float imput)
        {
            if (imput >= 0) return imput;
            return -imput;
        }

        public static bool NameHasWord(string word, string SeachedWord)
        {
            return word.Contains(SeachedWord) ? true : false;
        }
        public static int ReaperDropScaler(int i)
        {
            int a = i;
            if (DificultyUtils.ReaperMode) a = i * 5;
            return a;
        }
        public static DamageClass GetClassForHeldItem(Player player)
        {
            return player.HeldItem.DamageType;
        }
        public static Point ToPoint(Vector2 vector2)
        {
            return new Point((int)vector2.X, (int)vector2.Y);
        }
        public static bool NumberBetween(float min, float max, float number)
        {
            return (number - min) * (max - number) >= 0;
        }

        public static bool CanDevourerOfWorldsDrop(NPC npc)
        {
            return npc.boss && Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1;
        }
        public static bool CanDropCorruptBoss(NPC npc)
        {
            return CanDevourerOfWorldsDrop(npc) || npc.type == NPCID.BrainofCthulhu;
        }
        public static bool IsItemOnPlayerInventory(int type)
        {
            return Main.player[Main.myPlayer].inventory.Any(item => !item.IsAir && item.type == type);
        }
        public static bool NumberBetweenExact(float value, float min, float max)
        {
            if ((value > min && value < max))
            {
                return true;
            }
            else if (value == min || value == max)
            {
                return true;
            }

            return false;
        }
        public static double GenerateWave(float A, float t, float x)
        {
            return A * Math.Sin(2 * Math.PI * (((double)t / 10) + (double)x / 5));
        }
        public static bool IsAprilFoolDay()
        {
            return DateTime.Now.Month == 4 && DateTime.Now.Day == 1;
        }
        public static bool IsDate(int day)
        {
            return DateTime.Now.Day == day;
        }
        public static bool IsDate(int day, int month)
        {
            return DateTime.Now.Day == day && DateTime.Now.Month == month;
        }
        public static bool IsDate(int day, int month, int year)
        {
            return DateTime.Now.Day == day && DateTime.Now.Month == month && DateTime.Now.Year == year;
        }

        public static void AddSecure(List<int> list, int p)
        {
            if (!list.Contains(p)) list.Add(p);
        }
        public static void AddSecure(List<string> list, string p)
        {
            if (!list.Contains(p)) list.Add(p);
        }
        public static string TooltipHotkeyString(ModKeybind imput)
        {
            try
            {
                return imput.GetAssignedKeys()[0];
            }
            catch
            {
                return "[...]";
            }
        }
        public static void FindAndReplaceTooltip(List<TooltipLine> tooltips,string target, string replace)
        {
            var tip = tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
            if (tip != null)
            {
                var text = tip.Text.Replace(target, replace);
                tip.Text = text;
            }
        }
        public static void FindAndReplaceTooltip(List<TooltipLine> tooltips, string target, string replace,string TooltipName)
        {
            var tip = tooltips.FirstOrDefault(tip => tip.Name == TooltipName && tip.Mod == "Terraria");
            if (tip != null)
            {
                var text = tip.Text.Replace(target, replace);
                tip.Text = text;
            }
        }
    }
}