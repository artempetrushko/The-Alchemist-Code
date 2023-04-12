using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsConcatination : ItemsInteractionModule
{
    [SerializeField]
    private InventorySubsection inventorySubsection;
    [SerializeField]
    private QuickAccessToolbarSubsection quickAccessSubsection;
    [SerializeField]
    private InventorySectionNavigation sectionNavigation;

    private InventoryManager inventoryManager;

    public override void StartInteraction(ItemCellContainer currentItemCell)
    {
        IsInteractionStarted = true;
        this.currentItemCell = currentItemCell;
        interactionProcessBackground.gameObject.SetActive(true);
        interactionProcessBackground.transform.SetAsLastSibling();
        quickAccessSubsection.transform.SetAsLastSibling();
        inventorySubsection.GetComponentInParent<ScrollRect>().transform.SetAsLastSibling();
        switch (currentItemCell.GetComponentInParent<InventorySectionSubsection>())
        {
            case InventorySubsection:
                inventorySubsection.SelectedCell = currentItemCell.GetComponent<SimpleItemCellView>();
                break;

            case QuickAccessToolbarSubsection:
                quickAccessSubsection.SelectedCell = currentItemCell.GetComponent<SimpleItemCellView>();
                break;
        }
        inputManager.SwitchActionMap_UI_Inventory_ItemsConcatination();
    }

    public override void Execute()
    {
        var selectedCell = (sectionNavigation.CurrentSubsection as InventorySectionSubsection).SelectedCell.GetComponent<ItemCellContainer>();
        if (!selectedCell.IsItemPlaceEmpty)
        {
            var startCellItem = currentItemCell.ContainedItem.ItemState as StackableItemState;
            var selectedCellItem = selectedCell.ContainedItem.ItemState as StackableItemState;
            if (startCellItem != selectedCellItem && ItemState.Compare(startCellItem, selectedCellItem))
            {
                if (startCellItem.ItemsCount + selectedCellItem.ItemsCount > selectedCellItem.MaxStackItemsCount)
                {
                    startCellItem.ItemsCount = startCellItem.ItemsCount + selectedCellItem.ItemsCount - selectedCellItem.MaxStackItemsCount;
                    selectedCellItem.ItemsCount = selectedCellItem.MaxStackItemsCount;                    
                }
                else
                {
                    selectedCellItem.ItemsCount += startCellItem.ItemsCount;
                    inventoryManager.RemoveItemState(startCellItem);
                }
                FinishInteraction();
            }
        }
    }

    protected override void FinishInteraction()
    {
        IsInteractionStarted = false;
        interactionProcessBackground.gameObject.SetActive(false);
        inventorySubsection.GetComponentInParent<ScrollRect>().transform.SetAsFirstSibling();
        inventorySubsection.ResumeNavigation();
    }

    protected override void Start()
    {
        base.Start();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
}
