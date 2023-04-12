using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfo : ItemInfo
{
    private SimpleItemCellView attachedInventoryItemCellView;

    public override ItemState ItemState
    {
        get => itemState;
        set
        {
            itemState = value;
            inventoryItemIcon.sprite = itemState.Icon;
            if (attachedInventoryItemCellView != null)
            {
                itemState.CurrentInventoryItemCellView = attachedInventoryItemCellView;
            }            
        }
    }

    public override ItemCellView AttachedItemCellView 
    {
        set
        {
            if (ItemState != null)
            {
                ItemState.CurrentInventoryItemCellView = value as SimpleItemCellView;
                attachedInventoryItemCellView = value as SimpleItemCellView;
            }
        }
    }

    public override void UpdateItemCellViewState() => ItemState.CurrentInventoryItemCellView = attachedInventoryItemCellView;

    protected override void OnDestroy()
    {
        if (attachedInventoryItemCellView != null)
        {
            attachedInventoryItemCellView.DisableInfoElements();
        }
    }
}
