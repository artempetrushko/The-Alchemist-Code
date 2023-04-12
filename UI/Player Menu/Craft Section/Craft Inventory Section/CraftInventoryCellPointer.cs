using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftInventoryCellPointer : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    private ItemCellView itemCellView;
    private ItemCreationSectionNavigation itemCreationSectionNavigation;

    public void OnPointerDown(PointerEventData eventData)
    {
        //itemCreationSectionNavigation.SelectInventoryItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<CraftInventorySubsection>().SelectItemCellByPointer(itemCellView);
    }

    private void OnEnable()
    {
        itemCellView = GetComponent<ItemCellView>();
        itemCreationSectionNavigation = GetComponentInParent<CraftSection>().GetComponentInChildren<ItemCreationSectionNavigation>();
    }
}
