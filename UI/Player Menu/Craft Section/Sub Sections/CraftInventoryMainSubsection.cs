using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CraftInventoryMainSubsection : CraftInventorySubsection
{
    private ScrollRect subsectionScrollRect;
    private int cellsGridConstraintCount;

    public override void StartNavigation() => StartNavigation(InventoryItemSelectionMode.Normal);

    public override void ResumeNavigation()
    {
        throw new System.NotImplementedException();
    }

    public override void Navigate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var inputValue = context.ReadValue<Vector2>();
            switch (inputValue.x)
            {
                case 1:
                    SelectedCellNumber += (int)inputValue.x;
                    break;
                case -1:
                    if ((SelectedCellNumber - 1) % cellsGridConstraintCount == 0)
                    {
                        LeftNeighboringSubsection.StartNavigation();
                    }
                    else goto case 1;
                    break;
            }
            if (inputValue.y == 1 || inputValue.y == -1)
            {
                SelectedCellNumber -= cellsGridConstraintCount * (int)inputValue.y;
            }
        }
    }

    public override void StopNavigation()
    {
        SelectedItemCell = null;
    }

    private new void StartNavigation(InventoryItemSelectionMode selectionMode)
    {
        CurrentSelectionMode = selectionMode;
        sectionNavigation.CurrentSubsection = this;
        SelectedCellNumber = 1;
        subsectionScrollRect.verticalNormalizedPosition = 1;
    }

    private void Awake()
    {
        subsectionScrollRect = GetComponentInParent<ScrollRect>();
        cellsGridConstraintCount = GetComponent<GridLayoutGroup>().constraintCount;
    }
}
