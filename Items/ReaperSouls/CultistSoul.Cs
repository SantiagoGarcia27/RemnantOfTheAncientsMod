using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Items.ReaperSouls
{
	public class CultistSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cultist Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme de sectateur");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma del Sectario");
			Tooltip.SetDefault("A Ice Mist will protect you");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Une brume glaciale vous protégera");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Una niebla de hielo te protegerá");

			/*Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 4));//5,4
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;*/
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 26; //Tamaño inGame 
			Item.height = 26;
			Item.maxStack = 1; //Cantidad maxima en un espacio de inventario
			Item.value = 0; //Precio
			Item.GetGlobalItem<GlobalItem1>().customRarity = CustomRarity.Reaper;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.maxStack = 1;
			Item.UseSound = SoundID.Item60;
			Item.consumable = true;
		}
		public override bool CanUseItem(Player player)
		{

			return !player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<CultistReaperSoulPlayer>().CultistReaperUpgrade = true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class CultistReaperSoulPlayer : ModPlayer
	{
		public bool CultistReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(CultistReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["CultistReaperUpgrade"] = CultistReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			CultistReaperUpgrade = tag.Get<bool>("CultistReaperUpgrade");
		}
	}
}
