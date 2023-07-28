using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Armor.Reaper;
using RemnantOfTheAncientsMod.Content.Items.Armor.Reinforced_Iron;
using RemnantOfTheAncientsMod.Content.Items.Consumables.DificultChanger;
using RemnantOfTheAncientsMod.Prefixe;
using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global
{
    public class CustomTooltip : GlobalItem
    {
        public CustomRarity customRarity;

        public bool LegendaryDrop;
        public bool ReaperItem;
        public bool BossSummon;
        public bool SizedByReaper;
        public bool ReaperAccesories;
        public bool MusicBox;
        public bool Saber;
        public bool Lance;
        public bool SecondHabilitie;

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
            if (Saber) SaberHbilitieTooltip(item,Tooltips);
            if (Lance && ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf) SaberHbilitieTooltip(item, Tooltips);
            if (LegendaryDrop) LegendaryTooltip(item, Tooltips);
            if (ReaperItem) ReaperTooltip(item, Tooltips);
            if (BossSummon) BossSummonTooltip(item, Tooltips);
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
        private void SaberHbilitieTooltip(Item item, IList<TooltipLine> Tooltips)
        {
            Color? color = Color.Red;
            Color valueOrDefault = color.Value;
            string text = (color.HasValue ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.RightClickSaberHabilitie") : "");
            TooltipLine item2 = new TooltipLine(Mod, "Saber Habilitie", text)
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
            TooltipLine item2 = new TooltipLine(Mod, "Second Habilitie", text)
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
            string text = (color.HasValue ? "[" + Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.LegendaryTooltip")+"]"  : "");
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
            string text = (color.HasValue ? "["+Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.ReaperTooltip")+"]" : "");
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
    }
}
