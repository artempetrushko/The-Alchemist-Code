using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : PlayerMenuSection
{
    [SerializeField]
    private InventorySubsection inventoryCells;
    [SerializeField]
    private GameObject playerSetItemCells;
    [SerializeField]
    private QuickAccessToolbar quickAccessToolbar;
    [SerializeField]
    private GameObject emptyItemCellPrefab;

    private InventorySectionNavigation inventorySectionNavigation;

    public GameObject InventoryCells => inventoryCells.gameObject;
    public GameObject PlayerSetItemCells => playerSetItemCells;
    public QuickAccessToolbar QuickAccessToolbar => quickAccessToolbar;
    public List<PlayerSetItemCellContainer> PlayerSetItemCellContainers => playerSetItemCells.GetComponentsInChildren<PlayerSetItemCellContainer>().ToList();

    public override void SetVisibility(bool isVisible)
    {
        gameObject.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
        if (isVisible)
        {
            inventoryCells.StartNavigation();
            inventoryCells.GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 1;
        }
        else
        {
            inventorySectionNavigation.CurrentSubsection.StopNavigation();
        }
    }
    
    public void RefreshInventoryContent(ItemState itemState)
    {
        if (itemState.CurrentInventoryItemCellView != null)
        {
            return;
        }
        if (TryEquipItem(itemState))
        {
            return;
        }
        inventoryCells.GetComponentsInChildren<ItemCellContainer>().Where(cell => cell.IsItemPlaceEmpty).First().InstantiateAndPlaceItem(itemState);
    }

    public void ClearDeletedCell(ItemState attachedItemState)
    {
        if (attachedItemState.CurrentInventoryItemCellView != null)
        {
            Destroy(attachedItemState.CurrentInventoryItemCellView.GetComponent<ItemCellContainer>().ContainedItem.gameObject);
            attachedItemState.CurrentInventoryItemCellView.GetComponent<ItemCellContainer>().ItemPlace.transform.DetachChildren();
        }
    }

    public void FillWithEmptyCells(int cellsCount)
    {
        for (var i = 0; i < cellsCount; i++)
        {
            Instantiate(emptyItemCellPrefab, inventoryCells.transform);
        }
    }

    private bool TryEquipItem(ItemState item)
    {
        switch (item)
        {
            case EquipmentState:
                switch (item as EquipmentState)
                {
                    case WeaponState:
                        if (TryFindFreeCellAndPlaceItem(item, playerSetItemCells.GetComponentsInChildren<WeaponSetItemCellContainer>()
                                .Where(container => container.IsItemPlaceEmpty)
                                .Reverse()
                                .ToList()))
                        {
                            return true;
                        }
                        break;

                    case ClothesState:
                        if (TryFindFreeCellAndPlaceItem(item, playerSetItemCells.GetComponentsInChildren<EquipmentItemCellContainer>()
                                .Where(container => container.RequiredClothesType == (item as ClothesState).ClothesType && container.IsItemPlaceEmpty)
                                .ToList()))
                        {
                            return true;
                        }
                        break;
                }
                break;

            default:
                var existingInventoryItem = quickAccessToolbar.GetComponentsInChildren<SimpleItemCellContainer>()
                    .Where(container =>
                    {
                        var containingItem = container.ContainedItem;
                        if (containingItem != null)
                        {
                            return containingItem.ItemState == item;
                        }
                        return false;
                    })
                    .FirstOrDefault();
                if (existingInventoryItem != null)
                {
                    existingInventoryItem.ContainedItem.ItemState = item;
                    return true;
                }
                else
                {
                    if (TryFindFreeCellAndPlaceItem(item, quickAccessToolbar.GetComponentsInChildren<SimpleItemCellContainer>()
                            .Where(container => container.IsItemPlaceEmpty)
                            .ToList()))
                    {
                        return true;
                    }
                }      
                break;
        }
        return false;
    }

    private bool TryFindFreeCellAndPlaceItem<T>(ItemState item, List<T> possibleInventoryCells) where T : ItemCellContainer
    {
        if (possibleInventoryCells.Count() > 0)
        {
            possibleInventoryCells[0].InstantiateAndPlaceItem(item);
            return true;
        }
        return false;
    }

    private void Start()
    {
        inventorySectionNavigation = GetComponent<InventorySectionNavigation>();
        SetVisibility(false);
    }
}
