using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Common.Global;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Reaper
{
	[AutoloadEquip(EquipType.Legs)]
	public class Reaper_Pants : ModItem
	{
        private readonly int IncreasedMovementSpeed = 10;
        private readonly int IncreasesMaxMana = 40;
        private readonly int PercentIncreasedCritChance = 5;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedMovementSpeed, IncreasesMaxMana, PercentIncreasedCritChance);
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}   
        private static readonly Color rarityColorOne = Utils1.GetReaperColor(1);

        private static readonly Color rarityColorTwo = Utils1.GetReaperColor(2);

        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 9f);
        }
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
			Item.GetGlobalItem<CustomTooltip>().ReaperItem = true;
            Item.rare = ItemRarityID.Purple;
            Item.defense = 27;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += IncreasedMovementSpeed/100f;
			player.statManaMax2 += IncreasesMaxMana;
			player.GetCritChance(DamageClass.Generic) += PercentIncreasedCritChance;
        }
	}
}

