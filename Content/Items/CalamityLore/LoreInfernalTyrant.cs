using CalamityMod.Items.Placeables.Furniture.Trophies;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Items.CalamityLore;

[ExtendsFromMod("CalamityMod")]
[LegacyName(new string[] { "KnowledgeLoreInfernalTyrant" })]
public class LoreInfernalTyrant : LoreItem
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
        Item.width = 20;
        Item.height = 20;
        Item.rare = ItemRarityID.Orange;
        Item.consumable = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient<HiveMindTrophy>()
			.AddTile(TileID.Bookcases)
			.Register();
	}
}
