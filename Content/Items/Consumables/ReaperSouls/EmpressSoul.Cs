//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Terraria.Localization;
//using Microsoft.Xna.Framework;
//using Terraria.ModLoader.IO;
//using RemnantOfTheAncientsMod.Common.Global;
//using RemnantOfTheAncientsMod.Common.UtilsTweaks;

//namespace RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls
//{
//    public class EmpressSoul : ModItem
//	{
//		private static readonly Color rarityColorOne = new Color(46, 45, 45);

//		private static readonly Color rarityColorTwo = new Color(191, 187, 187);
//		public override void SetStaticDefaults()
//		{
//			//DisplayName.SetDefault("Empress of Light Soul");
//			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme de Impératrice de Lumière");
//			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma de la Emperatriz de la luz");
//			//Tooltip.SetDefault("Grants infinite wing and rocket boot flight");
//			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Accorde un vol infini d'ailes et de fusées");
//			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Otorga vuelo infinito");	
//			ItemID.Sets.ItemIconPulse[Item.type] = true;
//			ItemID.Sets.ItemNoGravity[Item.type] = true;
//		}

//		public override void SetDefaults()
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
//			return !player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[17];
//		}
//		public override bool? UseItem(Player player)
//		{
//			player.GetModPlayer<ReaperPlayer>().SoulsUpgrades[17] = true;
//			return true;
//		}
//		internal static Color GetRarityColor() => Utils1.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
//	}
//}
