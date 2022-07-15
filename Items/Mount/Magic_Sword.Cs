using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Mounts;
using Terraria.GameContent.Creative;

namespace opswordsII.Items.Mount
{
	public class Magic_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magic Sword");
			Tooltip.SetDefault("Summons a traveling sword.");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "épée magique");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque une épée voyageuse");

			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada Magica");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca una espada de viaje.");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 30;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 30000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item79;
			Item.noMelee = true;
			Item.mountType = ModContent.MountType<Sword>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.EnchantedSword, 1)
			.AddRecipeGroup("EspH", 1)
			.AddIngredient(ItemID.SoulofFlight, 40)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}
}