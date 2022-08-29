using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Projectiles;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Mele
{
    public class Solarang: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solarang");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "boomerang solaire");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bumerán solar");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Flamarang);
			Item.damage = 160;
			Item.useTime = 10;
			Item.useAnimation = 10;       
			Item.shoot = ModContent.ProjectileType<SolarangP>();
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FragmentSolar, 17)
			.AddIngredient(ItemID.Flamarang, 1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
