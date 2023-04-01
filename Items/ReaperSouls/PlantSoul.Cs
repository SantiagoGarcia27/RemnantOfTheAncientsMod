using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.VanillaChanges;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Items.ReaperSouls
{
	public class PlantSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Plantera Soul");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Plantera Âme");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma de Plantera");
Tooltip.SetDefault("Permanently increases maximum life by 10"
+ "\nSummons spores over time that will damage enemies");
Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente de manière permanente la vie maximale de 10"
+ "\nInvoque des spores au fil du temps qui endommageront les ennemis");
Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta permanentemente la vida máxima en 10"
+ "\nInvoca esporas a lo largo del tiempo que dañarán a los enemigos.");
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

return !player.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
player.GetModPlayer<PlantReaperSoulPlayer>().PlantReaperUpgrade = true;
return true;
		}
		internal static Color GetRarityColor()
		{
return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
		}
	}
	public class PlantReaperSoulPlayer : ModPlayer
	{
		public bool PlantReaperUpgrade = false;
		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
ModPacket packet = Mod.GetPacket();
packet.Write(PlantReaperUpgrade);
		}
		public override void SaveData(TagCompound tag)
		{
tag["PlantReaperUpgrade"] = PlantReaperUpgrade;
		}
		public override void LoadData(TagCompound tag)
		{
PlantReaperUpgrade = tag.Get<bool>("PlantReaperUpgrade");
		}
	}
}
