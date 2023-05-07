using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Tiles;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Furniture
{
	public class TuxoniteDoor : ModItem
	{
		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Tuxonite Door");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Porte en tuxonite");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Puerta de tusonita");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
Item.width = 14;
Item.height = 28;
Item.maxStack = 99;
Item.useTurn = true;
Item.autoReuse = true;
Item.useAnimation = 15;
Item.useTime = 10;
Item.useStyle = ItemUseStyleID.Swing;
Item.consumable = true;
Item.value = 150;
Item.createTile = ModContent.TileType<TuxoniteDoorClosed>();
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
CreateRecipe()
	.AddIngredient<TuxoniteBar>()
	.AddTile(TileID.Anvils)
	.Register();
		}
	}
}