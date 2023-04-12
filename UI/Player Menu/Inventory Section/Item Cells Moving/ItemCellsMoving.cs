using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCellsMoving : MonoBehaviour
{
    private ItemInfo movingItem;
    private ItemCellContainer movingCellCurrentParent;

    private UIManager uiManager;
    private InventorySectionNavigation inventorySectionNavigation;
    private float movingStateCellScale = 1.15f;

    public static bool IsMovingStarted { get; private set; } = false;

    private ItemCellContainer MovingCellCurrentParent
    {
        get => movingCellCurrentParent;
        set
        {
            movingCellCurrentParent = value;
            if (movingCellCurrentParent != null && movingItem != null)
            {
                movingItem.GetComponent<ItemInfo>().AttachedItemCellView = movingCellCurrentParent.GetComponentInParent<ItemCellView>();
            }            
        }
    }

    public bool TryStartCellMoving()
    {
        var currentSelectedCell = (inventorySectionNavigation.CurrentSubsection as InventorySectionSubsection).SelectedCell.GetComponent<ItemCellContainer>();
        if (currentSelectedCell is not PlayerSetItemCellContainer && !currentSelectedCell.IsItemPlaceEmpty)
        {
            IsMovingStarted = true;
            ToggleCursorState(false);
            MovingCellCurrentParent = currentSelectedCell;
            movingItem = MovingCellCurrentParent.ContainedItem;
            movingItem.transform.localScale = new Vector3(movingStateCellScale, movingStateCellScale, 1);
            movingItem.transform.SetParent(uiManager.transform);
            return true;
        }
        return false;
    }

    public void MoveCell(ItemCellContainer currentSelectedCellContainer)
    {
        if (IsMovingStarted)
        {
            var currentSelectedCellContent = currentSelectedCellContainer.ContainedItem;
            if (currentSelectedCellContent != null)
            {
                movingItem.AttachedItemCellView = null;
                MovingCellCurrentParent.PlaceItem(currentSelectedCellContent);
            }
            movingItem.transform.position = currentSelectedCellContainer.ItemPlace.transform.position;
            MovingCellCurrentParent = currentSelectedCellContainer;            
        }
    }

    public void FinishCellMoving()
    {
        IsMovingStarted = false;
        ToggleCursorState(true);
        MovingCellCurrentParent.PlaceItem(movingItem);
        movingItem = null;
        MovingCellCurrentParent = null;
    }

    private void ToggleCursorState(bool isEnable)
    {
        Cursor.lockState = isEnable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isEnable;
    }

    private void OnEnable()
    {
        uiManager = FindObjectOfType<UIManager>();
        inventorySectionNavigation = GetComponent<InventorySectionNavigation>();
    }
}
