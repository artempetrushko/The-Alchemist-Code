using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechanicsItemInfo : ItemInfo
{
    private MechanicsItemCellView attachedMechanicsItemCellView;

    public override ItemState ItemState
    {
        get => itemState;
        set
        {
            itemState = value;
            inventoryItemIcon.sprite = itemState.Icon;
            if (attachedMechanicsItemCellView != null)
            {
                itemState.CurrentMechanicsItemCellView = attachedMechanicsItemCellView;
            }
        }
    }

    public override ItemCellView AttachedItemCellView
    {
        set
        {
            if (ItemState != null)
            {
                attachedMechanicsItemCellView = value as MechanicsItemCellView;
                ItemState.CurrentMechanicsItemCellView = value as MechanicsItemCellView;
            }
        }
    }

    public override void UpdateItemCellViewState() => ItemState.CurrentMechanicsItemCellView = attachedMechanicsItemCellView;

    protected override void OnDestroy()
    {
        if (attachedMechanicsItemCellView != null)
        {
            attachedMechanicsItemCellView.DisableInfoElements();
        }
    }
}
