using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Ammo
{
	[CloneByReference]
	public class EndlesStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Endless Star");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Étoile sans fin");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Estrella Eterna");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.FallenStar);		
			Item.maxStack = 1;
			Item.consumable = false;
			Item.value += Item.sellPrice(0,1,0,0);
			Item.rare += 1;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FallenStar, 500)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
		}
	}
}