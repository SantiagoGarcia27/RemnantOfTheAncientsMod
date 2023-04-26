using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Items.Tools.Utilidad
{
	public class NightToken : ModItem
	{
		public override void SetStaticDefaults()
		{
//DisplayName.SetDefault("Night Token");
//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Żeton Nocy");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Jeton de nuit");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ficha lunar");
//Tooltip.SetDefault("Summons the moon");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"Przywołuje księżyc ");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Invoque la lune ");
//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca a la luna ");
CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
Item.width = 20;
Item.height = 20;
Item.rare = ItemRarityID.Pink;
Item.useAnimation = 20;
Item.useTime = 20;
Item.maxStack = 99;
Item.useStyle = ItemUseStyleID.HoldUp;
Item.UseSound = SoundID.Item60;
Item.consumable = true;
		}

		
		public override bool? UseItem(Player player)
		{
if (Main.netMode != NetmodeID.MultiplayerClient)
{
	Main.dayTime = false;
	Netcode.SyncWorld();
}
return true;
		}
	}
}
