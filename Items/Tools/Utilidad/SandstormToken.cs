using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.GameContent.Events;

namespace RemnantOfTheAncientsMod.Items.Tools.Utilidad
{
	public class SandstormToken : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Token");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jeton de tempête de sable");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ficha arenosa");
			Tooltip.SetDefault("Summons the sandstorm");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque la tempête de sable");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca la tormenta de arena");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.maxStack = 99;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item60;
			Item.consumable = true;
		}


		public override bool? UseItem(Player player)
		{
			if (Main.netMode != 1)
			{
				if (Sandstorm.Happening)
				{
					SandstormOff();
					return true;
				}
				SandstormOn();
			}
			return true;
		}
		private static void SandstormOff()
		{
			Sandstorm.Happening = false;
			Sandstorm.TimeLeft = 0;
			SandstormStuff();
		}

		private static void SandstormOn()
		{
			Sandstorm.Happening = true;
			Sandstorm.TimeLeft = (int)(3600.0 * (8.0 + (double)Main.rand.NextFloat() * 16.0));
			SandstormStuff();
		}

		private static void SandstormStuff()
		{
			Sandstorm.IntendedSeverity = (Sandstorm.Happening ? (0.4f + Main.rand.NextFloat()) : ((Main.rand.Next(3) != 0) ? (Main.rand.NextFloat() * 0.3f) : 0f));
			NetMessage.SendData(7);
		}

	}
}
