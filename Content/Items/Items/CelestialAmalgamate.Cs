using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace RemnantOfTheAncientsMod.Content.Items.Items
{
	public class CelestialAmalgamate : ModItem
	{
		public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 3));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 99;
			Item.value = 6000;
			Item.rare = ItemRarityID.Cyan;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.FragmentSolar, 1)
			.AddIngredient(ItemID.FragmentStardust, 1)
			.AddIngredient(ItemID.FragmentNebula, 1)
			.AddIngredient(ItemID.FragmentVortex, 1)
			.AddIngredient(ModContent.ItemType<Neutrum_Fragment>(), 1)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
		}
	}
}