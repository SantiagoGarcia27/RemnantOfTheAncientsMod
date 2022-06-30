using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Tools.Utilidad
{
	public class RainToken : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rain Token");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jeton de pluie");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ficha de lluvia");
			Tooltip.SetDefault("Summons the rain");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Invoque la pluie");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca a la lluvia ");
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
			if (Main.raining)
			{
				RainOff();
			}
			else if (!Main.raining && !Main.slimeRain)
			{
				RainOn();
			}
		}
		return true;
		}

		private static void RainOff()
	{
		//MiscMethods.WriteText("Rain disabled.", new Color(0, 128, 255));
		Main.raining = false;
		Main.rainTime = 0;
		Main.maxRaining = 0f;
		NetMessage.SendData(7);
	}

	private static void RainOn()
	{
		//MiscMethods.WriteText("Rain enabled.", new Color(0, 128, 255));
		int num = 86400;
		int num2 = num / 24;
		Main.rainTime = Main.rand.Next(num2 * 8, num);
		if (Main.rand.Next(3) == 0)
		{
			Main.rainTime += Main.rand.Next(0, num2);
		}
		if (Main.rand.Next(4) == 0)
		{
			Main.rainTime += Main.rand.Next(0, num2 * 2);
		}
		if (Main.rand.Next(5) == 0)
		{
			Main.rainTime += Main.rand.Next(0, num2 * 2);
		}
		if (Main.rand.Next(6) == 0)
		{
			Main.rainTime += Main.rand.Next(0, num2 * 3);
		}
		if (Main.rand.Next(7) == 0)
		{
			Main.rainTime += Main.rand.Next(0, num2 * 4);
		}
		if (Main.rand.Next(8) == 0)
		{
			Main.rainTime += Main.rand.Next(0, num2 * 5);
		}
		float num3 = 1f;
		if (Main.rand.Next(2) == 0)
		{
			num3 += 0.05f;
		}
		if (Main.rand.Next(3) == 0)
		{
			num3 += 0.1f;
		}
		if (Main.rand.Next(4) == 0)
		{
			num3 += 0.15f;
		}
		if (Main.rand.Next(5) == 0)
		{
			num3 += 0.2f;
		}
		Main.rainTime = (int)((float)Main.rainTime * num3);
		RainStuff();
		Main.raining = true;
		NetMessage.SendData(7);
	}

	private static void RainStuff()
	{
		if ((double)Main.cloudBGActive >= 1.0 || (double)Main.numClouds > 150.0)
		{
			if (Main.rand.Next(3) == 0)
			{
				Main.maxRaining = (float)Main.rand.Next(20, 90) * 0.01f;
			}
			else
			{
				Main.maxRaining = (float)Main.rand.Next(40, 90) * 0.01f;
			}
		}
		else if ((double)Main.numClouds > 100.0)
		{
			if (Main.rand.Next(3) == 0)
			{
				Main.maxRaining = (float)Main.rand.Next(10, 70) * 0.01f;
			}
			else
			{
				Main.maxRaining = (float)Main.rand.Next(20, 60) * 0.01f;
			}
		}
		else if (Main.rand.Next(3) == 0)
		{
			Main.maxRaining = (float)Main.rand.Next(5, 40) * 0.01f;
		}
		else
		{
			Main.maxRaining = (float)Main.rand.Next(5, 30) * 0.01f;
		}
	}


	}
}