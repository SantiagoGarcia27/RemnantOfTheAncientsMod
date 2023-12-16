using CalamityMod.Items.PermanentBoosters;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Reaper;
using RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger;
using RemnantOfTheAncientsMod.Prefixe;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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
        public bool EternityItem; 

        public static float HealthyReforgeValue { get; set; } = 5f;
        public static float HealerReforgeValue { get; set; } = 10f;
        public static float AtleticReforgeValue { get; set; } = 15f;
        public static float GigantReforgeValue { get; set; } = 20f;
        public static float TitanicReforgeValue { get; set; } = 40f;
        public static int UnpenetrableReforgeValue { get; set; } = 8;

        private string GenericPath = "Mods.RemnantOfTheAncientsMod.Generic.";
        public override void ModifyTooltips(Item item, List<TooltipLine> Tooltips)
        {
            Color? color = new Color(0, 0, 0);
            TooltipLine val = Tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
            if (val != null) ApplyRarityColor(item, val);
            if (MusicBox)
            {
                color = new Color(Main.DiscoG, Main.DiscoG, Main.DiscoG);
                SetTooltipCustom(Tooltips,Language.GetTextValue(GenericPath + "MusicBoxCredit","Wandering"),color, MusicBox,$"{Mod.Name}:MusicBoxCredit",color.GetValueOrDefault(Utils1.LegendaryRarityColor));
            }
            if (SecondHabilitie)
            {
                color = Color.Red;
                SetTooltipCustom(Tooltips, Language.GetTextValue(GenericPath + "RightClickSecondHabilitie"), color, $"{Mod.Name}:RightClickSecondHabilitie", color.Value);  
            }

            if (Saber || (Lance && ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf))
            {
                color = Color.Red;
                SetTooltipCustom(Tooltips, Language.GetTextValue(GenericPath + "RightClickSaberHabilitie"), color, $"{Mod.Name}:RightClickSecondHabilitie", color.Value);  
            }

            if (LegendaryDrop)
            {
                color = new Color(255, Main.DiscoG, 53);
                SetTooltipCustom(Tooltips,"[" + Language.GetTextValue(GenericPath + "LegendaryTooltip") + "]", color, $"{Mod.Name}:LegendaryDrop", color.GetValueOrDefault(Utils1.LegendaryRarityColor));
            }
            if (ReaperItem)
            {
                color = Utils1.ReaperRarityColor;
                SetTooltipCustom(Tooltips, "[" + Language.GetTextValue(GenericPath + "ReaperTooltip") + "]", color, $"{Mod.Name}:ReaperTooltip", color.GetValueOrDefault(Utils1.ReaperRarityColor));
            }
            if (BossSummon)
            {
                color = Utils1.ReaperRarityColor;
                SetTooltipCustom(Tooltips,Language.GetTextValue(GenericPath + "NotConsumableTooltip"), color, $"{Mod.Name}:BossSummonTooltip", color.GetValueOrDefault(Utils1.ReaperRarityColor));
            }
            if (CompletistItem)
            {
                color = Utils1.CompletistRarityColor;
                SetTooltipCustom(Tooltips,"[" + Language.GetTextValue(GenericPath + "CompletistTooltip") + "]", color, $"{Mod.Name}:CompletistTooltip", color.GetValueOrDefault(Utils1.ReaperRarityColor));
            }
            if(ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod))
            {
                if (EternityItem)
                {
                    SetTooltipCustom(Tooltips, Language.GetTextValue("Mods.FargowiltasSouls.Message.EternityItem"), Color.White, EternityItem, "FargowiltasSouls:Eternity", Color.White);
                }
            }
            FixLocalizations(item, Tooltips);
            AplyReforgeTooltip(item, Tooltips);
        }

        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances => true;

        private void SetTooltipCustom(IList<TooltipLine> Tooltips, string tooltip, Color? ColorOverride, bool Condition, string tooltipLineName = "BossSumon", Color? color = null )
        {
            if (Condition) 
            {
                if (color == null) color = ColorOverride;
                string text = (color.HasValue ? tooltip : "");
                TooltipLine item2 = new TooltipLine(Mod, tooltipLineName, text)
                {
                    OverrideColor = color
                };      
                Tooltips.Add(item2);
            }
        } 
        private void SetTooltipCustom(IList<TooltipLine> Tooltips, string tooltip, Color? ColorOverride, string tooltipLineName = "BossSumon", Color? color = null)
        {
            if (color == null) color = ColorOverride;
            string text = (color.HasValue ? tooltip : "");
            TooltipLine item2 = new TooltipLine(Mod, tooltipLineName, text)
            {
                OverrideColor = color
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
            else if (item.type == ItemID.JungleRose)
            {
                var tip = Tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    string text1 = "\n" + Language.GetTextValue("CommonItemTooltip.PercentIncreasedCritChance", 5);
                    var text = tip.Text.Insert(tip.Text.Count(), text1);
                    tip.Text = text;
                }
            }
            else if (item.type == ItemID.CritterShampoo)
            {
                var tip = Tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    string text1 = "\n" + Language.GetTextValue("CommonItemTooltip.IncreasesMaxLifeBy", DificultyUtils.ReaperMode ? 10 : 30);
                    var text = tip.Text.Insert(tip.Text.Count(), text1);
                    tip.Text = text;
                }
            }
            else if (item.type == ItemID.LuckyHorseshoe)
            {
                var tip = Tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    string text1 = "\n" + Language.GetTextValue("Mods.RemnantOfTheAncientsMod.EditVanilla.IncreasedLuck", DificultyUtils.ReaperMode ? 0.4f : 0.2f);
                    var text = tip.Text.Insert(tip.Text.Count(), text1);
                    tip.Text = text;
                }
            }
            if (ModifyAccsesories.FastFall.Contains(item.type))
            {
                var tip = Tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    string text1 = "\n" + Language.GetTextValue("Mods.RemnantOfTheAncientsMod.EditVanilla.IncreasedFallSpeed");
                    var text = tip.Text.Insert(tip.Text.Count(), text1);
                    tip.Text = text;
                }
            } 
            else if (item.type == ItemID.FlurryBoots)
            {
                var tip = Tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    string text1 = Language.GetTextValue("Mods.RemnantOfTheAncientsMod.EditVanilla.IncreasedBootSpeedInBlock", new Item(ItemID.SnowBlock).Name);
                    var text = tip.Text.Insert(tip.Text.Count(), text1);
                    tip.Text = text;
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
                        if (tip.Text == "{0}")
                        {
                            var text = tip.Text.Replace("{0}","");
                            tip.Text = text;
                        }
                    }
                }
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
