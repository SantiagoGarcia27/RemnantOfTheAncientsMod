using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Tools.Utilidad
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
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"Przywołuje słońce");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Invoque le soleil");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca al sol");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 4;
			Item.maxStack = 99;
			Item.UseSound = SoundID.Item60;
			Item.consumable = true;
		}

		
		public override bool? UseItem(Player player)
		{
			if (Main.netMode != 1)
			{
				Main.dayTime = true;
				Netcode.SyncWorld();
			}
			return true;
		}
	}
}
