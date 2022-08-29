using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using RemnantOfTheAncientsMod.Items.Items;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Items.Pociones
{
	public class UltraHealtingPotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultra Healing potion");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Potion de guérison ultra");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Ultra poción curativa");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item3;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.rare = ItemRarityID.Cyan;
			Item.healLife = 300; 
			Item.potion = true; 
			Item.value = Item.buyPrice(gold: 10);
		}


		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.SuperHealingPotion, 4)
			.AddIngredient(ItemID.LunarBar, 1)
			.AddIngredient(ModContent.ItemType<Neutrum_Fragment>(),3)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}    