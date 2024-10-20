using System.Collections.Generic;
using Terraria;

namespace RemnantOfTheAncientsMod.Common.UtilsTweaks
{
    public static class RecipeUtils
    {

        public static List<int> GetUpgradeTree(int itemID, List<int> list)
        {
            HashSet<Recipe> recipes = GetRecipeChain(itemID);

            foreach (Recipe Items in recipes)
            {
                Utils1.AddSecure(list, Items.createItem.type);
            }
            return list;
        }

        private static HashSet<Recipe> GetRecipeChain(int itemType)
        {
            HashSet<Recipe> recipes = [];
            foreach (var recipe in Main.recipe)
            {
                if (!recipe.Disabled && recipe.HasIngredient(itemType))
                {
                    recipes.Add(recipe);
                    CollateRecipes(recipes, recipe.createItem.type);
                }
            }

            return recipes;
            static void CollateRecipes(ISet<Recipe> recipes, int itemType)
            {
                foreach (var recipe in Main.recipe)
                {
                    if (!recipe.Disabled && recipe.HasIngredient(itemType) && !recipes.Contains(recipe))
                    {
                        recipes.Add(recipe);
                        CollateRecipes(recipes, recipe.createItem.type);
                    }
                }
            }
        }
        public static Recipe SearchRecipe(int Id)
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(Id, out _))
                {
                    return recipe;
                }
            }
            return null;
        }
        public static List<int> SearchRecipeMaterials(int Id)
        {
            Recipe recipe = SearchRecipe(Id);
                List<int> results = [];
          
            foreach(Item item in recipe.requiredItem)
            {
                Utils1.AddSecure(results, item.type);
            }
            return results;
        }
        public static List<int> SearchRecipeTile(int Id)
        {
            Recipe recipe = SearchRecipe(Id);
            
            return recipe.requiredTile;
        }
        public static int SearchAmmountRecipe(int ResultID,int searchItemId)
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.TryGetResult(ResultID, out _))
                {
                    Item _searchItemId;
                    recipe.TryGetIngredient(searchItemId, out _searchItemId);
                    if (_searchItemId != null && _searchItemId.type == searchItemId)
                    {
                        return _searchItemId.stack;
                    }
                }
            }
            return 0;
        }

    }
}