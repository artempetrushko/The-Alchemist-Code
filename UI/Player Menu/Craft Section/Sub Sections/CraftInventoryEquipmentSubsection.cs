using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftInventoryEquipmentSubsection : CraftInventorySubsection
{
    private PlayerSetCellNavigation SelectedPlayerSetCell
    {
        get => SelectedItemCell.GetComponent<PlayerSetCellNavigation>();
        set => SelectedItemCell = value.GetComponent<CraftEquipmentItemCellView>();
    }

    public override void StartNavigation()
    {
        var startCellNumber = 3;
        switch (sectionNavigation.CurrentSubsection)
        {
            case CraftInventoryQuickAccessSubsection:
                startCellNumber = 6;
                break;

            case EnergyCellsSubsection:
                startCellNumber = 2;
                break;
        }
        sectionNavigation.CurrentSubsection = this;
        SelectedCellNumber = startCellNumber;
    }

    public override void ResumeNavigation()
    {
        throw new NotImplementedException();
    }

    public override void Navigate(InputAction.CallbackContext context)
    {
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

    public override void StopNavigation()
    {
        SelectedItemCell = null;
    }
}
