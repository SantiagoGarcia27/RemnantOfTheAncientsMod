using Microsoft.Xna.Framework;
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

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine val = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
            if (val != null) ApplyRarityColor(item, val); 
            if (MusicBox) MusicBoxToolTip(item, tooltips);
            if (SecondHabilitie) SecondHbilitieToolTip(item, tooltips);
            if (Saber) SaberHbilitieToolTip(item,tooltips);
            if (Lance && ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf) SaberHbilitieToolTip(item, tooltips);
            if (LegendaryDrop) LegendaryToolTip(item, tooltips);
            if (ReaperItem) ReaperToolTip(item, tooltips);
            if (BossSummon) BossSummonToolTip(item, tooltips);
            AplyReforgeTooltip(item, tooltips);
            //   if (ReaperAccesories) ReaperArtifactToolTip(item, tooltips);

        }

        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances => true;

        private void BossSummonToolTip(Item item, IList<TooltipLine> tooltips)
        {
            Color? color = new Color(10, Main.DiscoG, 10);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.ReaperRarityColor);
            string text = (color.HasValue ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.NotConsumableTooltip") : "");
            TooltipLine item2 = new TooltipLine(Mod, "BossSumon", text)
            {
                OverrideColor = valueOrDefault
            };
            if (Reaper.ReaperMode) tooltips.Add(item2);
        }
        private void SaberHbilitieToolTip(Item item, IList<TooltipLine> tooltips)
        {
            Color? color = Color.Red;
            Color valueOrDefault = color.Value;
            string text = (color.HasValue ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.RightClickSaberHabilitie") : "");
            TooltipLine item2 = new TooltipLine(Mod, "Saber Habilitie", text)
            {
                OverrideColor = valueOrDefault
            };
            tooltips.Add(item2);
        }
        private void SecondHbilitieToolTip(Item item, IList<TooltipLine> tooltips)
        {
            Color? color = Color.Red;
            Color valueOrDefault = color.Value;
            string text = (color.HasValue ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.RightClickSecondHabilitie") : "");
            TooltipLine item2 = new TooltipLine(Mod, "Second Habilitie", text)
            {
                OverrideColor = valueOrDefault
            };
            tooltips.Add(item2);
        }
        /*     private void ReaperArtifactToolTip(Item item, IList<TooltipLine> tooltips)
             {
                 Player p = new Player();
                 Color? color = new Color(10, Main.DiscoG, 10);
                 Color valueOrDefault = color.GetValueOrDefault(Utils1.ReaperRarityColor);
                 string text;

                 if (RemanntWorld.ReaperMode)
                 {
                     if (p.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade)
                     {

                         text = p.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade ? "30% increased movement speed" : "";
                     }

                     else text = "Error";
                 }
                 else text = "Error";
                 TooltipLine item2 = new TooltipLine(Mod, "BossSumon", text)
                 {
                     OverrideColor = valueOrDefault
                 };
                 if (RemanntWorld.ReaperMode) tooltips.Add(item2);
             }*/
        private void ApplyRarityColor(Item item, TooltipLine nameLine)
        {
            Color? rarityColor = Utils1.GetRarityColor(customRarity);
            if (!item.expert && rarityColor.HasValue) nameLine.OverrideColor = rarityColor.Value;
            if (item.type == ModContent.ItemType<Ftoggler>()) nameLine.OverrideColor = Ftoggler.GetRarityColor();
        }

        private void LegendaryToolTip(Item item, IList<TooltipLine> tooltips)
        {
            Color? color = new Color(255, Main.DiscoG, 53);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.LegendaryRarityColor);
            string text = (color.HasValue ? "[" + Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.LegendaryTooltip")+"]"  : "");
            TooltipLine item2 = new TooltipLine(Mod, "LegendaryDrop", text)
            {
                OverrideColor = valueOrDefault
            };
            tooltips.Add(item2);
        }
        private void ReaperToolTip(Item item, IList<TooltipLine> tooltips)
        {
            Color? color = new Color(10, Main.DiscoG, 10);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.ReaperRarityColor);
            string text = (color.HasValue ? "["+Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.ReaperTooltip")+"]" : "");
            TooltipLine item2 = new TooltipLine(Mod, "ReaperItem", text)
            {
                OverrideColor = valueOrDefault
            };
            tooltips.Add(item2);
        }
        private void MusicBoxToolTip(Item item, IList<TooltipLine> tooltips)
        {
            int a = Main.DiscoG;
            Color? color = new Color(a, a, a);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.LegendaryRarityColor);
            string text = (color.HasValue ? Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Generic.MusicBoxCredit", "Wandering") : "");
            TooltipLine item2 = new TooltipLine(Mod, "By Wandering", text)
            {
                OverrideColor = valueOrDefault
            };
            tooltips.Add(item2);
        }
        private void AplyReforgeTooltip(Item item, IList<TooltipLine> tooltips)
        {
            if (item.prefix == ModContent.PrefixType<Healthy>())
            {
                RegorgeToolTip(item, tooltips, (int)HealthyReforgeValue, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");
            }
            if (item.prefix == ModContent.PrefixType<Healer>())
            {
                RegorgeToolTip(item, tooltips, (int)HealerReforgeValue, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");
            }
            if (item.prefix == ModContent.PrefixType<Atletic>())
            {
                RegorgeToolTip(item, tooltips, (int)AtleticReforgeValue, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");
            }
            if (item.prefix == ModContent.PrefixType<Gigant>())
            {
                RegorgeToolTip(item, tooltips, (int)GigantReforgeValue, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");
            }
            if (item.prefix == ModContent.PrefixType<Titanic>())
            {
                RegorgeToolTip(item, tooltips, (int)TitanicReforgeValue, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Life");    
            }
            if (item.prefix == ModContent.PrefixType<Impenetrable>())
            {
                RegorgeToolTip(item, tooltips, UnpenetrableReforgeValue, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Defense");
                if (RemnantOfTheAncientsMod.CalamityMod != null)
                    RegorgeToolTip(item, tooltips, (int)2, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.DamageReduction");
            }
            if (item.prefix == ModContent.PrefixType<Supersonic>())
            {
                RegorgeToolTip(item, tooltips, 8, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Speed");
            }
            if (item.prefix == ModContent.PrefixType<Acurate>())
            {
                RegorgeToolTip(item, tooltips, 8, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.CritChance");
            }
            if (item.prefix == ModContent.PrefixType<Uncontrolled>())
            {
                RegorgeToolTip(item, tooltips, 8, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.MeleeSpeed");
            }
            if (item.prefix == ModContent.PrefixType<Sharp>())
            {
                RegorgeToolTip(item, tooltips, 8, new Color(92, Main.DiscoColor.G, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.Damage");
            }
            if (item.prefix == ModContent.PrefixType<Regenerative>())
            {
                RegorgeToolTip(item, tooltips, 2, new Color(92, 145, 94, 255), "Mods.RemnantOfTheAncientsMod.Prefix.Description.LifeRegeneration");
            }
        }
      
        public void RegorgeToolTip(Item item, IList<TooltipLine> tooltips, int quantity, Color color, string textString)
        {
            string text = Language.GetTextValue(textString, quantity);
            TooltipLine item2 = new TooltipLine(Mod, "Reforge", text)
            {
                OverrideColor = color
            };
            tooltips.Add(item2);
        }
    }
}
