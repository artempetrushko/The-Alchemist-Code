using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CraftingItemTemplateSubsection : CraftSectionSubsection
{
    [Space]
    [SerializeField]
    private GameObject templateCellsContainer;

    private List<CraftingItemTemplateCellView> templateCells;
    private int currentCellNumber;

    public CraftingItemTemplateCellView CurrentSelectedCell
    {
        get => templateCells[CurrentCellNumber];
        set
        {
            CurrentCellNumber = templateCells.IndexOf(value);
            if (sectionNavigation.CurrentSubsection != this)
            {
                sectionNavigation.CurrentSubsection = this;
            }
        }
    }

    private int CurrentCellNumber
    {
        get => currentCellNumber;
        set
        {
            templateCells[currentCellNumber].ToggleBackgroundState(CellViewState.Normal);
            if (value > templateCells.Count - 1)
            {
                currentCellNumber = 0;
            }
            else if (value < 0)
            {
                currentCellNumber = templateCells.Count - 1;
            }
            else
            {
                currentCellNumber = value;
            }            
            templateCells[currentCellNumber].ToggleBackgroundState(CellViewState.Selected);
        }
    }

    public override void StartNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
        CurrentCellNumber = 0;
    }

    public override void ResumeNavigation()
    {
        sectionNavigation.CurrentSubsection = this;
        templateCells[CurrentCellNumber].ToggleBackgroundState(CellViewState.Selected);
    }

    public override void Navigate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var inputValue = context.ReadValue<Vector2>();
            if (inputValue.x == 1 || inputValue.x == -1)
            {
                CurrentCellNumber += (int)inputValue.x;
            }
            if (inputValue.y == 1 || inputValue.y == -1)
            {
                CurrentCellNumber -= (int)inputValue.y;
            }
        }
    }

    public override void StopNavigation()
    {
        templateCells[CurrentCellNumber].ToggleBackgroundState(CellViewState.Normal);
    }

    private void OnEnable()
    {
        templateCells = templateCellsContainer.GetComponentsInChildren<CraftingItemTemplateCellView>().ToList();
    }
}
