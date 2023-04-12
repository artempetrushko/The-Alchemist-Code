using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableItemState : ItemState
{
    private int itemsCount;

    public int ItemsCount
    {
        get => itemsCount;
        set
        {
            itemsCount = value;
            if (CurrentInventoryItemCellView != null)
            {
                CurrentInventoryItemCellView.UpdateInfoElementsState(this);
            }
            if (CurrentMechanicsItemCellView != null)
            {
                CurrentMechanicsItemCellView.UpdateInfoElementsState(this);
            }
        }
    }
    public int MaxStackItemsCount { get; set; }
    public int TotalContainedEnergyCount => ContainedEnergyCount * ItemsCount;

    public StackableItemState(StackableItemData item, int itemsCount = 0) : base(item)
    {
        ItemsCount = itemsCount > 0 ? itemsCount : item.BaseCount;
        MaxStackItemsCount = item.StackMaxCount;
    }

    protected StackableItemState() { }
}
