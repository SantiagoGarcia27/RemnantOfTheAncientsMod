using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
{
    public class EyeSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Eye Soul");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "il Âme");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma de ocular");
			//Tooltip.SetDefault("Permanently increases maximum life by 10");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente de manière permanente la vie maximale de 10");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta permanentemente la vida máxima en 10");
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}
        int LifeBoost = 10;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LifeBoost);

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
			return !player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[1];
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[1] = true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}

}
