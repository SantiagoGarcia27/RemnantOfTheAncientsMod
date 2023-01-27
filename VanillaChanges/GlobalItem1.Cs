using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Items.DificultChanger;
using RemnantOfTheAncientsMod.World;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.VanillaChanges
{
    public class GlobalItem1 : GlobalItem
    {
        public CustomRarity customRarity;

        public bool LegendaryDrop;
        public bool ReaperItem;
        public bool BossSummon;
        public bool SizedByReaper;
        public bool ReaperAccesories;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine val = tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria");
            if (val != null) ApplyRarityColor(item, val);
            if (LegendaryDrop) LegendaryToolTip(item, tooltips);
            if (ReaperItem) ReaperToolTip(item, tooltips); 
            if(BossSummon) BossSummonToolTip(item, tooltips);
         //   if (ReaperAccesories) ReaperArtifactToolTip(item, tooltips);

        }

        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances => true;

        private void BossSummonToolTip(Item item, IList<TooltipLine> tooltips)
        {
            Color? color = new Color(10, Main.DiscoG, 10);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.ReaperRarityColor);
            string text = (color.HasValue ? "Not Consumable" : "");
            TooltipLine item2 = new TooltipLine(Mod, "BossSumon", text)
            {
                OverrideColor = valueOrDefault
            };
            if (Reaper.ReaperMode) tooltips.Add(item2);
        }
   /*     private void ReaperArtifactToolTip(Item item, IList<TooltipLine> tooltips)
        {
            Player p = new Player();
            Color? color = new Color(10, Main.DiscoG, 10);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.ReaperRarityColor);
            string text;

            if (world1.ReaperMode)
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
            if (world1.ReaperMode) tooltips.Add(item2);
        }*/
        private void ApplyRarityColor(Item item, TooltipLine nameLine)
        {
            Color? rarityColor = Utils1.GetRarityColor(customRarity);
            if (!item.expert && rarityColor.HasValue)  nameLine.OverrideColor = rarityColor.Value;
            if (item.type == ModContent.ItemType<Ftoggler>()) nameLine.OverrideColor = Ftoggler.GetRarityColor();  
        }
        
        private void LegendaryToolTip(Item item, IList<TooltipLine> tooltips)
        {
            Color? color = new Color(255, Main.DiscoG, 53);
            Color valueOrDefault = color.GetValueOrDefault(Utils1.LegendaryRarityColor);
            string text = (color.HasValue ? "[Legendary Drop]" : "");
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
            string text = (color.HasValue ? "[Reaper Item]" : "");
            TooltipLine item2 = new TooltipLine(Mod, "ReaperItem", text)
            {
                OverrideColor = valueOrDefault
            };
            tooltips.Add(item2);
        } 
    }
}
