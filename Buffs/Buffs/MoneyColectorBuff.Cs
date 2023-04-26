using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.Items.Items;

namespace RemnantOfTheAncientsMod.Buffs.Buffs
{
    public class MoneyColectorBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Money Collector");
            //Description.SetDefault("");
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
            int num = 0;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            int num2 = 0;
            int num3 = CalculateSlots(player, ref copper, ref silver, ref gold, ref platinum, ref terra, ref slots);
            if (num3 == -1)
            {
                return;
            }
            for (int i = 0; i < 40; i++)
            {
                Item item = player.bank.item[i];
                if (item.IsAir || (item.type >= 71 && item.type <= ItemID.PlatinumCoin))
                {
                    num++;
                    if (num >= slots)
                    {
                        break;
                    }
                }
            }
            if (num < slots)
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
                    player.bank.item[k].TurnToAir();
                }
            }
            for (int i = 39; i >= 0; i--)
            {
                if (player.bank.item[i].IsAir)
                {
                    if (num2 + 1 < num3)
                    {
                        player.bank.item[i] = new Item();
                        player.bank.item[i].SetDefaults(74);
                        player.bank.item[i].stack = 999;
                        platinum -= 999;
                        num2++;
                    }
                    else if (num2 + 1 == num3)
                    {
                        player.bank.item[i] = new Item();
                        player.bank.item[i].SetDefaults(74);
                        player.bank.item[i].stack = (int)platinum;
                        num2++;
                    }
                    else if (!flag3 && gold > 0)
                    {
                        player.bank.item[i] = new Item();
                        player.bank.item[i].SetDefaults(73);
                        player.bank.item[i].stack = (int)gold;
                        flag3 = true;
                    }
                    else if (!flag2 && silver > 0)
                    {
                        player.bank.item[i] = new Item();
                        player.bank.item[i].SetDefaults(72);
                        player.bank.item[i].stack = (int)silver;
                        flag2 = true;
                    }
                    else if (!flag && copper > 0)
                    {
                        player.bank.item[i] = new Item();
                        player.bank.item[i].SetDefaults(71);
                        player.bank.item[i].stack = (int)copper;
                        flag = true;
                        break;
                    }
                }
            }
            if (Main.playerInventory)
            {
                Recipe.FindRecipes(false);
            }
        }

        private static int CalculateSlots(Player player, ref long copper, ref long silver, ref long gold, ref long platinum, ref long terra, ref int slots)
        {
            int num = 0;
            for (int i = 50; i < 54; i++)
            {
                Item item = player.inventory[i];
                if (item.type >= ItemID.CopperCoin && item.type <= ItemID.PlatinumCoin)
                {
                    copper += (long)(item.stack * Math.Pow(100.0, item.type - 71));
                }
            }
            if (copper > 0)
            {
                for (int j = 0; j < 40; j++)
                {
                    Item item2 = player.bank.item[j];
                    if (item2.type >= ItemID.CopperCoin && item2.type <= ItemID.PlatinumCoin)
                    {
                        copper += (long)(item2.stack * Math.Pow(100.0, item2.type - 71));
                    }
                }
                ValueCalc(ref copper, ref silver, ref gold, ref platinum, ref terra);
                if (platinum > 0)
                {
                    num = (int)((platinum % 999 == 0L) ? (platinum / 999) : (platinum / 999 + 1));
                }
                slots = ((gold > 0) ? 1 : 0) + ((silver > 0) ? 1 : 0) + ((copper > 0) ? 1 : 0) + num;
                return num;
            }
            return -1;
        }

        private static void ValueCalc(ref long copper, ref long silver, ref long gold, ref long platinum, ref long terra)
        {
            platinum = copper / 1000000;
            copper -= platinum * 1000000;
            gold = copper / 10000;
            copper -= gold * 10000;
            silver = copper / 100;
            copper -= silver * 100;
            terra = copper / 100000000;
            copper -= platinum * 100000000;
        }
    }
}