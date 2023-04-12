using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemCellView : PlayerSetItemCellView
{
    [Space]
    [SerializeField]
    private Button removeItemButton;

    private WeaponEquipping weaponEquippingModule;

    public override void UpdateInfoElementsState(ItemState inventoryItem)
    {
        var equipment = inventoryItem as EquipmentState;
        SetEnduranceValue((float)equipment.Endurance / equipment.MaxEndurance);
        Debug.Log("Данные обновлены");

        if (linkedItemCellView != null)
        {
            linkedItemCellView.UpdateInfoElementsState(inventoryItem);
        }
    }

    public override void DisableInfoElements()
    {
        enduranceBar.gameObject.SetActive(false);
        ToggleRemoveItemButtonState(false);

        if (linkedItemCellView != null)
        {
            linkedItemCellView.DisableInfoElements();
        }
    }

    public void ToggleRemoveItemButtonState(bool isEnable)
    {
        if (!weaponEquippingModule.IsInteractionStarted)
        {
            removeItemButton.gameObject.SetActive(isEnable);
        }       
    }

    protected override void OnEnable()
    {
        weaponEquippingModule = GetComponentInParent<WeaponEquipping>();
        enduranceFillBar = enduranceBar.transform.GetChild(0).GetComponent<Image>();
        if (GetComponent<ItemCellContainer>().IsItemPlaceEmpty)
        {
            DisableInfoElements();
        }
    }
}
