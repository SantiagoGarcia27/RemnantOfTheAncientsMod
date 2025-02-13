using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models
{
    public abstract class SoulModel : ModItem
    {
        private static readonly Color rarityColorOne = new(46, 45, 45);

        private static readonly Color rarityColorTwo = new(191, 187, 187);
        public abstract Mod ItemMod { get; }
        public abstract NPC BaseNpc { get; }

        internal static Mod _mod;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return ItemMod != null;
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.maxStack = 1;
            Item.value = 0;
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 1;
            Item.UseSound = SoundID.Item60;
            Item.consumable = true;
        }
        public override bool CanUseItem(Player player)
        {
            Dictionary<int, bool> souls = player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ItemMod];
            bool hasSoul = player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[ItemMod][BaseNpc.type];
            return !hasSoul;
        }
        public override bool? UseItem(Player player)
        {
            ReaperPlayer rp = player.GetModPlayer<ReaperPlayer>();
            rp.SoulsUpgrades[ItemMod][BaseNpc.type] = true;
            return true;
        }
        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        }
    }
}