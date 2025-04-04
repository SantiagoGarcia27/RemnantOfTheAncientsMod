using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.World;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
    internal class ReaperChalice : ModItem
    {
        private static readonly Color rarityColorOne = GetReaperColor(1);

        private static readonly Color rarityColorTwo = GetReaperColor(2);

        int damageBonus = 50;
        int meleeSizeBonus = 150;
        int minionBonus = 1;
        int manaBonus = 20;
        int ammoSaveBonus = 20;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(damageBonus, meleeSizeBonus, minionBonus,manaBonus,ammoSaveBonus);
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Reaper.ReaperMode)
            {
                player.GetDamage(DamageClass.Generic) += 0.50f;

                player.maxMinions += minionBonus;
                player.statManaMax2 += manaBonus;
                player.GetModPlayer<RemnantPlayer>().NotConsumeAmmoChance = ammoSaveBonus;

                player.GetModPlayer<ReaperPlayer>().ChaliceOn = true;
                player.GetModPlayer<ReaperEffectsPlayer>().ReaperSoulsBoost(Item);
                player.GetModPlayer<ReaperEffectsPlayer>().ReaperSoulsBoost();
            }
        } 
        public override void SetDefaults()
        {
            Item.height = 34;
            Item.width = 30;
            Item.accessory = true;
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
            Item.GetGlobalItem<CustomTooltip>().ReaperItem = true;
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
