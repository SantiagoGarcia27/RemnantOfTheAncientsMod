using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{

    public class The_Legion : ModItem
    {
        private static readonly Color ColorOne = new Color(255, 0, 0);
        private static readonly Color ColorTwo = new Color(0, 0, 255);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Legion");
            Tooltip.SetDefault( tooltip()
            + "\nMany thanks to:"
            + "\n S-Pladison   -NKSHV  -perrogalan"
            + "\n-Banmei   -Scratch Lunin  -DinoZers -scorpion17907");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 8));
        }
        public string tooltip()
        {
            return Utils1.IncreasedDamageByTooltip(25, DamageClass.Summon)
                + "\n" + Utils1.IncreasedMinionTooltip(5);

        }
        internal static Color GetRarityColor() => Utils1.ColorSwap(ColorOne, ColorTwo, 3f);
        public override void SetDefaults()
        {
           Item.width = 10;
           Item.height = 14;
            Item.value = Item.buyPrice(0, 18, 0, 0);
             Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Legendary;
           Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().LegendaryDrop = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.name == "S-Pladison" || player.name == "NKSHV" || player.name == "perrogalan" || player.name == "Banmei" || player.name == "Scratch Lunin" || player.name == "DinoZers" || player.name == "scorpion17907")
            {
                player.GetDamage(DamageClass.Summon) *= 2f;
                player.maxMinions += 8;
            }
            else
            {
                player.GetDamage(DamageClass.Summon) *= 1.25f;
                player.maxMinions += 5;
            }
        }
    }
}

