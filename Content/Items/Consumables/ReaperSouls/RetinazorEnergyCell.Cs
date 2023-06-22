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
    public class RetinazorEnergyCell : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Retinazor Energy Cell");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cellule énergétique Retinazor");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Célula de energía Retinazor");
			Tooltip.SetDefault("Removes restriction on wings of Reaper Mode");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Supprime la restriction sur les ailes du mode Reaper");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Elimina la restricción en las alas del modo Segador.");
			//ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
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
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
		public override bool CanUseItem(Player player)
		{

			return !player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[11];
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[11] = true;
			return true;
		}
	}
}
