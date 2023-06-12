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
    public class CorruptSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Corrupt Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme Âme corrompue");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma Corrupta");
			Tooltip.SetDefault("increases life regen");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "augmente la régénération de vie");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "aumenta la regeneración de vida");

			/*Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 4));//5,4
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;*/
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

			return !player.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<CorruptReaperSoulPlayer>().CorruptReaperUpgrade = true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class CorruptReaperSoulPlayer : ModPlayer
	{
		public bool CorruptReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(CorruptReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["CorruptReaperUpgrade"] = CorruptReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			CorruptReaperUpgrade = tag.Get<bool>("CorruptReaperUpgrade");
		}
	}
}
