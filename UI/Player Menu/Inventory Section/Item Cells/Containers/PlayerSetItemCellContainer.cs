using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class PlayerSetItemCellContainer : ItemCellContainer
{
    [Space]
    [SerializeField]
    protected InventorySubsection inventorySection;

    public void PlaceOrSwapItem(ItemInfo item)
    {
        if (itemPlace.transform.childCount > 0)
        {
            SwapItemCells_ItemDragging();
            return;
        }
        PlaceItem(item);
    }

    public void RemoveAttachedItem()
    {
        var freeInventoryCells = inventorySection.GetComponentsInChildren<SimpleItemCellContainer>().Where(container => container.IsItemPlaceEmpty).ToList();
        if (freeInventoryCells.Count > 0)
        {
            freeInventoryCells[0].PlaceItem(ContainedItem);
        }
    }
}
