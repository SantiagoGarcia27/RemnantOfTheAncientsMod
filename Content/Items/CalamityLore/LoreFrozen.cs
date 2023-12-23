using Terraria.ModLoader;
using Terraria.ID;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;

namespace RemnantOfTheAncientsMod.Content.Items.CalamityLore;

[ExtendsFromMod("CalamityMod")]
[LegacyName(new string[] { "KnowledgeLoreFrozen" })]
public class LoreFrozen : LoreItem
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
        Item.width = 20;
        Item.height = 20;
        Item.rare = ItemRarityID.Pink;
		Item.consumable = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient<FrostTrophy>()
			.AddTile(TileID.Bookcases)
			.Register();
	}
}
