using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QuickAccessToolbar : MonoBehaviour
{
    [SerializeField]
    private GameObject quickAccessToolbarView;

    private InventoryManager inventoryManager;
    private SimpleItemCellContainer currentQuickAccessCell;
    private int currentQuickAccessCellNumber;

    public SimpleItemCellContainer CurrentQuickAccessCell
    {
        get => currentQuickAccessCell;
        private set
        {
            if (currentQuickAccessCell != null)
            {
                currentQuickAccessCell.GetComponent<QuickAccessItemCellView>().LinkedItemCellView.ToggleBackgroundState(CellViewState.Normal);
            }          
            currentQuickAccessCell = value;
            currentQuickAccessCell.GetComponent<QuickAccessItemCellView>().LinkedItemCellView.ToggleBackgroundState(CellViewState.Selected);

            var containedInventoryItem = currentQuickAccessCell.ContainedItem;
            inventoryManager.PlayerSetItems.SelectedQuickAccessItem = containedInventoryItem != null 
                ? containedInventoryItem.ItemState
                : null;
        }
    }

    public int CurrentQuickAccessCellNumber
    {
        get => currentQuickAccessCellNumber;
        private set
        {
            if (value < 0)
            {
                currentQuickAccessCellNumber = transform.childCount - 1;
            }
            else if (value > transform.childCount - 1)
            {
                currentQuickAccessCellNumber = 0;
            }
            else
            {
                currentQuickAccessCellNumber = value;
            }
            CurrentQuickAccessCell = transform.GetChild(currentQuickAccessCellNumber).GetComponent<SimpleItemCellContainer>();
        }
    }

    public void SelectFirstQuickAccessCell(InputAction.CallbackContext context) => SelectQuickAccessCell(context, 0);

    public void SelectSecondQuickAccessCell(InputAction.CallbackContext context) => SelectQuickAccessCell(context, 1);

    public void SelectThirdQuickAccessCell(InputAction.CallbackContext context) => SelectQuickAccessCell(context, 2);

    public void SelectFourthQuickAccessCell(InputAction.CallbackContext context) => SelectQuickAccessCell(context, 3);

    public void SelectFifthQuickAccessCell(InputAction.CallbackContext context) => SelectQuickAccessCell(context, 4);

    public void SelectSixthQuickAccessCell(InputAction.CallbackContext context) => SelectQuickAccessCell(context, 5);

    public void ChangeQuickAccessCellByInputValue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var inputValue = context.ReadValue<Vector2>().x;
            if (inputValue == 1 || inputValue == -1)
            {
                CurrentQuickAccessCellNumber += (int)inputValue;
            }
        }
    }

    private void SelectQuickAccessCell(InputAction.CallbackContext context, int cellNumber)
    {
        if (context.performed)
        {
            CurrentQuickAccessCellNumber = cellNumber;
        }
    }

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        CurrentQuickAccessCellNumber = 0;
    }
}
