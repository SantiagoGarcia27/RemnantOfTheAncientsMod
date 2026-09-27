using RemnantOfTheAncientsMod.Content.Projectiles.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee
{
	public class Solarang : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Solarang");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "boomerang solaire");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Bumerán solar");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Flamarang);
			Item.damage = 130;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.autoReuse = true;
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
