using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Exceptions;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class Utils1
    {
        internal static readonly Color ReaperRarityColor = new(48, 48, 48);
        internal static readonly Color LegendaryRarityColor = new(0, 255, 200);
        internal static readonly Color CompletistRarityColor = new(242, 231, 15);

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
            int i = 0;
            while (!Main.npc[i].boss && i != Main.maxNPCs)
            {
                i++;
            }
            return Main.npc[i].active && i < Main.maxNPCs;
        }
        public static bool IsItemOnPlayerInventory(int id, Player player)
        {
            return player.inventory.Any(item => !item.IsAir && item.type == id);
        }
        internal static Color GetRarityColor(Color rarityColorOne, Color rarityColorTwo)
        {
            return ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        }
        public static Color GetReaperColor(int x)
        {
            if (x == 1) return new Color(46, 45, 45);
            else if (x == 2) return new Color(212, 210, 210);
            else return new Color(255, 255, 255);
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
        public static int FormatMoney(int Terra, int Platinum, int Gold, int Silver, int Copper)
        {
            int _Copper = 1;
            int _Silver = 100;
            int _Gold = 10000;
            int _Platinum = 1000000;
            int _Terra = 100000000;
            int value = (Terra * _Terra) + (Platinum * _Platinum) + (Gold * _Gold) + (Silver * _Silver) + (Copper * _Copper);

            return value;
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
            return word.Contains(SeachedWord);
        }
        public static int ReaperDropScaler(int i)
        {
            int a = i;
            if (DificultyUtils.ReaperMode) a = i * 5;
            return a;
        }
        public static Point ToPoint(Vector2 vector2)
        {
            return new Point((int)vector2.X, (int)vector2.Y);
        }
        public static bool NumberBetween(float min, float max, float number)
        {
            return number <= max && number >= min;
        }

        public static bool CanDevourerOfWorldsDrop(NPC npc)
        {
            return npc.boss && Array.IndexOf([NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail], npc.type) > -1;
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
        public static void AddSecure(List<int> list, List<int> p)
        {
            foreach (int i in p)
            {
                if (!list.Contains(i)) list.Add(i);
            }
        }
        public static void AddSecure(List<int> list, int[] p)
        {
            foreach (int i in p)
            {
                if (!list.Contains(i)) list.Add(i);
            }
        }
        public static void AddSecure(List<Type> list, Type p)
        {
            if (!list.Contains(p)) list.Add(p);
        }
        public static void AddSecure(List<Asset<Texture2D>> list, Asset<Texture2D> p)
        {
            if (!list.Contains(p)) list.Add(p);
        }
        public static void AddSecure(List<string> list, string p)
        {
            if (!list.Contains(p)) list.Add(p);
        }
        public static bool OnlyOneValue(List<bool> list, bool a)
        {
            List<bool> result = [];
            foreach (bool e in list)
            {
                if (e == a) result.Add(e);
            }

            return result.Count == 1;
        }
        public static bool OnlyOneValue(List<bool> list, bool a, int index)
        {
            if (list[index] != a) return false;
            List<bool> result = [];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == a)
                    result.Add(list[i]);
            }


            return result.Count == 1;
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
        public static void FindAndReplaceTooltip(List<TooltipLine> tooltips, string target, string replace)
        {
            var tip = tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
            if (tip != null)
            {
                var text = tip.Text.Replace(target, replace);
                tip.Text = text;
            }
        }
        public static void FindAndReplaceTooltip(List<TooltipLine> tooltips, string target, string replace, string TooltipName)
        {
            var tip = tooltips.FirstOrDefault(tip => tip.Name == TooltipName && tip.Mod == "Terraria");
            if (tip != null)
            {
                var text = tip.Text.Replace(target, replace);
                tip.Text = text;
            }
        }

        public static int GetRecipeGroupId(string name)
        {
            if (!RecipeGroup.recipeGroupIDs.TryGetValue(name, out int value))
            {
                throw new RecipeException("A recipe group with the name " + name + " does not exist.");
            }
            return value;
        }

        public static NPC SearchNPC(int Id)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.type == Id)
                {
                    return npc;
                }
            }
            return null;
        }
        public static NPC SearchNPC(int Id, bool Shimered = false)
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.type == Id && npc.IsShimmerVariant == Shimered)
                {
                    return npc;
                }
            }
            return null;
        }
        public static List<Projectile> SearchProjectiles(bool friendly, Vector2 center, float range)
        {
            List<Projectile> l = [];
            foreach (Projectile p in Main.projectile)
            {
                if (Vector2.Distance(center, p.Center) <= range)
                {
                    if (p.friendly == friendly && p.hostile == !friendly)
                    {
                        l.Add(p);
                    }
                }
            }
            return l;
        }
        public static Projectile SearchFirstProjectile(int id, bool friendly = false, bool hostile = false)
        {
            foreach (Projectile p in Main.projectile)
            {
                if (p.friendly == friendly && p.hostile == hostile && p.type == id)
                {
                     return p;
                }
            }
            return null;


        }
        public static List<Projectile> SearchProjectiles(bool friendly, NPC center, float range)
        {
            List<Projectile> l = [];
            foreach (Projectile p in Main.projectile)
            {
                if (Vector2.Distance(center.Center, p.Center) <= range)
                {
                    if (p.friendly == friendly && p.hostile == !friendly)
                    {
                        l.Add(p);
                    }
                }
            }
            return l;
        }
        public static bool PlayerIsUnderWather(Player player)
        {
            return Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);
        }

        public static int SearchPlayerAmmoSlot(Player player, int UseAmmo, ref Item Item)
        {
            int i = 54;
            while (player.inventory[i].ammo != UseAmmo)
            {
                i++;
            }
            if (i < 58)
            {
                Item = player.inventory[i];
                return i;
            }
            else
            {
                i = 0;
                while (player.inventory[i].ammo != UseAmmo && i <= 50)
                {
                    i++;
                }
                if (i < 58)
                {
                    Item = player.inventory[i];
                    return i;
                }
            }
            return -1;
        }
        public static int GetBlockPickPower(Tile tile, int j)
        {
            int result = 0;
            if (Main.tileNoFail[tile.TileType])
            {
                return result;
            }

            ModTile tile_ = TileLoader.GetTile(tile.TileType);
            if (tile_ != null)
            {
                result = tile_.MinPick;
            }
            else
            {
                result = GetVainllaPickPower(tile,  j);
            }

            if (Main.tileDungeon[tile.TileType] && j > Main.worldSurface)
            {
                result = 100;
            }

            return result;
        }

        private static int GetVainllaPickPower(Tile tile,int j)
        {
            if (tile.TileType == 211)
               return 200;
            else if (tile.TileType == 25 || tile.TileType == 58 || tile.TileType == 117 || tile.TileType == 203)
               return 65;
            else if (tile.TileType == 56)
               return 55;
            else if (tile.TileType == 37)
               return 50;
            else if (tile.TileType == 204 && j > Main.worldSurface)
               return 55;
            else if (tile.TileType == 226 || tile.TileType == 237)
               return 210;
            else if (tile.TileType == 107 || tile.TileType == 221)
               return 100;
            else if (tile.TileType == 108 || tile.TileType == 222)
               return 110;
            else if (tile.TileType == 111 || tile.TileType == 223)
               return 150;
            return 0;
        }
        public static int GetBestPickPower(Player player)
        {
            int num = 35;
            foreach (Item item in player.inventory)
            {
                if (item.pick > 0 && item.pick > num)
                {
                    num = item.pick;
                }
            }  
            return num;
        }
    }
}