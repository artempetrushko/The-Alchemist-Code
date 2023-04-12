using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuickAccessCellBinding : ItemsInteractionModule
{
    [SerializeField]
    private QuickAccessToolbarSubsection quickAccessSubsection;

    public override void StartInteraction(ItemCellContainer currentItemCell)
    {
        IsInteractionStarted = true;
        this.currentItemCell = currentItemCell;
        interactionProcessBackground.gameObject.SetActive(true);
        interactionProcessBackground.transform.SetAsLastSibling();
        quickAccessSubsection.transform.SetAsLastSibling();
        quickAccessSubsection.StartNavigation();
        inputManager.SwitchActionMap_UI_Inventory_QuickAccessCellBinding();
    }

    public override void Execute()
    {
        quickAccessSubsection.SelectedCell.GetComponent<ItemCellContainer>().SwapAndPlaceItem(currentItemCell);
        FinishInteraction();
    }

    protected override void FinishInteraction()
    {
        IsInteractionStarted = false;
        interactionProcessBackground.gameObject.SetActive(false);
        quickAccessSubsection.ResumeNavigation();
    }
}
