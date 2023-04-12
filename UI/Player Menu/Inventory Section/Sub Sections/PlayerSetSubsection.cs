using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponEquipping))]
public class PlayerSetSubsection : InventorySectionSubsection
{
    private WeaponEquipping weaponEquippingModule;

    private PlayerSetCellNavigation SelectedPlayerSetCell
    {
        get => SelectedCell.GetComponent<PlayerSetCellNavigation>();
        set => SelectedCell = value.GetComponent<PlayerSetItemCellView>();
    }
    private WeaponEquippingNavigation SelectedWeaponCell
    {
        get => SelectedCell.GetComponent<WeaponEquippingNavigation>();
        set => SelectedCell = value.GetComponent<EquipmentItemCellView>();
    }

    public override void StartNavigation()
    {
        var startCellNumber = 3;
        switch (sectionNavigation.CurrentSubsection)
        {
            case QuickAccessToolbarSubsection:
                startCellNumber = 6;
                break;

            case InventorySubsection:
                startCellNumber = 4;
                break;          
        }
        sectionNavigation.CurrentSubsection = this;
        SelectedCellNumber = startCellNumber;
    }

    public void StartNavigation_WeaponEquipping()
    {
        sectionNavigation.CurrentSubsection = this;
        SelectedCellNumber = 2;
    }

    public override void ResumeNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
    }

    public override void Navigate(InputAction.CallbackContext context)
    {
        if (weaponEquippingModule.IsInteractionStarted)
        {
            Navigate_WeaponEquipping(context);
            return;
        }
        var inputValue = context.ReadValue<Vector2>();
        switch (inputValue.x)
        {
            case 1:
                if (SelectedPlayerSetCell.RightNeighboringPlayerSetCell == null)
                {
                    RightNeighboringSubsection.StartNavigation();
                }
                else
                {
                    SelectedPlayerSetCell = SelectedPlayerSetCell.RightNeighboringPlayerSetCell;
                }
                break;

            case -1:
                if (SelectedPlayerSetCell.LeftNeighboringPlayerSetCell != null)
                {
                    SelectedPlayerSetCell = SelectedPlayerSetCell.LeftNeighboringPlayerSetCell;
                }
                break;
        }
        switch (inputValue.y)
        {
            case 1:
                if (SelectedPlayerSetCell.TopNeighboringPlayerSetCell != null)
                {
                    SelectedPlayerSetCell = SelectedPlayerSetCell.TopNeighboringPlayerSetCell;
                }
                break;
            case -1:
                if (SelectedPlayerSetCell.BottomNeighboringPlayerSetCell == null)
                {
                    BottomNeighboringSubsection.StartNavigation();
                }
                else
                {
                    SelectedPlayerSetCell = SelectedPlayerSetCell.BottomNeighboringPlayerSetCell;
                }
                break;
        }
    }

    private void Navigate_WeaponEquipping(InputAction.CallbackContext context)
    {
        var inputValueX = context.ReadValue<Vector2>().x;
        switch (inputValueX)
        {
            case 1:
                if (SelectedWeaponCell.RightWeaponCell != null)
                {
                    SelectedWeaponCell = SelectedWeaponCell.RightWeaponCell;
                }
                break;

            case -1:
                if (SelectedWeaponCell.LeftWeaponCell != null)
                {
                    SelectedWeaponCell = SelectedWeaponCell.LeftWeaponCell;
                }
                break;
        }
    }

    public override void StopNavigation()
    {
        SelectedCell = null;
    }

    private void Start()
    {
        weaponEquippingModule = GetComponent<WeaponEquipping>();
    }
}
