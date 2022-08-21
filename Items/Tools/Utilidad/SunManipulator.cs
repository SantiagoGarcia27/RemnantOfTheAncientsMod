using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;

namespace opswordsII.Items.Tools.Utilidad
{
    public class SunManipulator : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sun Manipulator");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Manipulateur solaire");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Manipulador solar");
			Tooltip.SetDefault("Summons the sun");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French),"Invoque le soleil");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish),"Convoca al sol");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.maxStack = 1;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
		}

		
		public override bool? UseItem(Player player)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				Main.dayTime = true;
				Netcode.SyncWorld();
			}
			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ModContent.ItemType<DayToken>(),30)
			.AddIngredient(ItemID.SoulofLight, 5)
            .AddIngredient(ItemID.SoulofNight, 5)
			.AddIngredient(ItemID.SunplateBlock, 5)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		
		}
	}
}
