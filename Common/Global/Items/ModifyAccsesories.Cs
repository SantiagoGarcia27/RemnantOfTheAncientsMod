using RemnantOfTheAncientsMod.Common.UtilsTweaks;
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
                    player.statLifeMax2 += Reaper.ReaperMode ? 10 : 30;
                }
                if (item.type == ItemID.LuckyHorseshoe)
                {
                    player.luck += Reaper.ReaperMode ? 0.2f : 0.4f;
                    player.luckMaximumCap = 2;
                }
                if (item.type == ItemID.FlurryBoots)
                {
                    if (DistanceUtils.GetPlayerFlour(player) == TileID.SnowBlock)
                    {
                        player.moveSpeed += 2f;
                    }
                }
                if (item.type == ItemID.HermesBoots)
                {
                    player.maxFallSpeed *= 2f;
                }
                if (item.type == ItemID.SpectreBoots)
                {
                    player.maxFallSpeed *= 2f;
                }
                if (item.type == ItemID.LightningBoots)
                {
                    player.maxFallSpeed *= 2f;
                }
                if (item.type == ItemID.FrostsparkBoots)
                {
                    player.maxFallSpeed *= 2f;
                }
                if (item.type == ItemID.TerrasparkBoots)
                {
                    player.maxFallSpeed *= 2f;
                }
            }
            base.UpdateAccessory(item, player, hideVisual);
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