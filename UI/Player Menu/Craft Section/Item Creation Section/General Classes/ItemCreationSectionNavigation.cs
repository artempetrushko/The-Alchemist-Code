using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemCreationSectionNavigation : MonoBehaviour
{
    [SerializeField]
    private CraftSection craftSection;
    [SerializeField]
    private GameObject creatingItemPlace;
    [SerializeField]
    private EnergyCellsSubsection energyCellsSubsection;
    [SerializeField]
    private CraftInventoryCategoriesNavigation craftInventoryCategoriesNavigation;

    private CraftSectionInventory craftSectionInventory;
    private CraftSectionNavigation sectionNavigation;
    private ItemCellView selectedCell;

    #region ֲûחמגû הכÿ New Input System
    public void ReturnToItemCreationSubsection(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ReturnToItemCreationSubsection();
        }
    }

    public void GoToCraftingTemplate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GoToCraftingItemTemplate();
        }
    }

    public void ReturnToEnergyCellsSubsection(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ReturnToEnergyCellsSubsection();
        }
    }

    public void SelectCraftingItemTemplateCell(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SelectCraftingItemTemplateCell();
        }
    }

    public void SelectEnergyCell(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SelectEnergyCell();
        }
    }

    public void SelectInventoryItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SelectInventoryItem();
        }
    }

    public void ReturnItemFromCraftingTemplateToInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ReturnItemFromCraftingTemplateToInventory();
        }
    }

    public void ReturnItemFromEnergyCellToInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ReturnItemFromEnergyCellToInventory();
        }
    }
    #endregion

    public void SelectCraftingItemTemplateCell()
    {
        var craftingItemTemplate = creatingItemPlace.GetComponentInChildren<CraftingItemTemplateSubsection>();
        selectedCell = craftingItemTemplate.CurrentSelectedCell;
        craftSection.HighlightCraftInventory();
        craftInventoryCategoriesNavigation.StartCurrentCategoryNavigation(InventoryItemSelectionMode.IngredientSelection);
    }

    public void SelectEnergyCell()
    {
        selectedCell = energyCellsSubsection.SelectedEnergyCell;
        craftSection.HighlightCraftInventory();
        craftInventoryCategoriesNavigation.StartCurrentCategoryNavigation(InventoryItemSelectionMode.IngredientSelection);
    }

    public void SelectInventoryItem()
    {
        var inventoryItem = (sectionNavigation.CurrentSubsection as CraftInventorySubsection).SelectedItemCell.GetComponent<ItemCellContainer>().ContainedItem;
        if (inventoryItem != null)
        {
            var previousInventoryItem = selectedCell.GetComponent<ItemCellContainer>().ContainedItem;
            if (previousInventoryItem != null)
            {
                inventoryItem.GetComponentInParent<ItemCellContainer>().PlaceItem(previousInventoryItem);
            }
            selectedCell.GetComponent<ItemCellContainer>().PlaceItem(inventoryItem);
            ReturnToItemCreationSubsection();
        }
    }

    public void ReturnToItemCreationSubsection()
    {
        switch (selectedCell)
        {
            case EnergyCellView:
                ReturnToEnergyCellsSubsection();
                return;

            case CraftingItemTemplateCellView:
                GoToCraftingItemTemplate(true);
                return;
        }
    }

    public void ReturnItemFromCraftingTemplateToInventory() => ReturnItemToInventory(creatingItemPlace.GetComponentInChildren<CraftingItemTemplateSubsection>().CurrentSelectedCell);

    public void ReturnItemFromEnergyCellToInventory() => ReturnItemToInventory(energyCellsSubsection.SelectedEnergyCell);

    private void GoToCraftingItemTemplate(bool isNavigationResume = false)
    {
        energyCellsSubsection.GetComponent<CanvasGroup>().alpha = 0.1f;
        creatingItemPlace.transform.SetAsLastSibling();
        craftSection.HighlightCraftingItemTemplate();

        var craftingItemTemplate = creatingItemPlace.GetComponentInChildren<CraftingItemTemplateSubsection>();
        if (craftingItemTemplate != null)
        {
            if (isNavigationResume)
            {
                craftingItemTemplate.ResumeNavigation();
            }
            else
            {
                craftingItemTemplate.StartNavigation();
            }
        }
    }

    private void ReturnToEnergyCellsSubsection()
    {
        energyCellsSubsection.GetComponent<CanvasGroup>().alpha = 1;
        energyCellsSubsection.transform.SetAsLastSibling();
        craftSection.DisableCraftingItemTemplateHighlight();
        energyCellsSubsection.ResumeNavigation();
    }

    private void ReturnItemToInventory(ItemCellView startCell)
    {
        var returningItem = startCell.GetComponent<ItemCellContainer>().ContainedItem;
        var destinationItemContainer = returningItem.ItemState.CurrentInventoryItemCellView.LinkedMechanicsItemCellView.GetComponent<ItemCellContainer>();
        if (destinationItemContainer.IsItemPlaceEmpty)
        {
            destinationItemContainer.PlaceItem(returningItem);
        }
        else
        {
            var freeCell = craftSectionInventory.GetComponentInChildren<CraftInventoryQuickAccessSubsection>().GetComponentsInChildren<ItemCellContainer>().Where(cell => cell.IsItemPlaceEmpty)
                .Concat(craftSectionInventory.GetComponentInChildren<CraftInventoryMainSubsection>().GetComponentsInChildren<ItemCellContainer>().Where(cell => cell.IsItemPlaceEmpty))
                .First();
            freeCell.PlaceItem(startCell.GetComponent<ItemCellContainer>().ContainedItem);
        }
    }

    private void OnEnable()
    {
        craftSectionInventory = FindObjectOfType<CraftSectionInventory>();
        sectionNavigation = GetComponentInParent<CraftSectionNavigation>();
    }
}
