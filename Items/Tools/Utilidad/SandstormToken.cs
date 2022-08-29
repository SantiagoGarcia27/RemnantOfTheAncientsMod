using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Events;
using System.Reflection;

namespace RemnantOfTheAncientsMod.Items.Tools.Utilidad
{
	public class SandstormToken : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Token");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Żeton Nocy");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jeton de nuit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ficha lunar");
			Tooltip.SetDefault("Summons the sandstorm");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"Przywołuje księżyc ");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Invoque la lune ");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca a la luna ");
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
