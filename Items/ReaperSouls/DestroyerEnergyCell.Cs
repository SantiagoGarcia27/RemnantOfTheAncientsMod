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
	public class DestroyerEnergyCell : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Destroyer Energy Cell");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cellule énergétique destructeur");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Célula de energía del destructor");
//Tooltip.SetDefault("Increases mining speed by 35%");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente la vitesse de minage de 35%");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la velocidad de minería en un 35%.");
//ItemID.Sets.AnimatesAsSoul[Item.type] = true;
ItemID.Sets.ItemIconPulse[Item.type] = true;
ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults()
		{
Item.width = 26; //Tamaño inGame 
Item.height = 20;
Item.maxStack = 1; //Cantidad maxima en un espacio de inventario
Item.value = 0; //Precio
Item.GetGlobalItem<GlobalItem1>().customRarity = CustomRarity.Reaper;
Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
Item.maxStack = 1;
Item.UseSound = SoundID.Item60;
Item.consumable = true;
		}
		public override bool CanUseItem(Player player)
		{

return !player.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
player.GetModPlayer<DestroyerReaperSoulPlayer>().DestroyerReaperUpgrade = true;
return true;
		}
		internal static Color GetRarityColor()
		{
return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class DestroyerReaperSoulPlayer : ModPlayer
	{
		public bool DestroyerReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
ModPacket packet = Mod.GetPacket();
packet.Write(DestroyerReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
tag["DestroyerReaperUpgrade"] = DestroyerReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
DestroyerReaperUpgrade = tag.Get<bool>("DestroyerReaperUpgrade");
		}
	}
}
