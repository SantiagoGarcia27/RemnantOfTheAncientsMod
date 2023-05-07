using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
{
	public class EyeSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "il Âme");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma de ocular");
			Tooltip.SetDefault("Permanently increases maximum life by 10");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente de manière permanente la vie maximale de 10");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta permanentemente la vida máxima en 10");
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
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
			return !player.GetModPlayer<EyeReaperSoulPlayer>().EyeReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<EyeReaperSoulPlayer>().EyeReaperUpgrade = true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class EyeReaperSoulPlayer : ModPlayer
	{
		public bool EyeReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(EyeReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["EyeReaperUpgrade"] = EyeReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			EyeReaperUpgrade = tag.Get<bool>("EyeeReaperUpgrade");
		}
	}
}
