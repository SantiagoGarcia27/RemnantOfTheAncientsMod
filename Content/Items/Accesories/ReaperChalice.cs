﻿using Terraria;
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
            DisplayName.SetDefault("Chalice of souls");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "moissonneuse");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cáliz de almas");
            Tooltip.SetDefault("You can store the souls of defeated bosses in this chalice to gain their power."
            + "\n(only works in reaper)");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vous pouvez stocker les âmes des boss vaincus dans ce calice pour gagner leur pouvoir."
            + "\n(ne fonctionne que dans Reaper)");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Puedes almacenar las almas de los jefes derrotados en este caliz para obtener su poder."
            + "\n(Solo funciona en cegador)");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<RemnantPlayer>().ChaliceOn = true;
            player.GetModPlayer<RemnantPlayer>().ReaperSoulsBoost(Item);
            player.GetModPlayer<RemnantPlayer>().ReaperSoulsBoost();
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
