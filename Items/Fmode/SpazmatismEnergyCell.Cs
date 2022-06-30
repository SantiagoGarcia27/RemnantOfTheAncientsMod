using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using opswordsII;
using opswordsII.VanillaChanges;
using Terraria.ModLoader.IO;

namespace opswordsII.Items.Fmode
{
	public class SpazmatismEnergyCell : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spazmatism Energy Cell");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Cellule énergétique du spazmatisme");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Célula de energía del espazmatisador");
			Tooltip.SetDefault("Grants immunity to all fire debuffs");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Accorde l'immunité à tous les affaiblissements de feu");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Otorga inmunidad a todas las desventajas de fuego.");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 4));//5,4
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			//ItemID.Sets.ItemIconPulse[Item.type] = true;
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
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
		public override bool CanUseItem(Player player)
		{

			return !player.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<SpazmatismReaperSoulPlayer>().SpazmatismReaperUpgrade = true;
			return true;
		}
	}
	public class SpazmatismReaperSoulPlayer : ModPlayer
	{
		public bool SpazmatismReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(SpazmatismReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["SpazmatismReaperUpgrade"] = SpazmatismReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			SpazmatismReaperUpgrade = tag.Get<bool>("SpazmatismReaperUpgrade");
		}
	}
}
