using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftEquipmentItemCellView : CraftInventorySectionCellView
{
    public override void UpdateInfoElementsState(ItemState inventoryItem)
    {
        if (inventoryItem is EquipmentState)
        {
            var equipment = inventoryItem as EquipmentState;
            SetEnduranceValue((float)equipment.Endurance / equipment.MaxEndurance);
        }
        SetEnergyCount(inventoryItem);
    }

    public override void DisableInfoElements()
    {
        enduranceBar.gameObject.SetActive(false);
        energyCounterContainer.SetActive(false);
    }
}
