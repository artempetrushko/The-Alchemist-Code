using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CraftInventoryCategoriesNavigation : MonoBehaviour
{
    [SerializeField]
    private CraftInventoryMainSubsection itemCells;
    [SerializeField]
    private CraftInventoryEquipmentSubsection playerSetCells;
    [SerializeField]
    private CraftInventoryQuickAccessSubsection quickAccessCells;
    [SerializeField]
    private GameObject itemCategoryButtons;

    private CraftSectionNavigation craftSectionNavigation;
    private List<InventoryItemsCategoryButton> categoryButtons;
    private GameObject currentItemCellsCategoryContainer;
    private int currentItemCellsCategoryIndex;

    public CraftInventorySubsection CurrentItemCellsCategory { get; private set; }
    private GameObject CurrentItemCellsCategoryContainer
    {
        get => currentItemCellsCategoryContainer;
        set
        {
            if (currentItemCellsCategoryContainer != value)
            {
                if (currentItemCellsCategoryContainer != null)
                {
                    ToggleCurrentCategoryVisibility(false);
                }               
                currentItemCellsCategoryContainer = value;
                ToggleCurrentCategoryVisibility(true);
            }
        }
    }
    private int CurrentItemCellsCategoryIndex
    {
        get => currentItemCellsCategoryIndex;
        set
        {
            if (value >= 0 && value < categoryButtons.Count)
            {
                categoryButtons[currentItemCellsCategoryIndex].ToggleButtonState(false);
                currentItemCellsCategoryIndex = value;
                categoryButtons[currentItemCellsCategoryIndex].ToggleButtonState(true);
                switch (currentItemCellsCategoryIndex)
                {
                    case 0:
                        CurrentItemCellsCategory = itemCells;
                        CurrentItemCellsCategoryContainer = itemCells.GetComponentInParent<ScrollRect>().gameObject;                                           
                        currentItemCellsCategoryContainer.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                        if (craftSectionNavigation.CurrentSubsection is CraftInventoryEquipmentSubsection
                            || craftSectionNavigation.CurrentSubsection is CraftInventoryQuickAccessSubsection)
                        {
                            itemCells.GetComponentInChildren<CraftInventoryMainSubsection>().StartNavigation();
                        }
                        break;

                    case 1:
                        CurrentItemCellsCategory = playerSetCells;
                        CurrentItemCellsCategoryContainer = playerSetCells.transform.parent.gameObject;  
                        if (craftSectionNavigation.CurrentSubsection is CraftInventoryMainSubsection)
                        {
                            playerSetCells.GetComponentInChildren<CraftInventoryEquipmentSubsection>().StartNavigation();
                        }                       
                        break;
                }
            }
        }
    }

    public void ChangeItemsCategory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CurrentItemCellsCategoryIndex += (int)context.ReadValue<Vector2>().x;
        }
    }

    public void StartCurrentCategoryNavigation(InventoryItemSelectionMode selectionMode)
    {
        itemCells.CurrentSelectionMode = selectionMode;
        playerSetCells.CurrentSelectionMode = selectionMode;
        quickAccessCells.CurrentSelectionMode = selectionMode;
        CurrentItemCellsCategory.StartNavigation_AlternateMode(selectionMode);
    }

    public void ShowDefaultInventorySubsection() => CurrentItemCellsCategoryIndex = 0;

    public void SelectItemCellsCategoryByIndex(int index) => CurrentItemCellsCategoryIndex = index;

    private void ToggleCurrentCategoryVisibility(bool isVisible)
    {
        currentItemCellsCategoryContainer.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
    }

    private void OnEnable()
    {
        craftSectionNavigation = FindObjectOfType<CraftSectionNavigation>();
        categoryButtons = itemCategoryButtons.GetComponentsInChildren<InventoryItemsCategoryButton>().ToList();
    }
}
