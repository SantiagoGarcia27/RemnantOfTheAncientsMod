using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

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
            if (item.type == ItemID.HandOfCreation)
            {
                player.pickSpeed -= 0.45f;
            }
            base.UpdateAccessory(item, player, hideVisual);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.HandOfCreation)
            {
                var tip = tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    var text = tip.Text.Replace("25", "45");
                    tip.Text = text;

                }
            }
            base.ModifyTooltips(item, tooltips);
        }
    }
}