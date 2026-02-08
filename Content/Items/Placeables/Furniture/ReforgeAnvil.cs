using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Content.Tiles.Stations;

namespace RemnantOfTheAncientsMod.Content.Items.Placeables.Furniture
{ 
    public class ReforgeAnvil : ModItem
	{
		public override void SetStaticDefaults() 
		{
			//DisplayName.SetDefault("Tuxonite Toilet");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Toilette en tuxonite");
           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Inodoro de tusonita");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults() 
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<ReforgeAnvilTile>());
			Item.value = 150;
			Item.maxStack = 99;
			Item.width = 16;
			Item.height = 24;
		}
		
	}
}
