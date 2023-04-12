using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponEquipping : ItemsInteractionModule
{
    [SerializeField]
    private PlayerSetSubsection playerSetSubsection;

    private WeaponSetItemCellContainer[] weaponCellContainers;

    public override void StartInteraction(ItemCellContainer currentItemCell)
    {
        IsInteractionStarted = true;
        this.currentItemCell = currentItemCell;
        interactionProcessBackground.gameObject.SetActive(true);
        interactionProcessBackground.transform.SetAsLastSibling();
        playerSetSubsection.transform.SetAsLastSibling();
        playerSetSubsection.StartNavigation_WeaponEquipping();
        inputManager.SwitchActionMap_UI_Inventory_WeaponEquipping();
        SetWeaponCellsSibling(true);
    }

    public override void Execute()
    {
        playerSetSubsection.SelectedCell.GetComponent<ItemCellContainer>().SwapAndPlaceItem(currentItemCell);
        FinishInteraction();
    }

    protected override void FinishInteraction()
    {
        IsInteractionStarted = false;
        interactionProcessBackground.gameObject.SetActive(false);
        playerSetSubsection.ResumeNavigation();
        SetWeaponCellsSibling(false);
    }

    private void SetWeaponCellsSibling(bool isEquippingStarted)
    {
        if (isEquippingStarted)
        {
            weaponCellContainers[0].transform.SetAsLastSibling();
            weaponCellContainers[1].transform.SetAsLastSibling();
        }
        else
        {
            weaponCellContainers[0].transform.SetSiblingIndex(1);
            weaponCellContainers[1].transform.SetSiblingIndex(3);
        }
    }

    protected override void Start()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
        weaponCellContainers = playerSetSubsection.GetComponentsInChildren<WeaponSetItemCellContainer>();
    } 
}
