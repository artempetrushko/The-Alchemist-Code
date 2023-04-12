using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RecipeIngredient : RecipeItem
{
    public int Count => count;

    public bool CheckSelectedItemMatching(ItemState selectedItem, bool isCountMatchingRequired = false)
    {
        if (isCountMatchingRequired)
        {
            return selectedItem switch
            {
                SingleItemState => selectedItem.CompareItemData(item),
                StackableItemState => selectedItem.CompareItemData(item) && (selectedItem as StackableItemState).ItemsCount >= count
            };
        }
        return selectedItem.CompareItemData(item);
    }
}
