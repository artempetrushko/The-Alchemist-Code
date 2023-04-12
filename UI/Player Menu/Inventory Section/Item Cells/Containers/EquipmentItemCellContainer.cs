using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentItemCellContainer : PlayerSetItemCellContainer
{
    [Space]
    [SerializeField]
    protected ClothesType requiredClothesType;

    public ClothesType RequiredClothesType => requiredClothesType;

    public override void OnDrop(PointerEventData eventData)
    {
        var draggingItem = InventoryItemDrag.DraggingItem.GetComponent<ItemInfo>();
        if (draggingItem.ItemState is ClothesState && (draggingItem.ItemState as ClothesState).ClothesType == requiredClothesType)
        {
            PlaceOrSwapItem(draggingItem);
        }
    }
}
