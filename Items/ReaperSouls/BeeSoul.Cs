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
	//este es un comentario de prueba no es necesario para nada y un XD
	public class BeeSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Queen Bee Soul");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme de la reine des abeilles");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma de la Reina Abeja");
//Tooltip.SetDefault("Releases bees when damaged");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Vous relâchez les abeilles quand elles vous font du mal");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Liberas abejas cuando te dañan");

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

return !player.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
player.GetModPlayer<BeeReaperSoulPlayer>().BeeReaperUpgrade = true;
return true;
		}
		internal static Color GetRarityColor()
		{
return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class BeeReaperSoulPlayer : ModPlayer
	{
		public bool BeeReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
ModPacket packet = Mod.GetPacket();
packet.Write(BeeReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
tag["BeeReaperUpgrade"] = BeeReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
BeeReaperUpgrade = tag.Get<bool>("BeeReaperUpgrade"); 
		}
	}
}

