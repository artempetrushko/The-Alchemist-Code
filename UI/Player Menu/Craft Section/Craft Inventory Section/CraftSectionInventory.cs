using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftSectionInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject itemsCells;
    [SerializeField]
    private GameObject equipmentCells;
    [SerializeField]
    private GameObject quickAccessCells;
    [SerializeField]
    private ScrollRect inventoryScrollView;
    [SerializeField]
    private CraftInventoryItemCellView emptyItemCellPrefab;
    [SerializeField]
    private InventoryView mainInventorySection;

    private CraftManager craftManager;
    private InventoryManager inventoryManager;

    public ScrollRect InventoryScrollView => inventoryScrollView;

    public void SetVisibility(bool isVisible)
    {
        gameObject.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
    }

    public bool ToggleVisibility()
    {
        SetVisibility(gameObject.transform.localScale == Vector3.zero);
        return gameObject.transform.localScale == Vector3.one;
    }

    public void UpdateInventoryContent()
    {
        ClearInventory();
        var inventoryItems = inventoryManager.Items;
        if (craftManager.CurrentCraftingItemCells != null)
        {
            var ignoringItems = craftManager.CurrentCraftingItemCells
                .Select(cell => cell.GetComponent<ItemCellContainer>().ContainedItem)
                .Where(item => item != null)
                .Select(item => item.ItemState)
                .ToList();
            inventoryItems = inventoryManager.Items.Where(item => !ignoringItems.Contains(item)).ToList();
        }      
        foreach (var item in inventoryItems) 
        {
            item.CurrentInventoryItemCellView.LinkedMechanicsItemCellView.GetComponent<ItemCellContainer>().InstantiateAndPlaceItem(item);
        }
    }

    private void ClearInventory()
    {
        for (var i = itemsCells.transform.childCount; i > 0; i--)
        {
            Destroy(itemsCells.transform.GetChild(i - 1).gameObject);
        }
        itemsCells.transform.DetachChildren();

        for (var i = 0; i < inventoryManager.InventorySize; i++)
        {
            var itemCell = Instantiate(emptyItemCellPrefab, itemsCells.transform);
            var linkedMainInventoryItemCellView = mainInventorySection.InventoryCells.GetComponentsInChildren<InventoryItemCellView>()[i];
            itemCell.LinkedMainInventoryItemCellView = linkedMainInventoryItemCellView;
            linkedMainInventoryItemCellView.LinkedMechanicsItemCellView = itemCell;
        }
    }

    private void OnEnable()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        craftManager = FindObjectOfType<CraftManager>();
    }
}
