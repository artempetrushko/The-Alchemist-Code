using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(QuickAccessCellBinding))]
public class QuickAccessToolbarSubsection : InventorySectionSubsection
{
    [Space]
    [SerializeField]
    private ItemsConcatination itemsConcatinationModule;

    private QuickAccessCellBinding cellBindingModule;    
    private ItemsConcatinationNavigation itemsConcatinationNavigation;

    public override void StartNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
        SelectedCellNumber = itemsConcatinationModule.IsInteractionStarted ? 6 : 3;
    }

    public override void ResumeNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
    }

    public override void Navigate(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>();
        if (inputValue.x == 1 || inputValue.x == -1)
        {
            if (SelectedCellNumber + inputValue.x > transform.childCount && !cellBindingModule.IsInteractionStarted)
            {
                if (itemsConcatinationModule.IsInteractionStarted)
                {
                    itemsConcatinationNavigation.RightSubsection.StartNavigation();
                    return;
                }
                else
                {
                    RightNeighboringSubsection.StartNavigation();
                }               
            }
            else
            {
                SelectedCellNumber = Mathf.Clamp(SelectedCellNumber + (int)inputValue.x, 1, transform.childCount);
            }
        }
        if (inputValue.y == 1 && !ItemCellsMoving.IsMovingStarted && !cellBindingModule.IsInteractionStarted && !itemsConcatinationModule.IsInteractionStarted)
        {
            TopNeighboringSubsection.StartNavigation();
        }
    }  

    public override void StopNavigation()
    {
        SelectedCell = null;
    }

    private void Start()
    {
        cellBindingModule = GetComponent<QuickAccessCellBinding>();
        itemsConcatinationNavigation = GetComponent<ItemsConcatinationNavigation>();
    }
}
