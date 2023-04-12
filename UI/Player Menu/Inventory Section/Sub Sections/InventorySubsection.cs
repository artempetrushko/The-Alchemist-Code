using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(ItemsConcatination))]
public class InventorySubsection : InventorySectionSubsection
{
    private ItemsConcatination itemsConcatinationModule;
    private ItemsConcatinationNavigation itemsConcatinationNavigation;
    private int cellsGridConstraintCount;

    public override void StartNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
        SelectedCellNumber = 1;
    }

    public override void ResumeNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
    }

    public override void Navigate(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();
        switch (inputValue.x)
        {
            case 1:
                if (SelectedCellNumber + inputValue.x <= transform.childCount)
                {
                    SelectedCellNumber += (int)inputValue.x;
                }
                break;
            case -1:
                if ((SelectedCellNumber - 1) % cellsGridConstraintCount == 0)
                {
                    if (itemsConcatinationModule.IsInteractionStarted)
                    {
                        itemsConcatinationNavigation.LeftSubsection.StartNavigation();
                        return;
                    }
                    else
                    {
                        LeftNeighboringSubsection.StartNavigation();
                    }                   
                }
                else if (SelectedCellNumber + inputValue.x > 0)
                {
                    SelectedCellNumber += (int)inputValue.x;
                }
                break;
        }
        switch (inputValue.y)
        {
            case 1:
                if (SelectedCellNumber - cellsGridConstraintCount > 0)
                {
                    SelectedCellNumber -= cellsGridConstraintCount;
                }
                break;
            case -1:
                if (SelectedCellNumber + cellsGridConstraintCount <= transform.childCount)
                {
                    SelectedCellNumber += cellsGridConstraintCount;
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
        itemsConcatinationModule = GetComponentInParent<ItemsConcatination>();
        itemsConcatinationNavigation = GetComponent<ItemsConcatinationNavigation>();
        cellsGridConstraintCount = GetComponent<GridLayoutGroup>().constraintCount;
    }
}
