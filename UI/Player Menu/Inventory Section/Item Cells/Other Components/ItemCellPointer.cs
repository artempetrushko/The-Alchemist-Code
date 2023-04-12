using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCellPointer : MonoBehaviour, IPointerEnterHandler
{
    private SimpleItemCellView itemCellView;
    private InventorySectionSubsection parentSubsection;

    public void OnPointerEnter(PointerEventData eventData)
    {
        parentSubsection.SelectedCell = itemCellView;  
    }

    private void OnEnable()
    {
        itemCellView = GetComponent<SimpleItemCellView>();
        parentSubsection = GetComponentInParent<InventorySectionSubsection>();
    }
}
