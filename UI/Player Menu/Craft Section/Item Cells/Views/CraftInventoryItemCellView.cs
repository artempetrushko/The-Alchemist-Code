using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftInventoryItemCellView : CraftInventorySectionCellView
{
    [Space]
    [SerializeField]
    protected TMP_Text itemsCounter;

    public override void UpdateInfoElementsState(ItemState inventoryItem)
    {
        if (inventoryItem is StackableItemState)
        {
            SetItemsCount((inventoryItem as StackableItemState).ItemsCount);
        }
        else if (inventoryItem is EquipmentState)
        {
            var equipment = inventoryItem as EquipmentState;
            SetEnduranceValue((float)equipment.Endurance / equipment.MaxEndurance);
        }
        SetEnergyCount(inventoryItem);
    }  

    public override void DisableInfoElements()
    {
        itemsCounter.gameObject.SetActive(false);
        enduranceBar.gameObject.SetActive(false);
        energyCounterContainer.SetActive(false);
    }

    private void SetItemsCount(int itemsCount)
    {
        itemsCounter.gameObject.SetActive(true);
        itemsCounter.text = "x" + itemsCount.ToString();
    }
}
