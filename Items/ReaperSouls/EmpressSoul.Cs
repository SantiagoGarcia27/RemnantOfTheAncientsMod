using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.VanillaChanges;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Items.ReaperSouls
{
    public class EmpressSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Empress of Light Soul");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme de Impératrice de Lumière");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma de la Emperatriz de la luz");
Tooltip.SetDefault("Grants infinite wing and rocket boot flight");
Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Accorde un vol infini d'ailes et de fusées");
Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Otorga vuelo infinito");
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

return !player.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade;
		}
		public override bool? UseItem(Player player)
		{
player.GetModPlayer<EmpressReaperSoulPlayer>().EmpressReaperUpgrade = true;
return true;
		}
        internal static Color GetRarityColor() => Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
    }
	public class EmpressReaperSoulPlayer : ModPlayer
	{
		public bool EmpressReaperUpgrade = false;
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) => Mod.GetPacket().Write(EmpressReaperUpgrade);
        public override void SaveData(TagCompound tag) => tag["EmpressReaperUpgrade"] = EmpressReaperUpgrade;
        public override void LoadData(TagCompound tag) => EmpressReaperUpgrade = tag.Get<bool>("EmpressReaperUpgrade");
    }
}
