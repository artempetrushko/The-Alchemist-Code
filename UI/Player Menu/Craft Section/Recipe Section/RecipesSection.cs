using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipesSection : MonoBehaviour
{
    [SerializeField]
    private GameObject recipesCategoriesContainer;
    [SerializeField]
    private RecipesCategoryView recipesCategoryViewPrefab;

    public List<RecipesCategoryView> RecipeCategories => recipesCategoriesContainer.GetComponentsInChildren<RecipesCategoryView>().ToList();

    public void CreateRecipeCategories(List<RecipeData> recipes)
    {
        var recipesDictionary = recipes.GroupBy(recipe => recipe.LocalizedCategoryTitle).ToDictionary(group => group.Key, group => group.ToList());
        foreach (var recipesGroup in recipesDictionary)
        {
            var newCategoryView = Instantiate(recipesCategoryViewPrefab, recipesCategoriesContainer.transform);
            newCategoryView.SetInfo(recipesGroup.Key, recipesGroup.Value);
        }
    }

    public void DeleteRecipeCategories()
    {
        for (var i = recipesCategoriesContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(recipesCategoriesContainer.transform.GetChild(i).gameObject);
        }
    }
}
