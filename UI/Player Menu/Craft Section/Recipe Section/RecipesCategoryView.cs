using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipesCategoryView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text categoryTitle;
    [SerializeField]
    private Button showRecipesButton;
    [SerializeField]
    private GameObject recipeButtonsContainer;
    [SerializeField]
    private RecipeButton recipeButtonPrefab;

    private List<RecipeData> recipeDatas;

    public bool IsOpened => recipeButtonsContainer.activeInHierarchy;
    public List<RecipeButton> RecipeButtons => recipeButtonsContainer.GetComponentsInChildren<RecipeButton>().ToList();

    public void SetInfo(string title, List<RecipeData> recipes)
    {
        categoryTitle.text = title;
        recipeDatas = recipes;
    }

    public void Select() => showRecipesButton.Select();

    public void ToggleRecipesVisibility() => SetRecipesVisibility(!recipeButtonsContainer.activeInHierarchy);

    public void SetRecipesVisibility(bool isVisible)
    {
        if (isVisible) 
        {
            EnableRecipeButtons(recipeDatas);
        }
        else
        {
            DisableRecipeButtons();
        }
    }

    private void EnableRecipeButtons(List<RecipeData> recipes)
    {
        recipeButtonsContainer.SetActive(true);
        foreach (var recipe in recipes)
        {
            var recipeButton = Instantiate(recipeButtonPrefab, recipeButtonsContainer.transform);
            recipeButton.SetInfo(recipe);
        }
    }

    private void DisableRecipeButtons()
    {
        for (var i = recipeButtonsContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(recipeButtonsContainer.transform.GetChild(i).gameObject);
        }
        recipeButtonsContainer.SetActive(false);
    }
}
