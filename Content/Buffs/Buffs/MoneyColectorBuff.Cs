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
           // //DisplayName.SetDefault("Money Collector");
            ////Description.SetDefault("");
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<RemnantPlayer>().MoneyCollector = true;
        }
        internal static void UpdateCoins(Player player)
        {

            long copper = 0L;
            long silver = 0L;
            long gold = 0L;
            long platinum = 0L;
            long terra = 0L;
            int slots = 0;
            int currentslot = 0;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;

            int num2 = 0;
            int num3 = CalculateSlots(player, ref copper, ref silver, ref gold, ref platinum, ref terra, ref slots);

            if (num3 == -1)
            {
                return;
            }
            for (int i = 0; i < 40; i++)
            {
                Item item = player.bank.item[i];
                if (item.IsAir || (item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin))
                {
                    currentslot++;
                    if (currentslot >= slots)
                    {
                        break;
                    }
                }
            }
            if (currentslot < slots)
            {
                return;
            }
            for (int j = 50; j < 54; j++)
            {
                Item item = player.inventory[j];
                if (item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin || item.type == ModContent.ItemType<Terracoin>())
                {
                    player.inventory[j].TurnToAir();
                }
            }
            for (int k = 0; k < 40; k++)
            {
                Item item = player.bank.item[k];
                if (item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin || item.type == ModContent.ItemType<Terracoin>())
                {
                    //player.bank.item[k].TurnToAir();
                }
            }
            for (int i = 0; i <= player.bank.item.Length - 1; i++)
            {
                if (!player.bank.item[i].IsAir)
                {
                    player.bank.item[i].active = true;
                }
                if (player.bank.item[i].DamageType == DamageClass.Ranged)
                {
                    if (player.bank.item[i].type == ItemID.CopperCoin)
                    {
                        if (copper > 0)
                        {
                            if (player.bank.item[i].stack + copper < 100)
                            {
                                player.bank.item[i].stack += (int)copper;
                                return;
                            }
                            else if (player.bank.item[i].stack + copper == 100)
                            {
                                copper = 0;
                                silver += 1;
                            }
                            else if (player.bank.item[i].stack + copper >= 100)
                            {
                                silver += copper / 100;
                                copper = copper % 100;
                                
                            }
                        }
                    }
                    else if (player.bank.item[i].type == ItemID.SilverCoin)
                    {
                        if (silver > 0)
                        {
                            if (player.bank.item[i].stack + silver < 100)
                            {
                                player.bank.item[i].stack += (int)silver;
                                return;
                            }
                            else if (player.bank.item[i].stack + silver == 100)
                            {
                                silver = 0;
                                gold += 1;
                            }
                            else if (player.bank.item[i].stack + silver >= 100)
                            {
                                gold += silver / 100;
                                silver = silver % 100;
                                
                            }
                        }
                    }
                    else if (player.bank.item[i].type == ItemID.GoldCoin)
                    {
                        if (gold > 0)
                        {
                            if (player.bank.item[i].stack + gold < 100)
                            {
                                player.bank.item[i].stack += (int)gold;
                                return;
                            }
                            else if (player.bank.item[i].stack + gold == 100)
                            {
                                gold = 0;
                                platinum += 1;
                            }
                            else if (player.bank.item[i].stack + gold >= 100)
                            {
                                platinum += gold / 100;
                                gold = gold % 100;
                                
                            }
                        }
                    }
                    else if (player.bank.item[i].type == ItemID.PlatinumCoin)
                    {
                        if (platinum > 0)
                        {
                            if (player.bank.item[i].stack + platinum < 100)
                            {
                                player.bank.item[i].stack += (int)platinum;
                                return;
                            }
                            else if (player.bank.item[i].stack + platinum == 100)
                            {
                                platinum = 0;
                                terra += 1;
                            }
                            else if (player.bank.item[i].stack + platinum >= 100)
                            {
                                terra += platinum / 100;
                                platinum = platinum % 100;
                               
                            }
                        }
                    }
                    else if (player.bank.item[i].type == ModContent.ItemType<Terracoin>())
                    {
                        if (terra > 0)
                        {
                            player.bank.item[i].stack += (int)terra;
                            return;
                        }
                    }
                }
                else
                {
                    if (player.bank.item[i].IsAir)
                    {
                        player.bank.item[i] = new Item();

                        if (terra > 0 && !flag5)
                        {
                            player.bank.item[i].SetDefaults(ModContent.ItemType<Terracoin>());
                            player.bank.item[i].stack = (int)terra;
                            flag5 = true;
                        }
                        else if (platinum > 0 && !flag4)
                        {
                            player.bank.item[i].SetDefaults(ItemID.PlatinumCoin);
                            player.bank.item[i].stack = (int)platinum;
                            flag4 = true;
                        }
                        else if (gold > 0 && !flag3)
                        {
                            player.bank.item[i].SetDefaults(ItemID.GoldCoin);
                            player.bank.item[i].stack = (int)gold;
                            flag3 = true;
                        }
                        else if (silver > 0 && !flag2)
                        {
                            player.bank.item[i].SetDefaults(ItemID.SilverCoin);
                            player.bank.item[i].stack = (int)silver;
                            flag2 = true;
                        }
                        else if (copper > 0 && !flag)
                        {
                            player.bank.item[i].SetDefaults(ItemID.CopperCoin);
                            player.bank.item[i].stack = (int)copper;
                            flag = true;
                        }
                    }
                }
            }
            if (Main.playerInventory)
            {
                Recipe.FindRecipes(false);
            }
        }

        public static List<int> CoinsList = new List<int>()
        {
            ItemID.CopperCoin,
            ItemID.SilverCoin,
            ItemID.GoldCoin,
            ItemID.PlatinumCoin,
            ModContent.ItemType<Terracoin>()
        };
        private static int CalculateSlots(Player player, ref long copper, ref long silver, ref long gold, ref long platinum, ref long terra, ref int slots)
        {
            int num = 0;
            int exponent = 0;
            for (int i = 50; i < 54; i++)
            {
                Item item = player.inventory[i];

                if (CoinsList.Contains(item.type))
                {
                    for(int l = 0; l < CoinsList.Count; l++)
                    {
                        if (CoinsList[l] == item.type)
                        {
                            exponent = l;
                        }
                    }
                    copper += (long)(item.stack * Math.Pow(100.0, exponent));
                }
            }
            if (copper > 0)
            {
                for (int j = 0; j < 40; j++)
                {
                    Item item2 = player.bank.item[j];
                    //if (item2.type >= ItemID.CopperCoin && item2.type <= ItemID.PlatinumCoin || item2.type == ModContent.ItemType<Terracoin>())
                    //{
                    //    copper += (long)(item2.stack * Math.Pow(100.0, exponent));
                    //}
                }
                ValueCalc(ref copper, ref silver, ref gold, ref platinum, ref terra);
                if (terra > 0)
                {
                    num = (int)((terra % 999 == 0L) ? (terra / 999) : (terra / 999 + 1));
                }
                slots = ((platinum > 0) ? 1 : 0) + ((gold > 0) ? 1 : 0) + ((silver > 0) ? 1 : 0) + ((copper > 0) ? 1 : 0) + num;
                return num;
            }
            return -1;
        }

        private static void ValueCalc(ref long copper, ref long silver, ref long gold, ref long platinum, ref long terra)
        {
            int copperValue = 1;
            int silverValue = copperValue * 100;
            int goldValue = silverValue * 100;
            int platinumValue = goldValue * 100;
            int terraValue = platinumValue * 100;

            terra = copper / terraValue;
            copper -= terra * terraValue;

            platinum = copper / platinumValue;
            copper -= platinum * platinumValue;

            gold = copper / goldValue;
            copper -= gold * goldValue;

            silver = copper / silverValue;
            copper -= silver * silverValue;
        }
    }
}