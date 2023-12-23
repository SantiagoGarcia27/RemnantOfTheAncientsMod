using CalamityMod.Items.Placeables.Furniture.Trophies;
using Terraria.ModLoader;
using Terraria.ID;

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
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 3;
		base.Item.consumable = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient<HiveMindTrophy>()
			.AddTile(TileID.Bookcases)
			.Register();
	}
}
