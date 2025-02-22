using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    internal class ReaperChalice : ModItem
    {
        private static readonly Color rarityColorOne = GetReaperColor(1);

        private static readonly Color rarityColorTwo = GetReaperColor(2);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ReaperPlayer>().ChaliceOn = true;
            player.GetModPlayer<ReaperEffectsPlayer>().ReaperSoulsBoost(Item);
            player.GetModPlayer<ReaperEffectsPlayer>().ReaperSoulsBoost();
        } 
        public override void SetDefaults()
        {
            Item.height = 34;
            Item.width = 30;
            Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
            Item.GetGlobalItem<CustomTooltip>().ReaperAccesories = true;
        }
        public override void AddRecipes() => CreateRecipe().AddTile(TileID.DemonAltar).Register();
        internal static Color GetRarityColor() => Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        public static Color GetReaperColor(int x)
        {
            Color color = new Color(100, 100, 100);
            if (x == 1) color = new Color(46, 45, 45);
            else if (x == 2) color = new Color(191, 187, 187);
            return color;
        }
    }
}
