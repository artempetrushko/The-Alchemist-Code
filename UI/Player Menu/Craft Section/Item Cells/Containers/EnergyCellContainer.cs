using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnergyCellContainer : ItemCellContainer
{
    public override void OnDrop(PointerEventData eventData)
    {
        var draggingItem = InventoryItemDrag.DraggingItem.GetComponent<MechanicsItemInfo>();
        if (!IsItemPlaceEmpty)
        {
            if (draggingItem.ItemState is StackableItemState)
            {
                if (ItemState.Compare(draggingItem.ItemState, ContainedItem.ItemState))
                {
                    var containedStackableItem = ContainedItem.ItemState as StackableItemState;
                    if (containedStackableItem.ItemsCount < containedStackableItem.MaxStackItemsCount)
                    {
                        containedStackableItem.ItemsCount += (draggingItem.ItemState as StackableItemState).ItemsCount;
                        if (containedStackableItem.ItemsCount <= containedStackableItem.MaxStackItemsCount)
                        {
                            inventoryManager.RemoveItemState(draggingItem.ItemState);
                            Destroy(draggingItem.gameObject);
                            return;
                        }
                        else
                        {
                            (draggingItem.ItemState as StackableItemState).ItemsCount = containedStackableItem.ItemsCount - containedStackableItem.MaxStackItemsCount;
                            containedStackableItem.ItemsCount = containedStackableItem.MaxStackItemsCount;
                        }
                    }
                }
                else
                {
                    SwapItemCells_ItemDragging();
                    return;
                }
            }
            else
            {
                SwapItemCells_ItemDragging();
                return;
            }
        }
        PlaceItem(draggingItem);
    }
}
