using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod
{
	// In this class we separate recipe related code from our main class
	public static class RecipeHelper
	{
			
		/*public static void ExampleRecipeEditing(Mod mod) {

			// In the following example, we find recipes that uses a chain as ingredient and then we remove that ingredient from the recipe.
			{
			RecipeFinder finder = new RecipeFinder(); // make a new RecipeFinder 
			finder.AddIngredient(ItemID.Bone, 40);
			finder.AddIngredient(ItemID.Cobweb, 40);
			finder.AddTile(TileID.WorkBenches); // add a required tile, any anvil
			finder.SetResult(ItemID.NecroHelmet); // set the result to be 10 chains
			Recipe exactRecipe = finder.FindExactRecipe();
			
			bool isRecipeFound = exactRecipe != null; // if our recipe is not null, it means we found the exact recipe
			if (isRecipeFound) // since our recipe is found, we can continue
			{
				RecipeEditor editor = new RecipeEditor(exactRecipe); // for our recipe, make a new RecipeEditor
				editor.DeleteRecipe(); // delete the recipe
			}

			}
			
			{
			RecipeFinder finder = new RecipeFinder();
			finder.AddIngredient(ItemID.Bone, 60);
			finder.AddIngredient(ItemID.Cobweb, 50);
			finder.AddTile(TileID.WorkBenches); // add a required tile, any anvil
			finder.SetResult(ItemID.NecroBreastplate); // set the result to be 10 chains
			Recipe exactRecipe = finder.FindExactRecipe();
			 // try to find the exact recipe matching our criteria

			bool isRecipeFound = exactRecipe != null; // if our recipe is not null, it means we found the exact recipe
			if (isRecipeFound) // since our recipe is found, we can continue
			{
				RecipeEditor editor = new RecipeEditor(exactRecipe); // for our recipe, make a new RecipeEditor
				editor.DeleteRecipe(); // delete the recipe
			}
			// The following is a more precise example, finding an exact recipe and deleting it if possible.
			
			}


			{
			RecipeFinder finder = new RecipeFinder();
			finder.AddIngredient(ItemID.Bone, 50);
			finder.AddIngredient(ItemID.Cobweb, 45);
			finder.AddTile(TileID.WorkBenches); // add a required tile, any anvil
			finder.SetResult(ItemID.NecroGreaves); // set the result to be 10 chains
			Recipe exactRecipe = finder.FindExactRecipe();
			 // try to find the exact recipe matching our criteria

			bool isRecipeFound = exactRecipe != null; // if our recipe is not null, it means we found the exact recipe
			if (isRecipeFound) // since our recipe is found, we can continue
			{
				RecipeEditor editor = new RecipeEditor(exactRecipe); // for our recipe, make a new RecipeEditor
				editor.DeleteRecipe(); // delete the recipe
			}
			// The following is a more precise example, finding an exact recipe and deleting it if possible.
			
			}


			{
			RecipeFinder finder = new RecipeFinder();
			finder.AddIngredient(ItemID.JungleSpores, 8);
			finder.AddTile(TileID.Anvils); // add a required tile, any anvil
			finder.SetResult(ItemID.JungleHat); // set the result to be 10 chains
			Recipe exactRecipe = finder.FindExactRecipe();
			 // try to find the exact recipe matching our criteria

			bool isRecipeFound = exactRecipe != null; // if our recipe is not null, it means we found the exact recipe
			if (isRecipeFound) // since our recipe is found, we can continue
			{
				RecipeEditor editor = new RecipeEditor(exactRecipe); // for our recipe, make a new RecipeEditor
				editor.DeleteRecipe(); // delete the recipe
			}
			// The following is a more precise example, finding an exact recipe and deleting it if possible.
			
			}


			{
			RecipeFinder finder = new RecipeFinder();
			finder.AddIngredient(ItemID.JungleSpores, 16);
			finder.AddIngredient(ItemID.Stinger, 10);
			finder.AddTile(TileID.Anvils); // add a required tile, any anvil
			finder.SetResult(ItemID.JungleShirt); // set the result to be 10 chains
			Recipe exactRecipe = finder.FindExactRecipe();
			 // try to find the exact recipe matching our criteria

			bool isRecipeFound = exactRecipe != null; // if our recipe is not null, it means we found the exact recipe
			if (isRecipeFound) // since our recipe is found, we can continue
			{
				RecipeEditor editor = new RecipeEditor(exactRecipe); // for our recipe, make a new RecipeEditor
				editor.DeleteRecipe(); // delete the recipe
			}
			// The following is a more precise example, finding an exact recipe and deleting it if possible.
			
			}


			{
			RecipeFinder finder = new RecipeFinder();
			finder.AddIngredient(ItemID.JungleSpores, 8);
			finder.AddIngredient(ItemID.Vine, 2);
			finder.AddTile(TileID.Anvils); // add a required tile, any anvil
			finder.SetResult(ItemID.JunglePants); // set the result to be 10 chains
			Recipe exactRecipe = finder.FindExactRecipe();
			 // try to find the exact recipe matching our criteria

			bool isRecipeFound = exactRecipe != null; // if our recipe is not null, it means we found the exact recipe
			if (isRecipeFound) // since our recipe is found, we can continue
			{
				RecipeEditor editor = new RecipeEditor(exactRecipe); // for our recipe, make a new RecipeEditor
				editor.DeleteRecipe(); // delete the recipe
			}
			// The following is a more precise example, finding an exact recipe and deleting it if possible.
			
			}

		}*/
	}
}