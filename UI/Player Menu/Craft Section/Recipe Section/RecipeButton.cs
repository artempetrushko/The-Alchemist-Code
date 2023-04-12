using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text recipeTitle;
    [SerializeField]
    private TMP_Text recipeDescription;
    [SerializeField]
    private Image recipeResultItemImage;

    private CraftManager craftManager;
    private RecipeData assignedRecipe;

    public void SetInfo(RecipeData recipe)
    {
        assignedRecipe = recipe;
        recipeTitle.text = recipe.Title;
        recipeDescription.text = recipe.Description;
        recipeResultItemImage.sprite = recipe.RecipeIcon;
    }

    public void SendRecipeData()
    {
        craftManager.CurrentRecipe = assignedRecipe;
    }

    private void OnEnable()
    {
        craftManager = FindObjectOfType<CraftManager>();
    }
}
