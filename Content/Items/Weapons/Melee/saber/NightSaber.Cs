using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Content.Items.Items;
using RemnantOfTheAncientsMod.Content.Items.Consumables.Pociones.Models;
using SangarUtilities.Common.UtilsTweaks;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber
{
    public class NightSaber : SaberBase
	{
        public override Item ItemBase => new(ItemID.NightsEdge);
        public override float[] DashStrength => [1f, 0.75f];
  
		public override void AddRecipes()
		{
			if (RemnantOfTheAncientsMod.CalamityMod != null)
			{
				CreateRecipe()
				.AddRecipeGroup("CorruptSaber")
				.AddIngredient(ItemType<GrassSaber>())
				.AddIngredient(ItemID.Muramasa, 1)
				.AddIngredient(ItemType<FireSaber>())
				.AddIngredient(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
				.AddTile(TileID.DemonAltar)
				.Register();

				Recipe.Create(ItemID.NightsEdge)
				.AddIngredient(ItemType<NightBar>(), 10)
				.AddIngredient(CallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "PurifiedGel"), 5)
				.AddTile(TileID.DemonAltar)
				.Register();
			}
			else
			{
				CreateRecipe()
				.AddRecipeGroup("CorruptSaber")
				.AddIngredient(ItemType<GrassSaber>())
				.AddIngredient(ItemID.Muramasa, 1)
				.AddIngredient(ItemType<FireSaber>())
				.AddTile(TileID.DemonAltar)
				.Register();

				CreateRecipe()
				 .AddIngredient(ModContent.ItemType<NightBar>(), 10)
				 .AddTile(TileID.DemonAltar)
				 .Register();
			}
		}
	}
}

