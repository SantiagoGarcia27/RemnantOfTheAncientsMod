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
    public class FrozenSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Assaulter Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme d'assaillant gelée");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma del Asaltante Congelado");
			Tooltip.SetDefault("Grants immunity to Frozen, Chilled and Frostburn");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Accorde l'immunité à Frozen, Chilled et Frostburn");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Otorga inmunidad a Congelado, Helado y Quemadura de hielo");

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

			return !player.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<FrozenReaperSoulPlayer>().FrozenReaperUpgrade = true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class FrozenReaperSoulPlayer : ModPlayer
	{
		public bool FrozenReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(FrozenReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["FrozenReaperUpgrade"] = FrozenReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			FrozenReaperUpgrade = tag.Get<bool>("FrozenReaperUpgrade");
		}
	}
}
