using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using RemnantOfTheAncientsMod.Common.Global;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
{
	public class KingSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("King Slime Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "King Slime Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Rey Slime Soul");
			Tooltip.SetDefault("30% increased movement speed");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "30% d'augmentation de la vitesse de déplacement");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Velocidad de movimiento en 30%");
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

			return !player.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<SlimeReaperSoulPlayer>().SlimeReaperUpgrade = true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class SlimeReaperSoulPlayer : ModPlayer
	{
		public bool SlimeReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(SlimeReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["SlimeReaperUpgrade"] = SlimeReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			SlimeReaperUpgrade = tag.Get<bool>("SlimeReaperUpgrade");
		}
	}
}
