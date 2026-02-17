using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Terraria.Localization;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{

    public class The_Legion : ModItem
    {
        private static readonly Color ColorOne = new Color(255, 0, 0);
        private static readonly Color ColorTwo = new Color(0, 0, 255);
        private static readonly List<string> ForumComments = new List<string>()
        {
            "S-Pladison",
            "NKSHV",
            "perrogalan",
            "Banmei",
            "Scratch Lunin",
            "DinoZers",
            "scorpion17907"
        };
        int damageBonus = getDamageBonus();
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(damageBonus, 5);
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 6));
        }
        static int getDamageBonus()
        {
            return ForumComments.Contains(Main.LocalPlayer.name) ? 25 : 200;
        }

        internal static Color GetRarityColor() => Utils1.ColorSwap(ColorOne, ColorTwo, 3f);
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 36;
            Item.value = Item.buyPrice(0, 18, 0, 0);
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Legendary;
            Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().LegendaryDrop = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (ForumComments.Contains(player.name))
            {
                player.GetDamage(DamageClass.Summon) += 2f;
                player.maxMinions += 8;
            }
            else
            {
                player.GetDamage(DamageClass.Summon) += .25f;
                player.maxMinions += 5;
            }
        }
    }
}

