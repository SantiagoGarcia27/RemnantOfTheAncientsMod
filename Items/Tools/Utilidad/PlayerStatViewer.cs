using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Tools.Utilidad
{
	public class PlayerStatViewer : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			DisplayName.SetDefault("Player Stat Viewer");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jeton de nuit");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ficha lunar");
			Tooltip.SetDefault("");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Invoque la lune ");
			//	DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca a la luna ");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.maxStack = 1;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
		}

		
		public override bool? UseItem(Player player)
		{
			Player p = new Player();
			if (Main.netMode != 1)
			{
				Main.NewText("Health:" + p.statLife +
				"\n Defense:" + p.statDefense +
				"\n Damage Reduction:" + p.endurance +
				"\n Speed:" + p.moveSpeed, Microsoft.Xna.Framework.Color.AliceBlue);
			}
			return true;
		}
	}
}
