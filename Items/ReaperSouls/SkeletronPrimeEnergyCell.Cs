using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Items.ReaperSouls
{
    public class SkeletronPrimeEnergyCell : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Skeletron Prime Energy Cell");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cellule énergétique Skeletron Prime");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Célula de energía Skeletron Prime");
//Tooltip.SetDefault("Increase jump height drastically \nAllows to see the minerals");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmenter considérablement la hauteur de saut \nPermet de voir les minerais");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la altura del salto drásticamente \nPermite ver los minerales");
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

return !player.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
player.GetModPlayer<SkeletronPrimeReaperSoulPlayer>().SkeletronPrimeReaperUpgrade = true;
return true;
		}
	}
	public class SkeletronPrimeReaperSoulPlayer : ModPlayer
	{
		public bool SkeletronPrimeReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
ModPacket packet = Mod.GetPacket();
packet.Write(SkeletronPrimeReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
tag["SkeletronPrimeReaperUpgrade"] = SkeletronPrimeReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
SkeletronPrimeReaperUpgrade = tag.Get<bool>("SkeletronPrimeReaperUpgrade");
		}
	}
}
