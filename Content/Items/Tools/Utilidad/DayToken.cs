using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad
{
	public class DayToken : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Day Token");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Żeton dnia");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jeton de jour");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ficha solar");
			Tooltip.SetDefault("Summons the sun");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przywołuje słońce");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque le soleil");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Convoca al sol");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.maxStack = 99;
			Item.UseSound = SoundID.Item60;
			Item.consumable = true;
		}


		public override bool? UseItem(Player player)
		{
			//if (Main.netMode != NetmodeID.MultiplayerClient)
			//{
			Main.dayTime = true;
            Main.time = 5000.0;
            Netcode.SyncWorld();
			//}
			return true;
		}
	}
}
