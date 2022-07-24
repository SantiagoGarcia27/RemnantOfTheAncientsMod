using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Items.Items;

namespace opswordsII.Items.Tools.Utilidad
{
    public class MoonManipulator : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Manipulator");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Manipulateur de la lune");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Manipulador lunar");
			Tooltip.SetDefault("Summons the moon");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Invoque la lune ");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca a la luna ");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.maxStack = 1;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
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
		public override void AddRecipes() //Crafteo del objeto
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<NightToken>(),30)
			.AddIngredient(ItemID.SoulofLight, 5)
            .AddIngredient(ItemID.SoulofNight, 5)
			.AddIngredient(ModContent.ItemType<NightBar>(),2)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		
		}
	}
}
