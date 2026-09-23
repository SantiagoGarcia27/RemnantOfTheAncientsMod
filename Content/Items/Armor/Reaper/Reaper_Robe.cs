using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Armor.Reaper
{
	[AutoloadEquip(EquipType.Body)]
	public class Reaper_Robe : ModItem
	{
        private static readonly Color rarityColorOne = Utils1.GetReaperColor(1);

        private static readonly Color rarityColorTwo = Utils1.GetReaperColor(2);

        private readonly int IncreasedDamageReduction = 10;
        private readonly int MaxLifeBonus = 40;
        private readonly int MaxMinions = 2;
        private readonly int MaxCentries = 2;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(IncreasedDamageReduction, MaxLifeBonus, MaxMinions, MaxCentries);
        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        }
        public override void SetStaticDefaults()
		{		
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
       
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
            Item.GetGlobalItem<CustomTooltip>().ReaperItem = true;
            Item.rare = ItemRarityID.Purple;
            Item.defense = 38;
		}
        public override void UpdateEquip(Player player)
        {
			player.endurance += IncreasedDamageReduction/100f;
            player.maxMinions += MaxMinions;
            player.maxTurrets += MaxCentries;
            player.raven = true;
            player.statLifeMax2 += MaxLifeBonus;        
        }
	}
}

