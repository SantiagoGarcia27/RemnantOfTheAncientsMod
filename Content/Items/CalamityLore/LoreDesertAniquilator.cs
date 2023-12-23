using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using Terraria.ModLoader;
using Terraria.ID;

namespace RemnantOfTheAncientsMod.Content.Items.CalamityLore;

[ExtendsFromMod("CalamityMod")]
[LegacyName(new string[] { "KnowledgeDesertAniquilator" })]
public class LoreDesertAniquilator : LoreItem
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 1;
		base.Item.consumable = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient<DesertTrophy>()
			.AddTile(TileID.Bookcases)
			.Register();
	}
}
