using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
    public class ModifyAccsesories : GlobalItem
    {
        static Mod Calamity = RemnantOfTheAncientsMod.CalamityMod;
        public override void SetDefaults(Item item)
        {
            bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;

            if (WeaponConf)
            {
                if (item.type == ItemID.BandofRegeneration)
                {
                    item.lifeRegen += 2;
                }
                if (item.type == ItemID.PlatinumWatch)
                {
                    item.defense = 1;
                }
                if (item.type == ItemID.GoldWatch)
                {
                    item.defense = 1;
                }
                if (item.type == ItemID.PortableStool)
                {
                    item.defense = 2;
                }
                if (item.type == ItemID.HandOfCreation)
                {
                    item.defense += 2;
                }

              
            }
        }

        public static List<int> FastFall = new List<int>()
        {
            //ItemID.HermesBoots,
            //ItemID.FlurryBoots,
            //ItemID.SpectreBoots,
            //ItemID.FairyBoots,
            //ItemID.HellfireTreads,
            //ItemID.LightningBoots,
            //ItemID.FrostsparkBoots,
            //ItemID.TerrasparkBoots
        };
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
            if (item.type == ItemID.HandOfCreation)
            {
                player.pickSpeed -= 0.45f;
            }

            if (WeaponConf)
            {
                if (item.type == ItemID.JungleRose)
                {
                    player.GetCritChance(DamageClass.Generic) += 5;
                }
                if (item.type == ItemID.CritterShampoo)
                {
                    player.statLifeMax2 += DificultyUtils.ReaperMode ? 10 : 30;
                }
                if (item.type == ItemID.LuckyHorseshoe)
                {
                    player.luck += DificultyUtils.ReaperMode ? 0.2f : 0.4f;
                    player.luckMaximumCap = 2;
                }
                if (item.type == ItemID.FlurryBoots)
                {
                    if (DistanceUtils.GetPlayerFlour(player) == TileID.SnowBlock)
                    {
                        player.moveSpeed += 2f;
                    }
                }
               
                if (FastFall.Contains(item.type))
                {
                   // string ResultItemName = new Item(item.type).Name;
                    player.maxFallSpeed *= 2f;
                }
            }
            base.UpdateAccessory(item, player, hideVisual);
        }


      

        public static void UpdateFallSpeedList()
        {
            RecipeUtils.GetUpgradeTree(ModContent.ItemType<Boot>(),FastFall);
        }

        public static void ModifyCalamity()
        {
            Utils1.AddSecure(FastFall, ExternalModCallUtils.GetItemFromMod(Calamity, "AngelTreads"));
            Utils1.AddSecure(FastFall, ExternalModCallUtils.GetItemFromMod(Calamity, "TracersCelestial"));
            Utils1.AddSecure(FastFall, ExternalModCallUtils.GetItemFromMod(Calamity, "TracersElysian"));
            Utils1.AddSecure(FastFall, ExternalModCallUtils.GetItemFromMod(Calamity, "TracersSeraph"));
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            FixDuplicatedPrefix(tooltips);

            if (item.type == ItemID.HandOfCreation)
            {
                var tip = tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    var text = tip.Text.Replace("25", "45");
                    tip.Text = text;

                }
            }
           

            //base.ModifyTooltips(item, tooltips);
        }
        string tooltip = "";
        public void FixDuplicatedPrefix(List<TooltipLine> tooltips)
        {
            var tip = tooltips.FirstOrDefault(tip => tip.Mod != "a");
            int[] index = new int[2] { TextUtils.GetTooltipDuplicatedIndex(tooltips) ,0};
            


            if (index[0] != -1)
            {
                tooltips.RemoveAt(index[0]);
            }
            else
            {
                for (int a = 0; a < tooltips.Count(); a++)
                {
                    string tooltipt = tooltips[a].Text;
                    if (tooltips[a].Text.Contains("\n"))
                    {
                        int l = TextUtils.SeparateSringOnList(tooltipt).Count;
                        for (int i = 0; i < l - 1; i++)
                        {
                            if (i > 0)
                            {
                                if (TextUtils.SeparateSringOnList(tooltipt)[i] != TextUtils.SeparateSringOnList(tooltipt)[i - 1])
                                {
                                    if (TextUtils.SeparateSringOnList(tooltipt)[i] != TextUtils.SeparateSringOnList(tooltipt)[l - 1])
                                    {
                                        tooltip += TextUtils.SeparateSringOnList(tooltipt)[i] + "\n";
                                    }
                                }           
                            }
                            else
                            {
                                tooltip += TextUtils.SeparateSringOnList(tooltipt)[i] + "\n";
                            }
                        }
                        if (l - 2 >= 0)
                        {
                            tooltip += TextUtils.SeparateSringOnList(tooltipt)[l - 1];
                        }
                        else
                        {
                            tooltip += TextUtils.SeparateSringOnList(tooltips[l - 1].Text);
                        }

                        index[1] = TextUtils.GetTooltipIndexWithaWord(tooltips, tooltip);
                        if (index[1] != -1)
                        {
                            tooltips[index[1]].Text = tooltip;
                        }
                        tooltip = "";
                    }
                }
            }  
        }
        public override bool InstancePerEntity => true;
    }
}