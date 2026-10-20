using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories
{
	public class IronBand : ModItem
	{
		int damageReduction = 2;

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(damageReduction);
        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 26; 
			Item.height = 26;
			Item.maxStack = 1; 
			Item.value = Item.buyPrice(0, 0, 2, 72);  
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
			Item.defense = 1;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.endurance += damageReduction/100f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddRecipeGroup(RecipeGroupID.IronBar, 2)
			.AddTile(TileID.Anvils)
			.Register();
		}
	}
}
