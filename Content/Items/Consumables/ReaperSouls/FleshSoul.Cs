//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.Localization;
//using Microsoft.Xna.Framework;
//using RemnantOfTheAncientsMod.Common.Global;
//using RemnantOfTheAncientsMod.Common.UtilsTweaks;

//namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
//{
//    public class FleshSoul : ModItem
//	{
//		private static readonly Color rarityColorOne = new Color(46, 45, 45);

//		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
//		public override void SetStaticDefaults()
//		{
//			//DisplayName.SetDefault("Wall of Flesh Soul");
//			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Mur de chair âme");
//			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma del Muro Carnoso");
//			//Tooltip.SetDefault("Increased all damage by 10%");
//			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmentation de tous les dégâts de 10 %");
//			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Incrementa todo el daño en un 10%");
//			ItemID.Sets.ItemIconPulse[Item.type] = true;
//			ItemID.Sets.ItemNoGravity[Item.type] = true;
//		}
//        int DamageBoost = 10;
//        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost);
//        public override void SetDefaults()
//		{
//			Item.width = 26; 
//			Item.height = 26;
//			Item.maxStack = 1; 
//			Item.value = 0; 
//			Item.GetGlobalItem<CustomTooltip>().customRarity = CustomRarity.Reaper;
//			Item.useAnimation = 20;
//			Item.useTime = 20;
//			Item.useStyle = ItemUseStyleID.HoldUp;
//			Item.maxStack = 1;
//			Item.UseSound = SoundID.Item60;
//			Item.consumable = true;
//		}

//		public override bool CanUseItem(Player player)
//		{

//			return !player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[7];
//		}
//		public override bool? UseItem(Player player)
//		{
//			player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[7] = true;
//			return true;
//		}
//		internal static Color GetRarityColor()
//		{
//			return Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
//		}
//	}
//}
