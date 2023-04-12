using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventorySectionSubsection : PlayerMenuSectionSubsection
{
    protected InventorySectionNavigation sectionNavigation;
    protected SimpleItemCellView currentSelectedCell;
    protected int currentSelectedCellNumber;

    public InventorySectionNavigation SectionNavigation => sectionNavigation;

    public SimpleItemCellView SelectedCell
    {
        get => currentSelectedCell;
        set
        {
            if (currentSelectedCell != value)
            {
                if (currentSelectedCell != null)
                {
                    ToggleSelectedCellViewState(false);
                }
                currentSelectedCell = value;
                if (currentSelectedCell != null)
                {
                    ToggleSelectedCellViewState(true);
                    currentSelectedCellNumber = GetComponentsInChildren<ItemCellView>().ToList().IndexOf(currentSelectedCell) + 1;
                    SectionNavigation.GetComponent<ItemCellsMoving>().MoveCell(currentSelectedCell.GetComponent<ItemCellContainer>());
                    if (sectionNavigation.CurrentSubsection != this)
                    {
                        sectionNavigation.CurrentSubsection = this;
                    }
                } 
            }           
        }
    }

    public int SelectedCellNumber
    {
        get => currentSelectedCellNumber;
        set
        {
            currentSelectedCellNumber = value;
            SelectedCell = transform.GetChild(currentSelectedCellNumber - 1).GetComponent<SimpleItemCellView>();
        }
    }

    public void ChangeCellPressState(bool isPressed)
    {
        if (!SelectedCell.GetComponent<ItemCellContainer>().IsItemPlaceEmpty)
        {
            SelectedCell.GetComponent<ItemCellSelect>().IsPressed = isPressed;
            SelectedCell.ToggleBackgroundState(isPressed ? CellViewState.Selected : CellViewState.Normal);
            SelectedCell.ToggleDescriptionPanelState(!isPressed);
        }
    }

    protected void ToggleSelectedCellViewState(bool isSelected)
    {
        SelectedCell.ToggleBackgroundState(isSelected ? CellViewState.Selected : CellViewState.Normal);
        SelectedCell.ToggleDescriptionPanelState(isSelected);
        if (SelectedCell is EquipmentItemCellView && !SelectedCell.GetComponent<ItemCellContainer>().IsItemPlaceEmpty)
        {
            (SelectedCell as EquipmentItemCellView).ToggleRemoveItemButtonState(isSelected);
        }
    }

    protected virtual void Awake()
    {
        sectionNavigation = GetComponentInParent<InventorySectionNavigation>();
    }
}
