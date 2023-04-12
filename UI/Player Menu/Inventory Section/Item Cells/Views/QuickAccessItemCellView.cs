using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessItemCellView : PlayerSetItemCellView
{
    [Space]
    [SerializeField]
    private TMP_Text itemsCounter;

    public override void UpdateInfoElementsState(ItemState inventoryItem)
    {
        if (inventoryItem is StackableItemState)
        {
            SetItemsCount((inventoryItem as StackableItemState).ItemsCount);
        }
        if (inventoryItem is EquipmentState)
        {
            var equipment = inventoryItem as EquipmentState;
            SetEnduranceValue((float)equipment.Endurance / equipment.MaxEndurance);
        }
        Debug.Log("Данные обновлены");

        if (linkedItemCellView != null)
        {
            linkedItemCellView.UpdateInfoElementsState(inventoryItem);
        }
    }

    public override void DisableInfoElements()
    {
        itemsCounter.gameObject.SetActive(false);
        enduranceBar.gameObject.SetActive(false);

        if (linkedItemCellView != null)
        {
            linkedItemCellView.DisableInfoElements();
        }
    }

    private void SetItemsCount(int itemsCount)
    {
        itemsCounter.gameObject.SetActive(true);
        itemsCounter.text = "x" + itemsCount.ToString();
    }

    protected override void OnEnable()
    {
        enduranceFillBar = enduranceBar.transform.GetChild(0).GetComponent<Image>();
        if (GetComponent<ItemCellContainer>().IsItemPlaceEmpty)
        {
            DisableInfoElements();
        }
    }

}
