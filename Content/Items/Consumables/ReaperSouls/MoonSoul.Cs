using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
{
	public class MoonSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Lord Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme du Seigneur de la Lune ");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma del Señor Lunar ");
			Tooltip.SetDefault("Reduces respawn time");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Réduit le temps de réapparition");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Reduce el tiempo de reaparición");


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

			return !player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<MoonReaperSoulPlayer>().MoonReaperUpgrade = true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class MoonReaperSoulPlayer : ModPlayer
	{
		public bool MoonReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(MoonReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["MoonReaperUpgrade"] = MoonReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			MoonReaperUpgrade = tag.Get<bool>("MoonReaperUpgrade");
		}
	}
}

