using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Items.Items;
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

        internal static Color GetRarityColor()
        {
            return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Reaper Robe");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vraie robe de faucheuse");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Verdadera túnica de parca");
            Tooltip.SetDefault(tooltip());
          
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
        public static string tooltip()
        {
            return LocalizationHelper.IncreasedDamageReductionTooltip(10) +
                "\n" + LocalizationHelper.IncreasedMinionTooltip(2) +
                "\n" + LocalizationHelper.IncreasedCentryTooltip(2) +
                "\n" + LocalizationHelper.IncreasedLifeMaxTooltip(40);   
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
			player.endurance += 0.10f;
            player.maxMinions += 2;
            player.maxTurrets += 2;
            player.raven = true;
            player.statLifeMax2 += 40;        
        }
	}
}

