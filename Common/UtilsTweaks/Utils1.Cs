using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.World;
using System;
using System.Linq;
using System.Text;
using Terraria;

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
                    return true;
            }
            return false;
        }
        public static bool IsItemOnPlayerInventory(int id,Player player)
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
            if (Reaper.ReaperMode)
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
        public static int FormatTime(int Day, int Hour, int Minute, int Second)
        {
            return Second * 60 + Minute * 60 * 60 + Hour * 60 * 60 * 60 + Day * 60 * 60 * 60 * 60;
        }
        public static float FormatTime(float Day, float Hour, float Minute, float Second)
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

        public static bool NameHasWord(string word, string SeachedWord)
        {
            return word.Contains(SeachedWord) ? true : false;
        }
        public static int ReaperDropScaler(int i)
        {
            int a = i;
            if (Reaper.ReaperMode) a = i * 5;
            return a;
        }
    }
}