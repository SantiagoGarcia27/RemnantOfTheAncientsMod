using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;
using Terraria.ID;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{

    public class Fake_The_Legion : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(25, 5);
        private static readonly Color ColorOne = new Color(255, 0, 0);
        private static readonly Color ColorTwo = new Color(0, 0, 255);
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("The Legion");
            //Tooltip.SetDefault("Increases the summon damage by 25% "
            //+ "\n+5 minions"
            //+ "\nMany thanks to:"
            //+ "\n S-Pladison   -NKSHV  -perrogalan"
            //+ "\n-Banmei   -Scratch Lunin  -DinoZers -scorpion17907 \n\n\n\n\n\n\n\n\n\n\n\n\n\nEs de carton");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 8));
        }
        internal static Color GetRarityColor() => Utils1.ColorSwap(ColorOne, ColorTwo, 3f);
        public override void SetDefaults()
        {
           Item.width = 10;
           Item.height = 14;
           Item.value = Item.buyPrice(0, 0, 0, 0);
           Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Legendary;
           Item.accessory = true;
           Item.GetGlobalItem<CustomTooltip>().LegendaryDrop = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.name == "S-Pladison" || player.name == "NKSHV" || player.name == "perrogalan" || player.name == "Banmei" || player.name == "Scratch Lunin" || player.name == "DinoZers" || player.name == "scorpion17907")
            {
                player.GetDamage(DamageClass.Summon) *= 0f;
                player.maxMinions = 0;
                //player.moveSpeed *= 99999999f;
            }
            else
            {
                player.GetDamage(DamageClass.Summon) *= 0f;
                player.maxMinions = 0;
               // player.moveSpeed *= 99999999f;
            }
        }
        public override void AddRecipes()  //How to craft this item
        {
            CreateRecipe()
            .AddIngredient(ItemID.Sign, 1)
            .AddIngredient(ItemID.DirtWall, 1)
            .AddTile(TileID.WorkBenches)  //at work bench
            .Register();
        }
    }
}

