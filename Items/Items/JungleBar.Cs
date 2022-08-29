using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Items
{
	public class JungleBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jungle Bar");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Polish), "Jungle Bar");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Bar de la jungle");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Lingote de la jungla");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 99;
			Item.value = 100;
			Item.rare = ItemRarityID.Orange;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = TileType<Tiles.JungleBarB>();
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.JungleSpores, 2)
			.AddIngredient(ItemID.Stinger, 2)
			.AddTile(TileID.Anvils)
			.Register();
		}    
        
	}
}