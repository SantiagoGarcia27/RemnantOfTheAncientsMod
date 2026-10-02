using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using Terraria.ID;
using Terraria.ModLoader;

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
        Item.width = 20;
        Item.height = 20;
        Item.rare = ItemRarityID.Blue;
        Item.consumable = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient<DesertTrophy>()
			.AddTile(TileID.Bookcases)
			.Register();
	}
}
