using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryCellInteractions : MonoBehaviour
{
    [SerializeField]
    private ChooseItemsCountPanel chooseItemsCountPanel;
    [SerializeField]
    private QuickAccessCellBinding quickAccessCellBindingModule;
    [SerializeField]
    private WeaponEquipping weaponEquippingModule;
    [SerializeField]
    private ItemsConcatination itemsConcatinationModule;
    [Space]
    [SerializeField]
    private UnityEvent onChooseItemsCountPanelClosed;
    [SerializeField]
    private UnityEvent onItemCellInteractionExecuted;

    private InventoryView inventoryView;
    private InventorySectionNavigation inventorySectionNavigation;
    private PlayerInputManager player;
    private InventoryManager inventoryManager;

    private ItemCellContainer CurrentInteractingItemCellContainer => (inventorySectionNavigation.CurrentSubsection as InventorySectionSubsection).SelectedCell.GetComponent<ItemCellContainer>();
    private ItemState CurrentInteractingItemState => CurrentInteractingItemCellContainer.ContainedItem.ItemState;
    private int SelectedItemsCount => chooseItemsCountPanel.DisplayedItemsCount;

    public void StartItemsSplitting()
    {
        HideItemCellActionsMenu();
        ShowChooseItemsCountPanel(new ChooseItemsCountActions
        (
            SplitItemsUp,
            CloseChooseItemsCountPanel
        ));
    }

    public void DropItems()
    {
        HideItemCellActionsMenu();
        if (CurrentInteractingItemState is StackableItemState)
        {
            ShowChooseItemsCountPanel(new ChooseItemsCountActions
            (
                DropSomeItems,
                DropAllItems,
                CloseChooseItemsCountPanel
            ));
        }
        else
        {
            DropAllItems();
        }
    }

    public void StartQuickAccessCellChoosing()
    {
        onItemCellInteractionExecuted.Invoke();
        quickAccessCellBindingModule.StartInteraction(CurrentInteractingItemCellContainer);
    }

    public void StartItemsConcatination()
    {
        onItemCellInteractionExecuted.Invoke();
        itemsConcatinationModule.StartInteraction(CurrentInteractingItemCellContainer);
    }

    public void EquipItem()
    {
        HideItemCellActionsMenu();
        switch (CurrentInteractingItemState)
        {
            case WeaponState:
                onItemCellInteractionExecuted.Invoke();
                weaponEquippingModule.StartInteraction(CurrentInteractingItemCellContainer);
                break;

            case ClothesState:
                var requiredCellContainer = inventoryView.PlayerSetItemCells.GetComponentsInChildren<EquipmentItemCellContainer>()
                    .Where(container => container.RequiredClothesType == (CurrentInteractingItemState as ClothesState).ClothesType)
                    .First();
                requiredCellContainer.SwapAndPlaceItem(CurrentInteractingItemCellContainer);
                break;
        }
    }

    public void EquipWeaponInOtherHand()
    {
        var otherWeaponCell = CurrentInteractingItemCellContainer.transform.parent.GetComponentsInChildren<WeaponSetItemCellContainer>()
            .Where(container => container != CurrentInteractingItemCellContainer)
            .First();
        otherWeaponCell.SwapAndPlaceItem(CurrentInteractingItemCellContainer);
        onItemCellInteractionExecuted.Invoke();
    }

    public void TakeEquipmentOff()
    {
        onItemCellInteractionExecuted?.Invoke();
        (CurrentInteractingItemCellContainer as PlayerSetItemCellContainer).RemoveAttachedItem();       
    }

    private void SplitItemsUp()
    {
        var selectedItem = CurrentInteractingItemState as StackableItemState;
        selectedItem.ItemsCount -= SelectedItemsCount;

        if (inventoryManager.CreateAndAddStackableItemStateCopy(selectedItem, SelectedItemsCount))
        {
            CloseChooseItemsCountPanel();
        }
    }

    private void DropSomeItems()
    {
        var selectedItem = CurrentInteractingItemState as StackableItemState;
        selectedItem.ItemsCount -= SelectedItemsCount;
        FindObjectOfType<GameManager>().CountSpecialItems();

        var droppedItemState = selectedItem.Clone() as StackableItemState;
        droppedItemState.ItemsCount = SelectedItemsCount;
        inventoryManager.CreateDroppedItem(droppedItemState);
        CloseChooseItemsCountPanel();
    }

    private void DropAllItems()
    {       
        if (chooseItemsCountPanel.isActiveAndEnabled)
        {
            CloseChooseItemsCountPanel();
        }
        else
        {
            onItemCellInteractionExecuted?.Invoke();
        }
        inventoryManager.DropItemState(CurrentInteractingItemState);
    }

    private void HideItemCellActionsMenu() => FindObjectOfType<ItemCellActionMenu>().transform.localScale = Vector3.zero;

    private void ShowChooseItemsCountPanel(ChooseItemsCountActions actions)
    {
        chooseItemsCountPanel.gameObject.SetActive(true);
        chooseItemsCountPanel.StartItemsCountChoosing(CurrentInteractingItemState as StackableItemState, actions);
        player.SwitchActionMap_UI_ChooseItemsCountPanel();
    }

    private void CloseChooseItemsCountPanel()
    {
        chooseItemsCountPanel.gameObject.SetActive(false);
        onChooseItemsCountPanelClosed.Invoke();
    }

    private void Start()
    {
        inventoryView = GetComponent<InventoryView>();
        inventorySectionNavigation = GetComponent<InventorySectionNavigation>();
        player = FindObjectOfType<PlayerInputManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
}
