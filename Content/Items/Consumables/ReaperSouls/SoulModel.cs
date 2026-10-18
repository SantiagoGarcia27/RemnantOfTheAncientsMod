using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.ReaperSystem;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using SangarUtilities.Common.UtilsTweaks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
{
    public abstract class SoulModel : ModItem
    {
        private static readonly Color rarityColorOne = new(46, 45, 45);

        private static readonly Color rarityColorTwo = new(191, 187, 187);
        public abstract Mod ItemMod { get; }
        public abstract int NpcType { get; }
        public virtual float MaxValue => 1f;
        public virtual float Increment => 1f;
        public virtual float Progression => 1f;

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
            Item.GetGlobalItem<CustomTooltip>().ReaperItem = true;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.maxStack = 1;
            Item.UseSound = SoundID.Item60;
            Item.consumable = true;

        }
        public override bool CanUseItem(Player player)
        {
            ReaperSoulsPlayer reaperSoulsPlayer = player.GetModPlayer<ReaperSoulsPlayer>();

            bool hasSoul = ItemMod != RemnantOfTheAncientsMod.CalamityMod ?
                reaperSoulsPlayer.SoulsUpgradesLoaded[ReaperSoulsPlayer.GetIndexFromLoadedBossById(NpcType)] :
                reaperSoulsPlayer.SoulsUpgradesMaybeLoaded[NpcType];

            return !hasSoul;
        }
        public override bool? UseItem(Player player)
        {
            ReaperSoulsPlayer reaperSoulsPlayer = player.GetModPlayer<ReaperSoulsPlayer>();

            if (ItemMod == RemnantOfTheAncientsMod.CalamityMod) reaperSoulsPlayer.SoulsUpgradesMaybeLoaded[NpcType] = true;
            else reaperSoulsPlayer.SoulsUpgradesLoaded[ReaperSoulsPlayer.GetIndexFromLoadedBossById(NpcType)] = true;
   
            return true;
        }
        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        }

        public void RegisterSoul()
        {
            ReaperUpgradeRegister register = new()
            {
                itemType = Type,
                npcType = NpcType,
                MaxValue = MaxValue,
                Increment = Increment,
                Progression = Progression
            };

            ListUtils.AddSecure(ref Reaper.soulData, register);
        }

    }
}