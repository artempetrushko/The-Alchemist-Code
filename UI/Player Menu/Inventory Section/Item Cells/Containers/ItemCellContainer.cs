using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ItemCellContainer : MonoBehaviour, IDropHandler
{
    [SerializeField]
    protected GameObject itemPlace;
    [SerializeField]
    protected ItemInfo inventoryItemPrefab;

    protected ItemCellView itemCellView;
    protected InventoryManager inventoryManager;

    public GameObject ItemPlace => itemPlace;
    public ItemInfo ContainedItem => itemPlace.GetComponentInChildren<ItemInfo>();
    public bool IsItemPlaceEmpty => ContainedItem == null;

    public abstract void OnDrop(PointerEventData eventData);

    public void PlaceItem(ItemInfo item)
    {
        item.transform.SetParent(ItemPlace.transform);
        AdjustItemInfoView(item);
        item.AttachedItemCellView = itemCellView;
        try
        {
            inventoryManager.UpdatePlayerSetItems();
        }
        catch
        {
            return;
        }       
    }

    public void InstantiateAndPlaceItem(ItemState itemState)
    {
        var itemCell = Instantiate(inventoryItemPrefab, ItemPlace.transform);
        AdjustItemInfoView(itemCell);
        itemCell.ItemState = itemState;
        itemCell.AttachedItemCellView = itemCellView;
        try
        {
            inventoryManager.UpdatePlayerSetItems();
        }
        catch
        {
            return;
        }
    }

    public void SwapAndPlaceItem(ItemCellContainer otherItemCellContainer)
    {
        var movingItem = otherItemCellContainer.ContainedItem;
        if (!IsItemPlaceEmpty)
        {
            movingItem.AttachedItemCellView = null;
            otherItemCellContainer.PlaceItem(ContainedItem);
        }
        PlaceItem(movingItem);
    }

    protected void SwapItemCells_ItemDragging()
    {
        var draggingItemStartContainer = InventoryItemDrag.DraggingItem.GetComponent<InventoryItemDrag>().StartParent.GetComponentInParent<ItemCellContainer>();
        draggingItemStartContainer.PlaceItem(ContainedItem);
        PlaceItem(InventoryItemDrag.DraggingItem.GetComponent<ItemInfo>());
    }

    protected virtual void OnEnable()
    {
        itemCellView = GetComponent<ItemCellView>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void AdjustItemInfoView(ItemInfo item)
    {
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;

        var itemPlaceSize = ItemPlace.GetComponent<RectTransform>().rect.size;
        if (itemPlaceSize.x != itemPlaceSize.y)
        {
            item.GetComponent<Image>().rectTransform.sizeDelta = item.TrySetBigIcon()
                ? itemPlaceSize
                : new Vector2(itemPlaceSize.x, itemPlaceSize.x);
        }
        else
        {
            item.GetComponent<Image>().rectTransform.sizeDelta = itemPlaceSize;
        }
    }
}
