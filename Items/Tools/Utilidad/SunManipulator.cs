using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace opswordsII.Items.Tools.Utilidad
{
	public class SunManipulator : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sun Manipulator");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Manipulator Słońca");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Manipulateur solaire");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Manipulador solar");
			Tooltip.SetDefault("Summons the sun");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish),"Przywołuje  słońce");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Invoque le soleil");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca al sol");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 4;
			Item.maxStack = 1;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
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
		public override void AddRecipes() //Crafteo del objeto
		{
			CreateRecipe()
			.AddIngredient(null,"DayToken",30)
			.AddIngredient(ItemID.SoulofLight, 5)
            .AddIngredient(ItemID.SoulofNight, 5)
			.AddIngredient(ItemID.SunplateBlock, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		
		}
	}
}
