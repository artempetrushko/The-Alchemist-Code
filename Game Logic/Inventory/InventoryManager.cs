using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private int inventorySize = 20;
    [SerializeField]
    private UnityEvent<ItemState> inventoryDataChanged;
    [SerializeField]
    private UnityEvent<ItemState> itemStateRemoved;

    private List<ItemState> items = new List<ItemState>();
    private PlayerSetItems playerSetItems = new PlayerSetItems();
    private InventoryView inventoryView;
    private PlayerInput player;
    
    public List<ItemState> Items => items;
    public PlayerSetItems PlayerSetItems => playerSetItems;
    public int InventorySize => inventorySize;
    private bool IsInventoryFull => items.Count == inventorySize;

    public bool AddNewItemState<T>(T itemState) where T : ItemState
    {
        switch (itemState)
        {
            case SingleItemState:
                return AddItemState(itemState);

            case StackableItemState:
                switch (itemState as StackableItemState)
                {
                    case IngredientState:
                        return AddStackableItemState(itemState as IngredientState);

                    case MaterialState:
                        return AddStackableItemState(itemState as MaterialState);
                }
                break;
        }     
        return false;
    }

    public bool CreateAndAddStackableItemStateCopy<T>(T baseItemState, int newItemsCount) where T : StackableItemState
    {
        var newItemState = baseItemState.Clone() as T;
        newItemState.ItemsCount = newItemsCount;
        return AddItemState(newItemState);
    }

    public bool DropItemState(ItemState itemState)
    {
        CreateDroppedItem(itemState);
        RemoveItemState(itemState);
        itemState.CurrentInventoryItemCellView = null;
        return true;
    }

    public void CreateDroppedItem(ItemState itemState)
    {
        var physicalItem = Instantiate(itemState.PhysicalRepresentaionPrefab);
        physicalItem.transform.position = player.transform.position;
        physicalItem.CurrentItemState = itemState;
    }

    public void RemoveItemState(ItemState itemState)
    {       
        itemStateRemoved.Invoke(itemState);
        items.Remove(itemState);
        Debug.Log(items.Count);
    }

    public void UpdatePlayerSetItems()
    {
        var cellContainers = inventoryView.PlayerSetItemCellContainers;
        foreach (var cellContainer in cellContainers)
        {
            switch (cellContainer)
            {
                case WeaponSetItemCellContainer:
                    var weaponCell = cellContainer.ContainedItem;
                    var weaponState = weaponCell != null ? weaponCell.ItemState as WeaponState : null;
                    switch ((cellContainer as WeaponSetItemCellContainer).WeaponCellPosition)
                    {
                        case WeaponCellContainerPosition.Left:
                            PlayerSetItems.LeftHandWeapon = weaponState;
                            break;

                        case WeaponCellContainerPosition.Right:
                            PlayerSetItems.RightHandWeapon = weaponState;
                            break;
                    }           
                    break;

                case EquipmentItemCellContainer:
                    var clothesCell = cellContainer.ContainedItem;
                    var clothesState = clothesCell != null ? clothesCell.ItemState as ClothesState : null;
                    switch ((cellContainer as EquipmentItemCellContainer).RequiredClothesType)
                    {
                        case ClothesType.Hat:
                            PlayerSetItems.Hat = clothesState;
                            break;

                        case ClothesType.Raincoat:
                            PlayerSetItems.Raincoat = clothesState;
                            break;

                        case ClothesType.Boots:
                            PlayerSetItems.Boots = clothesState;
                            break;

                        case ClothesType.Gloves:
                            PlayerSetItems.Gloves = clothesState;
                            break;

                        case ClothesType.Medallion:
                            PlayerSetItems.Medallion = clothesState;
                            break;
                    }
                    break;
            }
        }
        #region Debug Zone
        /*Debug.Log(PlayerSetItems.LeftHandWeapon);
        Debug.Log(PlayerSetItems.RightHandWeapon);
        Debug.Log(PlayerSetItems.Hat);
        Debug.Log(PlayerSetItems.Raincoat);
        Debug.Log(PlayerSetItems.Boots);
        Debug.Log(PlayerSetItems.Medallion);
        Debug.Log(PlayerSetItems.Gloves);*/
        #endregion
    }

    private bool AddStackableItemState<T>(T itemState) where T : StackableItemState
    {
        var partialFilledStacks = items
                .Select(stack => stack as T)
                .Where(stack => stack != null
                        && stack.ID == itemState.ID
                        && stack.ItemsCount < stack.MaxStackItemsCount);
        if (partialFilledStacks.Count() != 0)
        {
            var orderedStacks = partialFilledStacks.OrderByDescending(stack => stack.ItemsCount);
            foreach (var stack in orderedStacks)
            {
                var itemsCountToFillStack = stack.MaxStackItemsCount - stack.ItemsCount;
                if (itemState.ItemsCount > itemsCountToFillStack)
                {
                    stack.ItemsCount = stack.MaxStackItemsCount;
                    itemState.ItemsCount -= itemsCountToFillStack;
                    inventoryDataChanged.Invoke(stack);
                }
                else
                {
                    stack.ItemsCount += itemState.ItemsCount;
                    inventoryDataChanged.Invoke(stack);
                    return true;
                }
            }
        }
        while (itemState.ItemsCount > 0)
        {
            if (itemState.ItemsCount <= itemState.MaxStackItemsCount)
            {
                return AddItemState(itemState);
            }
            var newItemState = itemState.Clone() as T;
            newItemState.ItemsCount = itemState.MaxStackItemsCount;
            if (AddItemState(newItemState))
            {
                itemState.ItemsCount -= itemState.MaxStackItemsCount;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private bool AddItemState(ItemState itemState)
    {
        if (!IsInventoryFull)
        {
            items.Add(itemState);
            inventoryDataChanged.Invoke(itemState);
            Debug.Log(items.Count);
            return true;
        }
        return false;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerInput>();
        inventoryView = FindObjectOfType<InventoryView>();
        inventoryView.FillWithEmptyCells(InventorySize);
    }
}
