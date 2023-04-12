using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnergyCellsSubsection : CraftSectionSubsection
{
    private EnergyCellView selectedEnergyCell;

    public EnergyCellView SelectedEnergyCell
    {
        get => selectedEnergyCell;
        set
        {
            if (selectedEnergyCell != null)
            {
                selectedEnergyCell.ToggleBackgroundState(CellViewState.Normal);
            }
            selectedEnergyCell = value;
            if (selectedEnergyCell != null)
            {
                selectedEnergyCell.ToggleBackgroundState(CellViewState.Selected);
            }
            if (sectionNavigation.CurrentSubsection != this)
            {
                sectionNavigation.CurrentSubsection = this;
            }
        }
    }

    public override void StartNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
        SelectedEnergyCell = transform.GetComponentInChildren<EnergyCellView>();
    }

    public override void ResumeNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
        SelectedEnergyCell.ToggleBackgroundState(CellViewState.Selected);
    }

    public override void Navigate(InputAction.CallbackContext context)
    {
        ChangeEnergyCell(context);
    }

    public override void StopNavigation()
    {
        SelectedEnergyCell.ToggleBackgroundState(CellViewState.Normal);
    }

    private void ChangeEnergyCell(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var inputValue = context.ReadValue<Vector2>();
            switch (inputValue.x)
            {
                case 1:
                    if (SelectedEnergyCell.RightNeighboringCell != null)
                    {
                        SelectedEnergyCell = SelectedEnergyCell.RightNeighboringCell;
                    }
                    else if (RightNeighboringSubsection != null)
                    {
                        RightNeighboringSubsection.StartNavigation();
                    }
                    break;

                case -1:
                    if (SelectedEnergyCell.LeftNeighboringCell != null)
                    {
                        SelectedEnergyCell = SelectedEnergyCell.LeftNeighboringCell;
                    }
                    else if (LeftNeighboringSubsection != null)
                    {
                        LeftNeighboringSubsection.StartNavigation();
                    }
                    break;
            }
            switch (inputValue.y)
            {
                case 1:
                    if (SelectedEnergyCell.TopNeighboringCell != null)
                    {
                        SelectedEnergyCell = SelectedEnergyCell.TopNeighboringCell;
                    }                   
                    break;

                case -1:
                    if (SelectedEnergyCell.BottomNeighboringCell != null)
                    {
                        SelectedEnergyCell = SelectedEnergyCell.BottomNeighboringCell;
                    }                  
                    break;
            }
        }
    }
}
