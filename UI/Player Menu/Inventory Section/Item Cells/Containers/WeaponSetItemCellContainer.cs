using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum WeaponCellContainerPosition
{
    Left,
    Right
}

public class WeaponSetItemCellContainer : PlayerSetItemCellContainer
{
    [Space]
    [SerializeField]
    private WeaponCellContainerPosition weaponCellPosition;

    public WeaponCellContainerPosition WeaponCellPosition => weaponCellPosition;

    public override void OnDrop(PointerEventData eventData)
    {
        var draggingItem = InventoryItemDrag.DraggingItem.GetComponent<ItemInfo>();
        if (draggingItem.ItemState is WeaponState)
        {
            PlaceOrSwapItem(draggingItem);
        }
    }
}
