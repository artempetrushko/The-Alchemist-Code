using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemInfo : MonoBehaviour
{
    protected ItemState itemState;
    protected Image inventoryItemIcon;

    public abstract ItemState ItemState { get; set; }

    public abstract ItemCellView AttachedItemCellView { set; }

    public abstract void UpdateItemCellViewState();

    public bool TrySetBigIcon()
    {
        if (itemState is EquipmentState && (itemState as EquipmentState).BigIcon != null)
        {
            inventoryItemIcon.sprite = (itemState as EquipmentState).BigIcon;
            return true;
        }
        return false;
    }

    protected abstract void OnDestroy();

    protected void OnEnable()
    {
        inventoryItemIcon = GetComponent<Image>();
    }
}
