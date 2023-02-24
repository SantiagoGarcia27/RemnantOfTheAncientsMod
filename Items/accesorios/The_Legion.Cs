using Terraria;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.VanillaChanges;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod;
using Terraria.DataStructures;

namespace RemnantOfTheAncientsMod.Items.accesorios
{

    public class The_Legion : ModItem
    {
        private static readonly Color ColorOne = new Color(255, 0, 0);
        private static readonly Color ColorTwo = new Color(0, 0, 255);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Legion");
            Tooltip.SetDefault("Increases the summon damage by 25% "
            + "\n+5 minions"
            + "\nMany thanks to:"
            + "\n S-Pladison   -NKSHV  -perrogalan"
            + "\n-Banmei   -Scratch Lunin  -DinoZers -scorpion17907");

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 8));
        }
        internal static Color GetRarityColor() => Utils1.ColorSwap(ColorOne, ColorTwo, 3f);
        public override void SetDefaults()
        {
           Item.width = 10;
           Item.height = 14;
            Item.value = Item.buyPrice(0, 18, 0, 0);
            Item.GetGlobalItem<GlobalItem1>().customRarity = CustomRarity.Legendary;
           Item.accessory = true;
           Item.GetGlobalItem<GlobalItem1>().LegendaryDrop = true;
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

