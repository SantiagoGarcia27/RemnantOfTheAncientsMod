using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using opswordsII.Mounts;

namespace opswordsII.Items.Mount
{
	public class Magic_Sword : ModItem
	{
		public override void SetStaticDefaults()
		{
			
            DisplayName.SetDefault("Magic Sword");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "magiczny miecz");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "épée magique");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Espada Magica");
			Tooltip.SetDefault("Summons a traveling sword.");
           Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Przyzywa wędrujący miecz");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Invoque une épée voyageuse");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Invoca una espada de viaje.");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 30;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.value = 30000;
			Item.rare = 4;
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