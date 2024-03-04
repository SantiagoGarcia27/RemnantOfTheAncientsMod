//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.Localization;
//using Microsoft.Xna.Framework;
//using Terraria.ModLoader.IO;
//using RemnantOfTheAncientsMod.Common.Global;
//using RemnantOfTheAncientsMod.Common.UtilsTweaks;
//using CalamityMod.Projectiles.Magic;

//namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
//{
//    public class DestroyerEnergyCell : ModItem
//	{
//		private static readonly Color rarityColorOne = new Color(46, 45, 45);

//		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
//		public override void SetStaticDefaults()
//		{
//			//DisplayName.SetDefault("Destroyer Energy Cell");
//			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Cellule énergétique destructeur");
//			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Célula de energía del destructor");
//			//Tooltip.SetDefault("Increases mining speed by 35%");
//			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente la vitesse de minage de 35%");
//			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta la velocidad de minería en un 35%.");
//			ItemID.Sets.ItemIconPulse[Item.type] = true;
//			ItemID.Sets.ItemNoGravity[Item.type] = true;
//		}
//		int MiningBoost = 35;
//        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MiningBoost);

//        public override void SetDefaults()
//		{
//			Item.width = 26; 
//			Item.height = 20;
//			Item.maxStack = 1; 
//			Item.value = 0; 
//			Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
//			Item.useTime = 20;
//			Item.useAnimation = 20;
//			Item.useStyle = ItemUseStyleID.HoldUp;
//			Item.maxStack = 1;
//			Item.UseSound = SoundID.Item60;
//			Item.consumable = true;
//		}
//		public override bool CanUseItem(Player player)
//		{
//			return !player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[13];
//		}
//		public override bool? UseItem(Player player)
//		{
//			player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[13] = true;
//			return true;
//		}
//		internal static Color GetRarityColor()
//		{
//			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
//		}
//	}
//}
