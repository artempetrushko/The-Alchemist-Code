using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemsConcatinationPointer : MonoBehaviour, IPointerDownHandler
{
    private ItemsConcatination itemsConcatinationModule;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (itemsConcatinationModule.IsInteractionStarted)
        {
            itemsConcatinationModule.Execute();
        }
    }

    private void OnEnable()
    {
        var parentSubsection = GetComponentInParent<InventorySectionSubsection>();
        itemsConcatinationModule = parentSubsection switch
        {
            InventorySubsection => GetComponentInParent<ItemsConcatination>(),
            QuickAccessToolbarSubsection => GetComponentInParent<InventoryView>().GetComponentInChildren<ItemsConcatination>()
        };
    }
}
