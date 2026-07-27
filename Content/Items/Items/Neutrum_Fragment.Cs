using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class Neutrum_Fragment : ModItem
	{
		public override void SetStaticDefaults()
		{
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 99;
			Item.value = 2000;
			Item.rare = ItemRarityID.Cyan;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FragmentSolar, 1)
			.AddIngredient(ItemID.FragmentStardust, 1)
			.AddIngredient(ItemID.FragmentNebula, 1)
			.AddIngredient(ItemID.FragmentVortex, 1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}