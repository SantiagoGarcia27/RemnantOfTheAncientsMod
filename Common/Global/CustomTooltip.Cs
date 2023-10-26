using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Reaper;
using RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger;
using RemnantOfTheAncientsMod.Prefixe;
using RemnantOfTheAncientsMod.World;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Common.Global
{
    public class CustomTooltip : GlobalItem
    {
        public CustomRarity customRarity;

        public bool LegendaryDrop;
        public bool ReaperItem;
        public bool DonatorItem;
        public bool CompletistItem;
        public bool BossSummon;
        public bool SizedByReaper;
        public bool ReaperAccesories;
        public bool MusicBox;
        public bool Saber;
        public bool Lance;
        public bool SecondHabilitie;
        private ItemDefinition TooltipItem;

        public static float HealthyReforgeValue { get; set; } = 5f;
        public static float HealerReforgeValue { get; set; } = 10f;
        public static float AtleticReforgeValue { get; set; } = 15f;
        public static float GigantReforgeValue { get; set; } = 20f;
        public static float TitanicReforgeValue { get; set; } = 40f;
        public static int UnpenetrableReforgeValue { get; set; } = 8;

        
        public override void ModifyTooltips(Item item, List<TooltipLine> Tooltips)
        {
            TooltipLine val = Tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
            if (val != null) ApplyRarityColor(item, val);
            if (MusicBox) MusicBoxTooltip(item, Tooltips);
            if (SecondHabilitie) SecondHbilitieTooltip(item, Tooltips);
            if (Saber) SaberHbilitieTooltip(item, Tooltips);
            if (Lance && ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf) SaberHbilitieTooltip(item, Tooltips);
            if (LegendaryDrop) LegendaryTooltip(item, Tooltips);
            if (ReaperItem) ReaperTooltip(item, Tooltips);
            if (BossSummon) BossSummonTooltip(item, Tooltips);
            if (CompletistItem) CompletistTooltip(item, Tooltips);
            FixLocalizations(item, Tooltips);
            AplyReforgeTooltip(item, Tooltips);
        }

        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances => true;

        private void BossSummonTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            Color? color = new Color(10, Main.DiscoG, 10);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.ReaperRarityColor);
            string text = (color.HasValue ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.NotConsumableTooltip") : "");
            TooltipLine item2 = new TooltipLine(Mod, "BossSumon", text)
            {
                OverrideColor = valueOrDefault
            };
            if (Reaper.ReaperMode) Tooltips.Add(item2);
        }
        private void CompletistTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            Color? color = Utils1.ColorSwap(new Color(247, 239, 72), new Color(250, 205, 7), 3f);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.CompletistRarityColor);
            string text = (color.HasValue ? "[" + Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.CompletistTooltip") + "]" : "");
            TooltipLine item2 = new TooltipLine(Mod, "CompletistItem", text)
            {
                OverrideColor = valueOrDefault
            };
            Tooltips.Add(item2);
        }

        private void SaberHbilitieTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            Color? color = Color.Red;
            Color valueOrDefault = color.Value;
            string text = (color.HasValue ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.RightClickSaberHabilitie") : "");
            TooltipLine item2 = new TooltipLine(Mod, "SaberSecondClick", text)
            {
                OverrideColor = valueOrDefault
            };
            Tooltips.Add(item2);
        }
        private void SecondHbilitieTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            Color? color = Color.Red;
            Color valueOrDefault = color.Value;
            string text = (color.HasValue ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.RightClickSecondHabilitie") : "");
            TooltipLine item2 = new TooltipLine(Mod, "SecondClickHabilitie", text)
            {
                OverrideColor = valueOrDefault
            };
            Tooltips.Add(item2);
        }

        public List<int> ReaperItems = new List<int>
        {
           ModContent.ItemType<Ftoggler>(),
           ModContent.ItemType<Reaper_Hood>(),
           ModContent.ItemType<Reaper_Robe>(),
           ModContent.ItemType<Reaper_Pants>()
        };

        private void ApplyRarityColor(Item item, TooltipLine nameLine)
        {
            Color? rarityColor = Utils1.GetRarityColor(customRarity);
            if (!item.expert && rarityColor.HasValue) nameLine.OverrideColor = rarityColor.Value;
            if (ReaperItems.Contains(item.type))
            {
                if (item.type == ReaperItems[0])
                {
                    nameLine.OverrideColor = Ftoggler.GetRarityColor();
                }
                else if (item.type == ReaperItems[1])
                {
                    nameLine.OverrideColor = Reaper_Hood.GetRarityColor();
                }
                else if (item.type == ReaperItems[2])
                {
                    nameLine.OverrideColor = Reaper_Robe.GetRarityColor();
                }
                else if (item.type == ReaperItems[3])
                {
                    nameLine.OverrideColor = Reaper_Pants.GetRarityColor();
                }
            }
        }

        private void LegendaryTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            Color? color = new Color(255, Main.DiscoG, 53);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.LegendaryRarityColor);
            string text = (color.HasValue ? "[" + Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.LegendaryTooltip") + "]" : "");
            TooltipLine item2 = new TooltipLine(Mod, "LegendaryDrop", text)
            {
                OverrideColor = valueOrDefault
            };
            Tooltips.Add(item2);
        }
        private void ReaperTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            Color? color = Utils1.ReaperRarityColor;
            Color valueOrDefault = color.GetValueOrDefault(Utils1.ReaperRarityColor);
            string text = (color.HasValue ? "[" + Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.ReaperTooltip") + "]" : "");
            TooltipLine item2 = new TooltipLine(Mod, "ReaperItem", text)
            {
                OverrideColor = valueOrDefault
            };
            Tooltips.Add(item2);
        }
        private void MusicBoxTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            int a = Main.DiscoG;
            Color? color = new Color(a, a, a);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.LegendaryRarityColor);
            string text = (color.HasValue ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.MusicBoxCredit", "Wandering") : "");
            TooltipLine item2 = new TooltipLine(Mod, "By Wandering", text)
            {
                OverrideColor = valueOrDefault
            };
            Tooltips.Add(item2);
        }
        private void AplyReforgeTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            if (item.prefix == ModContent.PrefixType<Healthy>())
            {
                RegorgeTooltip(item, Tooltips, (int)HealthyReforgeValue, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");
            }
            if (item.prefix == ModContent.PrefixType<Healer>())
            {
                RegorgeTooltip(item, Tooltips, (int)HealerReforgeValue, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");
            }
            if (item.prefix == ModContent.PrefixType<Atletic>())
            {
                RegorgeTooltip(item, Tooltips, (int)AtleticReforgeValue, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");
            }
            if (item.prefix == ModContent.PrefixType<Gigant>())
            {
                RegorgeTooltip(item, Tooltips, (int)GigantReforgeValue, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");
            }
            if (item.prefix == ModContent.PrefixType<Titanic>())
            {
                RegorgeTooltip(item, Tooltips, (int)TitanicReforgeValue, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");
            }
            if (item.prefix == ModContent.PrefixType<Impenetrable>())
            {
                RegorgeTooltip(item, Tooltips, UnpenetrableReforgeValue, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Defense");
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                    RegorgeTooltip(item, Tooltips, (int)2, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.DamageReduction");
            }
            if (item.prefix == ModContent.PrefixType<Supersonic>())
            {
                RegorgeTooltip(item, Tooltips, 8, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Speed");
            }
            if (item.prefix == ModContent.PrefixType<Acurate>())
            {
                RegorgeTooltip(item, Tooltips, 8, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.CritChance");
            }
            if (item.prefix == ModContent.PrefixType<Uncontrolled>())
            {
                RegorgeTooltip(item, Tooltips, 8, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.MeleeSpeed");
            }
            if (item.prefix == ModContent.PrefixType<Sharp>())
            {
                RegorgeTooltip(item, Tooltips, 8, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Damage");
            }
            if (item.prefix == ModContent.PrefixType<Regenerative>())
            {
                RegorgeTooltip(item, Tooltips, 2, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.LifeRegeneration");
            }
        }

        public void RegorgeTooltip(Item item, IList<TooltipLine> Tooltips, int quantity, Color color, string textString)
        {
            string text = Language.GetTextValue(textString, quantity);
            TooltipLine item2 = new TooltipLine(Mod, "Reforge", text)
            {
                OverrideColor = color
            };
            Tooltips.Add(item2);
        }
        public void FixLocalizations(Item item, IList<TooltipLine> Tooltips)
        {
            Item baseItem = item.GetGlobalItem<RemnantGlobalItem>().ItemBase;

            if (item.type == ItemID.SoulDrain || item.type == ItemID.PirateStaff)
            {
                var tip2 = Tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip2 != null)
                {
                    string text1 = "\n"+Language.GetTextValue("Mods.RemnantOfTheAncientsMod.EditVanilla.IncreasedMoneyDrop");
                    var text = tip2.Text.Insert(tip2.Text.Count(), text1);
                    tip2.Text = text;
                }
            }

            if (baseItem != null)
            {
                int baseType = GetBaseType(baseItem);
                var Name = Tooltips.FirstOrDefault(tip => tip.Name == "ItemName" && tip.Mod == "Terraria");
                if (Name != null)
                {
                    var text = Name.Text.Replace("{0}", ReturnLanguage(baseItem.Name,baseType));
                    Name.Text = text;

                }
                var tip = Tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    if (baseItem.ToolTip.Lines > 0)
                    {
                        var text = tip.Text.Replace("{0}", baseItem.ToolTip.GetLine(0));
                        tip.Text = text;
                    }
                    else
                    {
                        //int index = GetIndexForText(tip, "{0}");
                        if (tip.Text == "{0}")
                        {
                            var text = tip.Text.Replace("{0}","");
                            tip.Text = text;
                        }
                    }
                }
               

                //string text = baseItem.ToolTip.GetLine(0);// Language.GetTextValue($"Mods.RemnantOfTheAncientsMod.Items.{baseItem.Name}.Tooltip");
                //TooltipLine item2 = new TooltipLine(Mod, "TooltipLine", text)
                //{
                //    OverrideColor = Color.White
                //};
                //Tooltips.Insert(3,item2);
            } 
        }
        public int GetIndexForText(TooltipLine tip,string text) 
        {
            for (int i = 0; i < tip.Text.LongCount(); i++)
            {
                string texto = tip.Text.ToString();
                if (texto == text)
                {
                    return i;
                }
            }
            return -1;
        }
        public string ReturnLanguage(string baseName,int baseType)
        {

            switch (Language.ActiveCulture.LegacyId)
            {
                case 1:
                    if (baseType == 1) return $"Endless {baseName} Quiver";
                    else if(baseType == 2) return $"Endless {baseName} Pouch";
                    else if(baseType == 3) return $"Endless {baseName} Depot";
                    else if(baseType == 3) return $"Endless {baseName} Box";
                    else return "Endless " + baseName;
                case 4:
                    if (baseType == 1) return $"Carquois de {baseName} sans fin";
                    else if (baseType == 2) return $"Endless {baseName} Pouch";
                    else if (baseType == 3) return $"Endless {baseName} Depot";
                    else if (baseType == 3) return $"Endless {baseName} Box";
                    else return baseName + " sans fin";
                case 5:
                    if (baseType == 1) return $"Carcaj de {baseName} eterno";
                    else if (baseType == 2) return $"Petaca de {baseName} eterna";
                    else if (baseType == 3) return $" Depósito de {baseName} eterno";
                    else if (baseType == 4) return $"Caja {baseName} Eterna";
                    else if (baseType == 5) return baseName + " Eterna";
                    else return baseName+" Eterno";
                default:
                    return "Endless " + baseName;
            }
        }
        public int GetBaseType(Item baseItem)
        {
            if (baseItem.ammo == AmmoID.Arrow) return 1;
            else if (baseItem.ammo == AmmoID.Bullet) return 2;
            else if (baseItem.ammo == AmmoID.Rocket) return 3;
            else if (baseItem.ammo == AmmoID.Dart) return 4;
            else if (baseItem.ammo == AmmoID.FallenStar) return 5;
            else return 0;
        }
    }
}
