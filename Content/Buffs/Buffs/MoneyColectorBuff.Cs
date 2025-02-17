using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Items;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace RemnantOfTheAncientsMod.Content.Buffs.Buffs
{
    public class MoneyColectorBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<RemnantPlayer>().MoneyCollector = true;
        }

        internal static void UpdateCoins(Player p)
        {
            if (p.whoAmI != Main.myPlayer)
            {
                return;
            }
            long copper = 0L;
            long silver = 0L;
            long gold = 0L;
            long platinum = 0L;
            int slots = 0;
            int num = 0;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            int num2 = 0;
            int num3 = CalculateSlots(p, ref copper, ref silver, ref gold, ref platinum, ref slots);
            
            
            if (num3 == -1)
            {
                return;
            }
            for (int i = 0; i < 40; i++)
            {
                Item item = p.bank.item[i];
                if (item.IsAir || (item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin))
                {
                    num++;
                    if (num >= slots) 
                        break;  
                }
            }
            if (num < slots)
            {
                return;
            }   

            ClearCoins(ref p.inventory, 50, 54);
            ClearCoins(ref p.bank.item, 0, 40);

            for (int slot = 39; slot >= 0; slot--)
            {
                if (p.bank.item[slot].IsAir)
                {
                    if (num2 + 1 < num3)
                    {
                        p.bank.item[slot] = new Item();
                        p.bank.item[slot].SetDefaults(ItemID.PlatinumCoin);
                        p.bank.item[slot].stack = 9999;
                        platinum -= 9999;
                        num2++;
                    }
                    else if (num2 + 1 == num3)
                    {
                        p.bank.item[slot] = new Item();
                        p.bank.item[slot].SetDefaults(ItemID.PlatinumCoin);
                        p.bank.item[slot].stack = (int)platinum;
                        num2++;
                    }
                    else if (!flag3 && gold > 0)
                    {
                        p.bank.item[slot] = new Item();
                        p.bank.item[slot].SetDefaults(ItemID.GoldCoin);
                        p.bank.item[slot].stack = (int)gold;
                        flag3 = true;
                    }
                    else if (!flag2 && silver > 0)
                    {
                        p.bank.item[slot] = new Item();
                        p.bank.item[slot].SetDefaults(ItemID.SilverCoin);
                        p.bank.item[slot].stack = (int)silver;
                        flag2 = true;
                    }
                    else if (!flag && copper > 0)
                    {
                        p.bank.item[slot] = new Item();
                        p.bank.item[slot].SetDefaults(ItemID.CopperCoin);
                        p.bank.item[slot].stack = (int)copper;
                        flag = true;
                        break;
                    }
                }
            }
            if (Main.playerInventory)
            {
                Recipe.FindRecipes();
            }


            void ClearCoins(ref Item[] inventory, int start, int end)
            {
                for (int j = start; j < end; j++)
                {
                    Item item = inventory[j];
                    if (item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin)
                    {
                        inventory[j].TurnToAir();
                    }
                }
            }
        }

        private static int CalculateSlots(Player player, ref long copper, ref long silver, ref long gold, ref long platinum, ref int slots)
        {
            int num = 0;
            
            CalculateCopper(player.inventory, 50, 54, ref copper);
            if (copper > 0)
            {
                CalculateCopper(player.bank.item, 0, 40, ref copper); 
                GetCoinValue(ref copper, ref silver, ref gold, ref platinum);
                if (platinum > 0)
                {
                    num = (int)((platinum % 9999 == 0L) ? (platinum / 9999) : (platinum / 9999 + 1));
                }
                slots = ((gold > 0) ? 1 : 0) + ((silver > 0) ? 1 : 0) + ((copper > 0) ? 1 : 0) + num;
                return num;
            }
            return -1;



            void CalculateCopper(Item[] inventory,int min, int max,ref long copper)
            {
                for (int j = min; j < max; j++)
                {
                    Item item = inventory[j];
                    if (item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin)
                    {
                        copper += (long)(item.stack * Math.Pow(100.0, item.type - 71));
                    }
                }
            }
        }

        private static void GetCoinValue(ref long copper, ref long silver, ref long gold, ref long platinum)
        {
            platinum = copper / 1000000;
            copper -= platinum * 1000000;
            gold = copper / 10000;
            copper -= gold * 10000;
            silver = copper / 100;
            copper -= silver * 100;
        }
    }
}