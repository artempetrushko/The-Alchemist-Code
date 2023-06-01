using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemsContainer : InteractiveObject
{
    [SerializeField]
    private ItemsSpawnChancesTable spawnChancesTable;
    [SerializeField]
    private GameObject filledContainerEffect;

    private List<ItemState> spawnedItems = new List<ItemState>();
    private InventoryManager inventoryManager;
    private ItemPickingMessagesSection itemPickingMessagesSection;
    private bool isItemsSpawned = false;

    public void OpenContainer()
    {
        if (!isItemsSpawned)
        {
            spawnedItems = spawnChancesTable.SpawnItems();
            isItemsSpawned = true;
        }
        interactiveObjectPanel.EnableContainerContentView(spawnedItems, PickItem);
    }

    public void CloseContainer()
    {
        if (isItemsSpawned && spawnedItems.Count == 0)
        {
            interactiveObjectPanel.DisableContainerContentView(true);
            if (filledContainerEffect != null)
            {
                Destroy(filledContainerEffect);
            }
            Destroy(this);
            return;
        }
        interactiveObjectPanel.DisableContainerContentView();       
    }

    public void PickItem(int itemNumber)
    {
        var pickingItem = spawnedItems[itemNumber];
        if (inventoryManager.AddNewItemState(pickingItem))
        {
            spawnedItems.RemoveAt(itemNumber);       
            itemPickingMessagesSection.UpdateContent(pickingItem);
        }
        interactiveObjectPanel.UpdateContainerContentView(spawnedItems, PickItem);
    }

    public void PickItemByPressKey() => interactiveObjectPanel.PickItemByPressKey();

    public void NavigateContainedItemsMenu(Vector2 inputValue) => interactiveObjectPanel.ChangeSelectedContainedItem(inputValue);

    public void PickAll()
    {
        for (var i = spawnedItems.Count - 1; i >= 0; i--)
        {
            PickItem(i);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        inventoryManager = FindObjectOfType<InventoryManager>();
        itemPickingMessagesSection = FindObjectOfType<ItemPickingMessagesSection>();
        if (filledContainerEffect != null && !filledContainerEffect.activeInHierarchy)
        {
            filledContainerEffect.SetActive(true);
        }
    }
}
