using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.Global.Items.WeaponsModels;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Reaper;
using RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Prefixe;
using RemnantOfTheAncientsMod.World;
using System;
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
        public bool MusicBox;
        public bool Lance;
        public bool SecondHabilitie;
        public bool HaveSecondMode;
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
            Dictionary<string, string> ReplacedWords = new()
            {
            };
            if(RemnantOfTheAncientsMod.CalamityMod != null)
            {
                ReplacedWords.TryAdd("throwing", "rogue");
                ReplacedWords.TryAdd("lanzador", "rogue");
            }


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
            if (HaveSecondMode)
            {
                color = Color.Red;           
                SetTooltipCustom(Tooltips, Language.GetTextValue(GenericPath + "RightClickSecondMode"), color, $"{Mod.Name}:RightClickSecondMode", color.Value);
            }

            if (item.GetGlobalItem< SaberGlobalItem>().isSaber || (Lance && ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf))
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
            if(item.GetGlobalItem<RemnantGlobalItem>().StyleStat != 0)
            {
                SetTooltipCustom(Tooltips, Language.GetTextValue(GenericPath + "StyleStat", item.GetGlobalItem<RemnantGlobalItem>().StyleStat), Color.Pink, $"{Mod.Name}:StyleStat", Color.Pink);
            }
            if(ModLoader.TryGetMod("FargowiltasSouls", out Mod FargosSoulMod))
            {
                if (EternityItem)
                {
                    SetTooltipCustom(Tooltips, Language.GetTextValue("Mods.FargowiltasSouls.Items.Extra.EternityItem"), Color.White, EternityItem, "FargowiltasSouls:Eternity", Color.White);
                }
            }
            if (ModLoader.TryGetMod("CalamityMod", out Mod CalamityMod))
            {
                ModifyTooltipsCalamityMod(item, Tooltips);
            }

            ModifyWord(Tooltips, ReplacedWords);
            FixLocalizations(item, Tooltips);
            AplyReforgeTooltip(item, Tooltips);
        }

        
        public void ModifyTooltipsCalamityMod(Item item, List<TooltipLine> Tooltips)
        {
            if (item.type == SangarUtilities.Common.CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EtherealTalisman"))
            {
                SetTooltipCustom(Tooltips, Language.GetTextValue(GenericPath + "InmuneTo", Lang.GetBuffName(BuffID.ManaSickness)), Color.White, $"ToolTip4", Color.White);
            }
        }
        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances => true;


        private void ModifyWord(IList<TooltipLine> Tooltips, Dictionary<string, string> ChangedWords)
        {
            foreach (TooltipLine line in Tooltips)
            {
                foreach (string word in ChangedWords.Keys)
                {
                    if (line.Text.Contains(word))
                    {
                        string newText = ChangedWords[word];
                        string newTooltip = line.Text.Replace(word, newText);
                        line.Text = newTooltip;
                    }
                }
            }
        }


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
            TooltipLine item2 = new(Mod, tooltipLineName, text)
            {
                OverrideColor = color
            };
            Tooltips.Add(item2);
        }

        public List<int> ReaperItems =
        [
           ModContent.ItemType<Ftoggler>(),
           ModContent.ItemType<Reaper_Hood>(),
           ModContent.ItemType<Reaper_Robe>(),
           ModContent.ItemType<Reaper_Pants>()
        ];

        private void ApplyRarityColor(Item item, TooltipLine nameLine)
        {
            Color? rarityColor = Utils1.GetRarityColor(customRarity);
            if (!item.expert && rarityColor.HasValue)
                nameLine.OverrideColor = rarityColor.Value;
            if (ReaperItems.Contains(item.type))
                nameLine.OverrideColor = Ftoggler.GetRarityColor();
        }
  

        private void AplyReforgeTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            int[] healthPrefix = [ModContent.PrefixType<Healthy>(), ModContent.PrefixType<Healer>(), ModContent.PrefixType<Atletic>(), ModContent.PrefixType<Gigant>(), ModContent.PrefixType<Titanic>()];
            string commonPath = "Mods.RemnantOfTheAncientsMod.Prefix.Description.";

            Color color = new(92, Main.DiscoColor.G, 94, 255);
            if (healthPrefix.Contains(item.prefix))
            {
                color = item.prefix == ModContent.PrefixType<Titanic>() ? new Color(92, Main.DiscoColor.G, 94, 255) : new Color(92, 145, 94, 255);
                RegorgeTooltip(Tooltips, (int)HealthyReforgeValue, color, commonPath + "Life");
            }
            else if (item.prefix == ModContent.PrefixType<Regenerative>())
            {
                color = new Color(92, 145, 94, 255);
                RegorgeTooltip(Tooltips, 2,color, commonPath + "LifeRegeneration");
            } 
            else if (item.prefix == ModContent.PrefixType<Impenetrable>())
            {
                RegorgeTooltip(Tooltips, UnpenetrableReforgeValue, new Color(92, Main.DiscoColor.G, 94, 255), commonPath + "Defense");
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                    RegorgeTooltip(Tooltips, 2, color, commonPath + "DamageReduction");
            }
            else if (item.prefix == ModContent.PrefixType<Supersonic>())
            {
                RegorgeTooltip(Tooltips, 8, color, commonPath + "Speed");
            }
            else if (item.prefix == ModContent.PrefixType<Acurate>())
            {
                RegorgeTooltip(Tooltips, 8, color, commonPath + "CritChance");
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                    RegorgeTooltip(Tooltips, 0.1f, color, commonPath + "Luck");
            }
            else if (item.prefix == ModContent.PrefixType<Uncontrolled>())
            {
                RegorgeTooltip(Tooltips, 8, color, commonPath + "MeleeSpeed");
            }
            else if (item.prefix == ModContent.PrefixType<Sharp>())
            {
                RegorgeTooltip(Tooltips, 8, color, commonPath + "Damage");
            }
            
        }

        public void RegorgeTooltip(IList<TooltipLine> Tooltips, float quantity, Color color, string textString)
        {
            string text = Language.GetTextValue(textString, quantity);
            TooltipLine item2 = new(Mod, "Reforge", text)
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
                AddLine(Tooltips, "Mods.RemnantOfTheAncientsMod.EditVanilla.IncreasedMoneyDrop",0);
            }
            else if (item.type == ItemID.JungleRose)
            {
                int CritBonus = 5;
                AddLineLn(Tooltips, "CommonItemTooltip.PercentIncreasedCritChance", CritBonus);   
            }
            else if (item.type == ItemID.CritterShampoo)
            {
                int LifeBonus = Reaper.ReaperMode ? 10 : 30;
                AddLineLn(Tooltips, "CommonItemTooltip.IncreasesMaxLifeBy", LifeBonus);
            }
            else if (item.type == ItemID.LuckyHorseshoe)
            {
                float LuckBonus = Reaper.ReaperMode ? 0.4f : 0.2f;
                AddLineLn(Tooltips, "Mods.RemnantOfTheAncientsMod.EditVanilla.IncreasedLuck", LuckBonus); 
            }
            else if (item.type == ItemID.NanoBullet)
            {
                AddLineLn(Tooltips, "Mods.RemnantOfTheAncientsMod.EditVanilla.NanoBulletDrone");    
            }
            if (ModifyAccsesories.FastFall.Contains(item.type))
            {
                AddLineLn(Tooltips, "Mods.RemnantOfTheAncientsMod.EditVanilla.IncreasedFallSpeed");         
            } 
            else if (item.type == ItemID.FlurryBoots)
            {
                AddLineLn(Tooltips, "Mods.RemnantOfTheAncientsMod.EditVanilla.IncreasedBootSpeedInBlock", new Item(ItemID.SnowBlock).Name);
            }
            else if (item.type == ModContent.ItemType<Terracoin>())
            {
                RemoveAllStatDescription(Tooltips);
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
        public static void AddLine(IList<TooltipLine> Tooltips, string Text, object Extra = null, string TipName = "Tooltip0", string TipMod = "Terraria")
        {
            var tip = Tooltips.FirstOrDefault(tip => tip.Name == TipName && tip.Mod == TipMod);
            if (tip != null)
            {
                string text1 = Extra != null ? Language.GetTextValue(Text,Extra): Language.GetTextValue(Text);
                var text = tip.Text.Insert(tip.Text.Length, text1);
                tip.Text = text;
            }
        }
        public static void AddLineLn(IList<TooltipLine> Tooltips, string Text, object Extra = null, string TipName = "Tooltip0", string TipMod = "Terraria")
        {
            var tip = Tooltips.FirstOrDefault(tip => tip.Name == TipName && tip.Mod == TipMod);
            if (tip != null)
            {
                string text1 = "\n" + (Extra != null ? Language.GetTextValue(Text, Extra) : Language.GetTextValue(Text));
                var text = tip.Text.Insert(tip.Text.Length, text1);
                tip.Text = text;
            }
        }
      
        public static void RemoveAllStatDescription(IList<TooltipLine> Tooltips)
        {
            string[] atributes =
            [
                   "Damage",
                    "CritChance",
                    "Speed",
                    "Knockback",
                    "Placeable"
            ];
            foreach (string atribute in atributes)
            {
                var tip = Tooltips.FirstOrDefault(tip => (tip.Name == atribute) && tip.Mod == "Terraria");
                if (tip != null)
                {
                    tip.Text = "";
                }
            }

        }
        public static string ReturnLanguage(string baseName,int baseType)
        {
            switch (Language.ActiveCulture.LegacyId)
            {
                case 1:
                    if (baseType == 1) return $"Endless {baseName} Quiver";
                    else if(baseType == 2) return $"Endless {baseName} Pouch";
                    else if(baseType == 3) return $"Endless {baseName} Depot";
                    else if(baseType == 3) return $"Endless {baseName} Box";
                    else return "Endless " + baseName;
                case 5:
                    if (baseType == 1) return $"Carcaj de {baseName} eterno";
                    else if (baseType == 2) return $"Petaca de {baseName} eterna";
                    else if (baseType == 3) return $" Depósito de {baseName} eterno";
                    else if (baseType == 4) return $"Caja {baseName} Eterna";
                    else if (baseType == 5) return baseName + " Eterna";
                    else return baseName/*+" Eterno"*/;
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
