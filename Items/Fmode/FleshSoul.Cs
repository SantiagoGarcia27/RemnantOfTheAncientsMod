using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using opswordsII.VanillaChanges;
using opswordsII;
using Terraria.ModLoader.IO;

namespace opswordsII.Items.Fmode
{
	public class FleshSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wall of Flesh Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Mur de chair âme");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma del Muro Carnoso");
			Tooltip.SetDefault("Increased all damage by 10%");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmentation de tous les dégâts de 10 %");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Incrementa todo el daño en un 10%");

			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 4));//5,4
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
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

			return !player.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<FleshReaperSoulPlayer>().FleshReaperUpgrade = true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class FleshReaperSoulPlayer : ModPlayer
	{
		public bool FleshReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(FleshReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["FleshReaperUpgrade"] = FleshReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			FleshReaperUpgrade = tag.Get<bool>("FleshReaperUpgrade");
		}
	}
}
