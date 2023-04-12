using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField]
    private ItemData itemData;
    private ItemState currentItemState;

    public ItemState CurrentItemState 
    { 
        get => currentItemState != null ? currentItemState : itemData.GetItemState();
        set
        {
            if (value.CompareItemData(itemData))
            {
                currentItemState = value;
            }
        }   
    }
}
