using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.VanillaChanges;
using RemnantOfTheAncientsMod;
using Terraria.ModLoader.IO;

namespace RemnantOfTheAncientsMod.Items.Fmode
{
	public class DeerclopsSoul : ModItem
	{
		private static readonly Color rarityColorOne = new Color(46, 45, 45);

		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Deerclops Soul");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme d'deerclops");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma del Ciervíclope");
			Tooltip.SetDefault("Grants immunity to Frozen, Chilled and Frostburn");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Accorde l'immunité à Frozen, Chilled et Frostburn");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Otorga inmunidad a Congelado, Helado y Quemadura de hielo");

			/*Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 4));//5,4
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;*/
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 1; 
			Item.value = 0;
			Item.GetGlobalItem<GlobalItem1>().customRarity = CustomRarity.Reaper;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.maxStack = 1;
			Item.UseSound = SoundID.Item60;
			Item.consumable = true;
		}

        public override bool CanUseItem(Player player) => !player.GetModPlayer<DeerclopsReaperSoulPlayer>().DeerclopsReaperUpgrade;
        public override bool? UseItem(Player player)
		{
			player.GetModPlayer<DeerclopsReaperSoulPlayer>().DeerclopsReaperUpgrade = true;
			return true;
		}
        internal static Color GetRarityColor() => Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
    }
	public class DeerclopsReaperSoulPlayer : ModPlayer
	{
		public bool DeerclopsReaperUpgrade = false;
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) => Mod.GetPacket().Write(DeerclopsReaperUpgrade);
        public override void SaveData(TagCompound tag) => tag["DeerclopsReaperUpgrade"] = DeerclopsReaperUpgrade;
        public override void LoadData(TagCompound tag) => DeerclopsReaperUpgrade = tag.Get<bool>("DeerclopsReaperUpgrade");
    }
}
