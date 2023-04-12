using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftSection : PlayerMenuSection
{
    [SerializeField]
    private ItemCreationSection itemCreationSection;
    [SerializeField]
    private CraftSectionInventory inventorySection;
    [SerializeField]
    private RecipesSection recipesSection;
    [SerializeField]
    private GameObject background;

    private CraftManager craftManager;

    public ItemCreationSection ItemCreationSection => itemCreationSection;
    public CraftSectionInventory InventorySection => inventorySection;
    public CraftDescriptionPanel CraftDescriptionPanel => itemCreationSection.CraftDescriptionPanel;

    public override void SetVisibility(bool isVisible)
    {
        transform.localScale = isVisible ? Vector3.one : Vector3.zero;
        if (isVisible)
        {
            inventorySection.UpdateInventoryContent();
            inventorySection.InventoryScrollView.verticalNormalizedPosition = 1;
            inventorySection.GetComponent<CraftInventoryCategoriesNavigation>().ShowDefaultInventorySubsection();
            recipesSection.CreateRecipeCategories(craftManager.AvailableCraftRecipes);
            recipesSection.GetComponent<RecipesSubsection>().StartNavigation();
            CraftDescriptionPanel.SetDefaultInfo();
        }
        else
        {
            var currentSelectedInventoryCell = inventorySection.GetComponentInChildren<CraftInventorySubsection>().SelectedItemCell;
            if (currentSelectedInventoryCell != null )
            {
                currentSelectedInventoryCell.GetComponent<CraftInventoryItemCellView>().DisableDescriptionPanel();
            }
            recipesSection.DeleteRecipeCategories();
            craftManager.CurrentRecipe = null;
        }
    }

    public void HighlightCraftingItemTemplate()
    {
        ActivateSectionBackground();
        itemCreationSection.transform.SetAsLastSibling();
    }

    public void HighlightCraftInventory()
    {
        ActivateSectionBackground();
        inventorySection.transform.SetAsLastSibling();
    }

    public void DisableCraftingItemTemplateHighlight()
    {
        background.SetActive(false);
        inventorySection.transform.SetAsLastSibling();
    }

    private void ActivateSectionBackground()
    {
        background.SetActive(true);
        background.transform.SetAsLastSibling();
    }

    private void Awake()
    {
        craftManager = FindObjectOfType<CraftManager>();
        SetVisibility(false);
    }
}
