using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftInventoryQuickAccessSubsection : CraftInventorySubsection
{
    public override void StartNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
        SelectedCellNumber = 3;
    }

    public override void ResumeNavigation()
    {
        throw new NotImplementedException();
    }

    public override void Navigate(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();
        if (inputValue.x == 1 || inputValue.x == -1)
        {
            if (SelectedCellNumber + inputValue.x > transform.childCount)
            {
                RightNeighboringSubsection.StartNavigation();
            }
            else
            {
                SelectedCellNumber = Mathf.Clamp(SelectedCellNumber + (int)inputValue.x, 1, transform.childCount);
            }
        }
        if (inputValue.y == 1 && !ItemCellsMoving.IsMovingStarted)
        {
            TopNeighboringSubsection.StartNavigation();
        }
    }

    public override void StopNavigation()
    {
        SelectedItemCell = null;
    }
}
