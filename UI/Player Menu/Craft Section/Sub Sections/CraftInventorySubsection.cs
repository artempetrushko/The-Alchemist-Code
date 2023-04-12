using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum InventoryItemSelectionMode
{
    Normal,
    IngredientSelection,
}

public abstract class CraftInventorySubsection : CraftSectionSubsection
{
    private CraftInventorySectionCellView selectedItemCell;
    private int selectedCellNumber;

    public InventoryItemSelectionMode CurrentSelectionMode { get; set; }

    public CraftInventorySectionCellView SelectedItemCell 
    { 
        get => selectedItemCell; 
        protected set
        {
            if (selectedItemCell != null)
            {
                SelectedItemCell.ToggleBackgroundState(CellViewState.Normal);
                SelectedItemCell.DisableDescriptionPanel();
            }
            selectedItemCell = value;
            if (selectedItemCell != null)
            {
                SelectedItemCell.ToggleBackgroundState(CellViewState.Selected);
                SelectedItemCell.EnableDescriptionPanel();
            }                  
            if (sectionNavigation.CurrentSubsection != this)
            {
                sectionNavigation.CurrentSubsection = this;
            }
        }
    }

    public int SelectedCellNumber
    {
        get => selectedCellNumber;
        protected set
        {
            selectedCellNumber = value;
            SelectedItemCell = transform.GetChild(selectedCellNumber - 1).gameObject.GetComponent<CraftInventorySectionCellView>();
        }
    }

    public void StartNavigation_AlternateMode(InventoryItemSelectionMode mode) => StartNavigation(mode);

    public void SelectItemCellByPointer(ItemCellView itemCell)
    {
        SelectedCellNumber = GetComponentsInChildren<ItemCellView>().ToList().IndexOf(itemCell) + 1;
    }

    protected void StartNavigation(InventoryItemSelectionMode selectionMode)
    {
        CurrentSelectionMode = selectionMode;
        sectionNavigation.CurrentSubsection = this;
        SelectedCellNumber = 1;      
    }
}
