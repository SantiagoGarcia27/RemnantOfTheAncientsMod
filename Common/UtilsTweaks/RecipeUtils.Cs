using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

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
            HashSet<Recipe> recipes = new();
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
    }
}