using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Localization;
using RemnantOfTheAncientsMod;
using RemnantOfTheAncientsMod.VanillaChanges;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Items.Fmode
{
    public class SkeletonSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skeleton Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme Squelette");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma esqueleto");
			Tooltip.SetDefault("Increases defense by 5");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente la défense de 5");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la defensa en 5");

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

			return !player.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<SkeletonReaperSoulPlayer>().SkeletonReaperUpgrade = true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class SkeletonReaperSoulPlayer : ModPlayer
	{
		public bool SkeletonReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(SkeletonReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["SkeletonReaperUpgrade"] = SkeletonReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			SkeletonReaperUpgrade = tag.Get<bool>("SkeletonReaperUpgrade");
		}
	}
}
