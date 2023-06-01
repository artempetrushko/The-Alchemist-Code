using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerItemPicker : ItemPicker
{
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private UnityEvent<ItemState> onItemPicked;

    public override void PickItems()
    {
        if (pickableItems.Count > 0)
        {
            foreach (var item in pickableItems)
            {
                if (inventoryManager.AddNewItemState(item.CurrentItemState))
                {
                    onItemPicked.Invoke(item.CurrentItemState);
                    Destroy(item.gameObject);
                }
            }
            pickableItems.Clear();
        }
    }

    private void Start()
    {
        if (inventoryManager == null)
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
        }
    } 
}
