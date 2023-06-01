using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    [SerializeField]
    private List<RecipeData> availableCraftRecipes = new List<RecipeData>();

    private InventoryManager inventoryManager;
    private GameManager gameManager;
    private CraftSection craftSection;
    private RecipeData currentRecipe;
    private RecipeVariant currentRecipeVariant;
    private int? currentExtractedEnergyCount;

    private List<CraftingItemTemplateCellView> currentCraftingTemplateCells;
    private bool isCreationAvailable = false;
    private bool isAllIngredientsPlaced = false;

    public List<RecipeData> AvailableCraftRecipes => availableCraftRecipes;
    public List<CraftingItemTemplateCellView> CurrentCraftingItemCells => currentCraftingTemplateCells;
    public List<ItemInfo> EnergyContainingItems => craftSection.ItemCreationSection.EnergyCellsItems;
    public RecipeData CurrentRecipe
    {
        get => currentRecipe;
        set
        {
            if (currentRecipe != value)
            {
                currentRecipe = value;
                if (currentRecipe != null)
                {
                    craftSection.ItemCreationSection.ShowCraftTemplate(currentRecipe);
                    CurrentExtractedEnergyCount = 0;
                }               
                else
                {
                    craftSection.ItemCreationSection.DeleteCraftTemplate();
                    currentCraftingTemplateCells = null;
                    CurrentExtractedEnergyCount = null;
                }                
            }           
        }
    }
    public bool IsCreationAvailable
    {
        get => isCreationAvailable;
        private set
        {
            isCreationAvailable = value;
            craftSection.ItemCreationSection.GetComponent<ItemCreationSectionPointer>().enabled = isCreationAvailable;
            craftSection.ItemCreationSection.CraftDescriptionPanel.SetCreationAvailabilityState(isCreationAvailable 
                ? CreationAvailabilityState.Available 
                : !IsAllIngredientsPlaced 
                    ? CreationAvailabilityState.NotEnoughItems 
                    : CreationAvailabilityState.NotEnoughEnergy);
        }
    }
    private bool IsAllIngredientsPlaced
    {
        get => isAllIngredientsPlaced;
        set
        {
            if (isAllIngredientsPlaced != value)
            {
                isAllIngredientsPlaced = value;
                IsCreationAvailable = isAllIngredientsPlaced && CurrentExtractedEnergyCount >= CurrentRecipe.RequiredEnergyCount;
            }
        }
    }
    private int? CurrentExtractedEnergyCount
    {
        get => currentExtractedEnergyCount;
        set
        {
            currentExtractedEnergyCount = value;
            if (currentExtractedEnergyCount != null)
            {
                craftSection.ItemCreationSection.CraftDescriptionPanel.SetExtractedEnergyCountInfo(CurrentExtractedEnergyCount.Value, currentRecipe.RequiredEnergyCount);
                IsCreationAvailable = IsAllIngredientsPlaced && currentExtractedEnergyCount >= CurrentRecipe.RequiredEnergyCount;
            }
        }
    }

    public void CreateNewItem()
    {
        inventoryManager.AddNewItemState(currentRecipeVariant.ResultItem.GetResultItemState());
        UpdateIngredientsStates();
        UpdateEnergyContainingItemsStates();
        craftSection.InventorySection.UpdateInventoryContent();
        gameManager.CountSpecialItems();
    }

    public void UpdateRequiredItemsProgress(List<CraftingItemTemplateCellView> itemCells)
    {
        currentCraftingTemplateCells = itemCells;

        var containedItems = currentCraftingTemplateCells.Select(cell => cell.GetComponent<CraftingItemTemplateCellContainer>().ContainedItem).ToList();
        if (containedItems.Where(item => item != null).Count() == 0)
        {
            foreach (var cell in currentCraftingTemplateCells)
            {
                cell.ItemsCounterView.SetInfo(0, null);
            }
            return;
        }

        var containedItemStates = containedItems.Select(item => item != null ? item.ItemState : null).ToList();
        currentRecipeVariant = currentRecipe.TryGetMatchingRecipeVariant(containedItemStates);
        SetTemplateCellViewCounterInfos(containedItems);

        if (currentRecipeVariant != null)
        {
            IsAllIngredientsPlaced = currentRecipeVariant.CheckCraftingAvailability(containedItemStates);
        }
    }

    public void GetEnergyCellsData()
    {
        var itemStates = EnergyContainingItems.Select(cell => cell.ItemState).ToList();  
        CurrentExtractedEnergyCount = itemStates
            .Select(item => item switch
            {
                StackableItemState => (item as StackableItemState).TotalContainedEnergyCount,
                _ => item.ContainedEnergyCount
            })
            .Sum();
    }

    private void SetTemplateCellViewCounterInfos(List<ItemInfo> containedItems)
    {
        for (var i = 0; i < containedItems.Count; i++)
        {
            var currentItemsCount = containedItems[i] switch
            {
                null => 0,
                _ => containedItems[i].ItemState switch
                {
                    StackableItemState => (containedItems[i].ItemState as StackableItemState).ItemsCount,
                    _ => 1
                }
            };
            int? requiredItemsCount = currentRecipeVariant != null ? currentRecipeVariant.Ingredients[i].Count : null;
            currentCraftingTemplateCells[i].ItemsCounterView.SetInfo(currentItemsCount, requiredItemsCount);
        }
    }

    private void UpdateIngredientsStates()
    {
        for (var i = 0; i < currentCraftingTemplateCells.Count; i++)
        {
            var usedItem = currentCraftingTemplateCells[i].GetComponent<ItemCellContainer>().ContainedItem;
            switch (usedItem.ItemState)
            {
                case StackableItemState:
                    var spentItemsCount = currentRecipeVariant.Ingredients[i].Count;
                    if ((usedItem.ItemState as StackableItemState).ItemsCount == spentItemsCount)
                    {
                        RemoveCraftUsedItem(usedItem);
                    }
                    else
                    {
                        (usedItem.ItemState as StackableItemState).ItemsCount -= spentItemsCount;
                    }
                    break;

                case SingleItemState:
                    RemoveCraftUsedItem(usedItem);
                    break; 
            }
        }
        UpdateRequiredItemsProgress(currentCraftingTemplateCells);
    }

    private void UpdateEnergyContainingItemsStates()
    {
        var requiredEnergyCount = currentRecipe.RequiredEnergyCount;
        foreach (var item in EnergyContainingItems)
        {
            switch (item.ItemState)
            {
                case StackableItemState:
                    if ((item.ItemState as StackableItemState).TotalContainedEnergyCount <= requiredEnergyCount)
                    {
                        requiredEnergyCount -= (item.ItemState as StackableItemState).TotalContainedEnergyCount;
                        RemoveCraftUsedItem(item);
                    }
                    else
                    {
                        (item.ItemState as StackableItemState).ItemsCount -= (int)Mathf.Ceil((float)requiredEnergyCount / (item.ItemState as StackableItemState).ContainedEnergyCount);
                        if ((item.ItemState as StackableItemState).ItemsCount == 0)
                        {
                            RemoveCraftUsedItem(item);
                        }
                        requiredEnergyCount = 0;                        
                    }
                    break;

                case SingleItemState:
                    requiredEnergyCount -= item.ItemState.ContainedEnergyCount;
                    RemoveCraftUsedItem(item);
                    break;
            }
            if (requiredEnergyCount <= 0)
            {
                break;
            }
        }
        GetEnergyCellsData();
    }

    private void RemoveCraftUsedItem(ItemInfo itemCell)
    {
        inventoryManager.RemoveItemState(itemCell.ItemState);
        itemCell.transform.parent.DetachChildren();
        Destroy(itemCell.gameObject);       
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        inventoryManager = GetComponent<InventoryManager>();
        craftSection = FindObjectOfType<CraftSection>();
    }
}
