using CalamityMod.Items.Potions.Alcohol;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.Enums;
using RemnantOfTheAncientsMod.World;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
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
        public static List<Color> GetColorGradiant(Color firstColor, Color secondColor,float Ammount)
        {
            var colorList = new List<Color>();
            for (int i = 0; i < Ammount; i++)
            {
                var rAverage = firstColor.R + (int)((secondColor.R - firstColor.R) * i / Ammount);
                var gAverage = firstColor.G + (int)((secondColor.G - firstColor.G) * i / Ammount);
                var bAverage = firstColor.B + (int)((secondColor.B - firstColor.B) * i / Ammount);
                Color color = new(rAverage, gAverage, bAverage);
                colorList.Add(color);
            }
            return colorList;
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

        public static RemnantPlayer RemnantOfTheAncients(this Player player)
        {
            return player.GetModPlayer<RemnantPlayer>();
        }
        public static Item ActiveItem(this Player player)
        {
            return Main.mouseItem;
        }
   
        public static int FormatTime(int Day = 0, int Hour = 0, int Minute = 0, int Second = 0)
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
        public static float FormatTimeToTick(float Day = 0, float Hour = 0, float Minute= 0, float Second= 0)
        {
            return Second * 60 + Minute * 60 * 60 + Hour * 60 * 60 * 60 + Day * 60 * 60 * 60 * 60;
        }
        public static int FormatMoney(int Terra = 0, int Platinum = 0, int Gold = 0, int Silver = 0, int Copper = 0)
        {
            int _Copper = 1;
            int _Silver = 100;
            int _Gold = 10000;
            int _Platinum = 1000000;
            int _Terra = 100000000;
            int value = (Terra * _Terra) + (Platinum * _Platinum) + (Gold * _Gold) + (Silver * _Silver) + (Copper * _Copper);

            return value;
        }

        public static string FormatMoneyToString(int copperValue)
        {
            int platinum = copperValue / 1000000;
            int gold = (copperValue % 1000000) / 10000;
            int silver = (copperValue % 10000) / 100;
            int copper = copperValue % 100;

            List<string> parts = [];
            if (platinum > 0) parts.Add($"{platinum} [i:74]");
            if (gold > 0) parts.Add($"{gold} [i:73]");
            if (silver > 0) parts.Add($"{silver} [i:72]");
            if (copper > 0) parts.Add($"{copper} [i:71]");

            return parts.Count > 0 ? string.Join(" ", parts) : "0 [i:71]";
        }
      
        //public static float FormatToPositive(float imput)
        //{
        //    if (imput >= 0) return imput;
        //    return -imput;
        //}

        public static bool NameHasWord(string word, string SeachedWord)
        {
            return word.Contains(SeachedWord);
        }
        public static int ReaperDropScaler(int i)
        {
            int a = i;
            if (Reaper.ReaperMode) a = i * 5;
            return a;
        }
        public static Point ToPoint(Vector2 vector2)
        {
            return new Point((int)vector2.X, (int)vector2.Y);
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
            //  var tip = tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
            foreach (TooltipLine line in tooltips)
            {
                if (line != null)
                {
                    var text = line.Text.Replace(target, replace);
                    line.Text = text;
                }
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
        public static void ConsumeItem(Player player, Item[] inventory, int index)
        {
            if (index <= inventory.Length)
            {
                if (inventory[index].stack > 0)
                    inventory[index].stack--;
                else
                {
                    inventory[index].active = false;
                    inventory[index].TurnToAir();
                }
            }
        }
        public static Item ChooseAmmo(Item weapon, int ammoID)
        {
            var sItem = weapon;
            Item item = null;
            bool flag = false;
            Player player = Main.LocalPlayer;
            /*
            usedAmmoItemId = 0;
            */
            //bool canShoot = false; // Dummy.

            if (ammoID == AmmoID.Coin)
            {
                for (int i = 0; i < 4; i++)
                {
                    int num = 50 + i;

                    /*
                    if (inventory[num].ammo == sItem.useAmmo && inventory[num].stack > 0) {
                    */
                    if (player.inventory[num].stack > 0 && player.inventory[i].ammo == ammoID)
                    {
                        item = player.inventory[num];
                        //canShoot = true;
                        flag = true;
                        break;
                    }
                }
            }

            for (int j = 54; j < 58; j++)
            {
                /*
                if (inventory[j].ammo == sItem.useAmmo && inventory[j].stack > 0) {
                */
                if (player.inventory[j].stack > 0 && player.inventory[j].ammo == ammoID)
                {
                    item = player.inventory[j];
                    //canShoot = true;
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                for (int k = 0; k < 54; k++)
                {
                    /*
                    if (inventory[k].ammo == sItem.useAmmo && inventory[k].stack > 0) {
                    */
                    if (player.inventory[k].stack > 0 && player.inventory[k].ammo == ammoID)
                    {
                        item = player.inventory[k];
                        //canShoot = true;
                        break;
                    }
                }
            }

            return item;
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
                result = GetVainllaPickPower(tile, j);
            }

            if (Main.tileDungeon[tile.TileType] && j > Main.worldSurface)
            {
                result = 100;
            }

            return result;
        }

        private static int GetVainllaPickPower(Tile tile, int j)
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

        public static float bytetoFloat(byte ro)
        {
            return ((float)ro) / 0x100;
        }

        public static byte floattoByte(float ro)
        {
            return (byte)(ro * 0x100);
        }

        public static float toFloat(byte x)
        {
            return x / 255.0e7f;
        }

        public static byte fromFloat(float x)
        {
            if (x < 0) return 0;
            if (x > 1e-7) return 255;
            return Convert.ToByte((255.0e7 * x)); // this truncates; add 0.5 to round instead
        }

        private static Dictionary<Color, int> ColorIDs;
       

        public static Color GetColorByID(int id)
        {
            if (ColorIDs == null)
                FillColorIDs(ref ColorIDs);

            if (ColorIDs.ContainsValue(id))
            {
                Color c = ColorIDs.FirstOrDefault(x => x.Value == id).Key;
                return c;
            }
            return Color.White;
        }
        public static int GetColorID(Color color)
        {
            if (ColorIDs == null) 
                FillColorIDs(ref ColorIDs);
            if(color == Main.DiscoColor)
            {
                return -1;
            }

            if (ColorIDs.ContainsKey(color))
                return ColorIDs[color];
            return 0;
        }

        private static void FillColorIDs(ref Dictionary<Color, int> ColorIDs)
        {
            ColorIDs = new()
            {
                { Color.White, 0 },
                { Color.Red, 1 },
                { Color.Blue, 2 },
                { Color.Green, 3 },
                { Color.Yellow, 4 },
                { Color.Magenta, 5 },
                { Color.Orange, 6 },
                { Color.SkyBlue, 7 },
                { Color.Brown, 8 },
                { Color.Purple, 9 },
                { Color.Gray, 10 },
                { Color.Black, 11},
                { Color.CornflowerBlue, 12 },
                { Color.IndianRed, 13 },
                { Color.Cyan, 14 },
                { Color.OrangeRed, 15 },
                { Color.LightGreen, 16 },
                { Color.SandyBrown , 17 },
                { Color.PaleVioletRed , 18 },
                { Color.PaleGreen, 19 },
                { Color.Pink, 20 },
                { Color.Violet, 21 },
                { Color.Salmon, 22 },
                { Color.Indigo, 23 },
                { Color.LimeGreen, 24 },
                { Color.Gold, 25 },
                { Color.Goldenrod, 26 },
                { Color.GreenYellow, 27 },
                { Color.Lavender, 28 },
                { Color.DarkOrange, 29 }
            };
        }

        internal static Color GetColorByID(float v)
        {
            throw new NotImplementedException();
        }

        public static bool IsGameStage(GlobalEnum.gameStage stage)
        {
            switch(stage)
            {
                case GlobalEnum.gameStage.PostKingSlime : return NPC.downedSlimeKing;
                case GlobalEnum.gameStage.PostEyeOfCthulhu: return NPC.downedBoss1;
                case GlobalEnum.gameStage.PostCorruptBoss: return NPC.downedBoss2;
                case GlobalEnum.gameStage.PostQueenBee: return NPC.downedQueenBee;
                case GlobalEnum.gameStage.PostSkeletron: return NPC.downedBoss3;
                case GlobalEnum.gameStage.PostDeerclops: return NPC.downedDeerclops;
                case GlobalEnum.gameStage.PostQueenSlime: return NPC.downedQueenSlime;
                case GlobalEnum.gameStage.PostDestoryer: return NPC.downedMechBoss1;
                case GlobalEnum.gameStage.PostTwins: return NPC.downedMechBoss2;
                case GlobalEnum.gameStage.PostSkeletronPrime: return NPC.downedMechBoss3;
                case GlobalEnum.gameStage.PostMech: return NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
                case GlobalEnum.gameStage.PostPlantera: return NPC.downedPlantBoss;
                case GlobalEnum.gameStage.PostEmpressOfLight: return NPC.downedEmpressOfLight;
                case GlobalEnum.gameStage.PostGolem: return NPC.downedGolemBoss;
                case GlobalEnum.gameStage.PostCultist: return NPC.downedAncientCultist;
                case GlobalEnum.gameStage.PostMoonLord: return NPC.downedMoonlord;
                case GlobalEnum.gameStage.PostDukeFishron: return NPC.downedFishron;
                case GlobalEnum.gameStage.Hardmode: return Main.hardMode && !NPC.downedMoonlord;
                case GlobalEnum.gameStage.PreHardmode: return !Main.hardMode;
                case GlobalEnum.gameStage.PreBoss: return !NPC.downedBoss1;
                case GlobalEnum.gameStage.None : return false;
                default: return false;
            }
        }
        public static int MechBossDefeatedTierReworkForClamity()
        {
            if (!NPC.downedMechBossAny)
                return 1;
            if ((!NPC.downedMechBoss1 && !NPC.downedMechBoss2) || (!NPC.downedMechBoss2 && !NPC.downedMechBoss3) || (!NPC.downedMechBoss3 && !NPC.downedMechBoss1))
                return 2;
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                return 4;
            return 3;
        }

        public static bool playerHaveItem(Player player,int itemID, int amount = 1)
        {
            foreach (Item item in player.inventory)
            {
                if (item.type == itemID && item.stack >= amount)
                {
                    return true;
                }
            }
            foreach (Item item in player.armor)
            {
                if (item.type == itemID && item.stack >= amount)
                {
                    return true;
                }
            }
            foreach (Item item in player.miscEquips)
            {
                if (item.type == itemID && item.stack >= amount)
                {
                    return true;
                }
            }
            foreach (Item item in player.bank.item)
            {
                if (item.type == itemID && item.stack >= amount)
                {
                    return true;
                }
            }
            foreach (Item item in player.bank2.item)
            {
                if (item.type == itemID && item.stack >= amount)
                {
                    return true;
                }
            }
            foreach (Item item in player.bank3.item)
            {
                if (item.type == itemID && item.stack >= amount)
                {
                    return true;
                }
            }
            foreach (Item item in player.bank4.item)
            {
                if (item.type == itemID && item.stack >= amount)
                {
                    return true;
                }
            }
            foreach (Item item in player.dye)
            {
                if (item.type == itemID && item.stack >= amount)
                {
                    return true;
                }
            }
            foreach (Item item in player.miscDyes)
            {
                if (item.type == itemID && item.stack >= amount)
                {
                    return true;
                }
            }
            return false;
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color = default, float splits = 1f,float rotation = 0f, float scale = 1f, Texture2D texture = null)
        {
            if(texture == null) texture = (Texture2D)ModContent.Request<Texture2D>(RemnantOfTheAncientsMod.MagicPixelPath);

            spriteBatch.Draw(texture, start, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, end, null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, 1);
            float num = 1f / splits;
            for (float i = 0f; i < 1f; i += num)
            {
                spriteBatch.Draw(texture, Vector2.Lerp(start, end, i), null, color, rotation, Vector2.Zero, scale, SpriteEffects.None, 1);
            }
        }
    }
}