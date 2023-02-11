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
	public class InfernalSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infernal Tyrant Soul");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme du tyran infernal");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma de tirano infernal");
			Tooltip.SetDefault("Increased all damage by 10%"
			+ "\nGrants immunity lava and fire blocks");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmentation de tous les dégâts de 10 %"
			+ "\nConfère l'immunité à la lave et aux blocs de feu");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Incrementa todo el daño en un 10%"
			+ "\nOtorga inmunidad a los bloques de lava y fuego.");
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

			return !player.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
			player.GetModPlayer<InfernalReaperSoulPlayer>().InfernalReaperUpgrade= true;
			return true;
		}
		internal static Color GetRarityColor()
		{
			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class InfernalReaperSoulPlayer : ModPlayer
	{
		public bool InfernalReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			ModPacket packet = Mod.GetPacket();
			packet.Write(InfernalReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
			tag["InfernalReaperUpgrade"] = InfernalReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
			InfernalReaperUpgrade = tag.Get<bool>("InfernalReaperUpgrade");
		}
	}
}
