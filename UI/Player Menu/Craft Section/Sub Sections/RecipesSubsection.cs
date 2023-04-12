using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RecipesSubsection : CraftSectionSubsection
{
    private RecipesSection recipesSection;
    private List<RecipesCategoryView> categoryViews;
    private List<RecipeButton> recipeButtons;
    private GameObject lastSelectedSectionItem;
    private int selectedRecipeCategoryNumber;
    private int selectedRecipeNumber;

    private int SelectedRecipeCategoryNumber
    {
        get => selectedRecipeCategoryNumber;
        set
        {
            selectedRecipeCategoryNumber = Mathf.Clamp(value, 0, categoryViews.Count - 1);
            categoryViews[selectedRecipeCategoryNumber].Select();
            lastSelectedSectionItem = categoryViews[selectedRecipeCategoryNumber].gameObject;
        }
    }

    private int SelectedRecipeNumber
    {
        get => selectedRecipeNumber;
        set
        {
            selectedRecipeNumber = Mathf.Clamp(value, 0, recipeButtons.Count - 1);
            recipeButtons[selectedRecipeNumber].GetComponent<Button>().Select();
            lastSelectedSectionItem = recipeButtons[selectedRecipeNumber].gameObject;
        }
    }

    public override void StartNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
        categoryViews = recipesSection.RecipeCategories;
        SelectedRecipeCategoryNumber = 0;
    }

    public override void ResumeNavigation()
    {
        throw new System.NotImplementedException();
    }

    public override void Navigate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var inputValue = context.ReadValue<Vector2>();
            ChangeRecipeOrRecipeCategory(inputValue.y);
            if (inputValue.x == 1)
            {
                RightNeighboringSubsection.StartNavigation();
            }
        }
    }

    public override void StopNavigation()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PressRecipeOrRecipeCategory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var currentSelectedObject = EventSystem.current.currentSelectedGameObject;
            if (currentSelectedObject.transform.parent.GetComponent<RecipesCategoryView>() != null)
            {
                categoryViews[SelectedRecipeCategoryNumber].ToggleRecipesVisibility();
                if (categoryViews[SelectedRecipeCategoryNumber].IsOpened)
                {
                    recipeButtons = categoryViews[SelectedRecipeCategoryNumber].RecipeButtons;
                    SelectedRecipeNumber = 0;
                }
            }
            else if (currentSelectedObject.GetComponent<RecipeButton>() != null)
            {
                currentSelectedObject.GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    private void ChangeRecipeOrRecipeCategory(float offset)
    {
        if (lastSelectedSectionItem.GetComponent<RecipesCategoryView>() != null)
        {
            switch (offset)
            {
                case 1:
                    if (SelectedRecipeCategoryNumber > 0)
                    {
                        SelectedRecipeCategoryNumber--;
                        if (categoryViews[SelectedRecipeCategoryNumber - 1].IsOpened)
                        {
                            recipeButtons = lastSelectedSectionItem.GetComponent<RecipesCategoryView>().RecipeButtons;
                            SelectedRecipeNumber = recipeButtons.Count - 1;
                        }
                    }
                    break;
                case -1:
                    if (lastSelectedSectionItem.GetComponent<RecipesCategoryView>().IsOpened)
                    {
                        recipeButtons = lastSelectedSectionItem.GetComponent<RecipesCategoryView>().RecipeButtons;
                        SelectedRecipeNumber = 0;
                    }
                    else
                    {
                        SelectedRecipeCategoryNumber++;
                    }
                    break;

            }

        }
        else if (lastSelectedSectionItem.GetComponent<RecipeButton>() != null)
        {
            if (SelectedRecipeNumber - offset < 0)
            {
                SelectedRecipeCategoryNumber = SelectedRecipeCategoryNumber;
            }
            else if (SelectedRecipeNumber - offset > recipeButtons.Count - 1)
            {
                SelectedRecipeCategoryNumber++;
            }
            else
            {
                SelectedRecipeNumber -= (int)offset;
            }
        }
    }

    private void OnEnable()
    {
        recipesSection = GetComponent<RecipesSection>();
    } 
}
