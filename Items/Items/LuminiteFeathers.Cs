using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Items
{
	public class LuminiteFeathers : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Feathers");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Pióra luminescencyjn");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Plumes de luminite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Plumas de Luminita");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 99; 
			Item.value = 10000; 
			Item.rare = ItemRarityID.Cyan;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.Feather, 5)
			.AddIngredient(ItemID.LunarOre, 1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}
