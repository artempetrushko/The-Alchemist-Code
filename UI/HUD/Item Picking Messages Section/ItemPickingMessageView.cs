using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickingMessageView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text itemsCountText;
    [SerializeField]
    private Image itemIcon;

    public void SetInfo(ItemState item)
    {
        itemIcon.sprite = item.Icon;
        itemsCountText.text = "+" + item switch
        {
            StackableItemState => (item as StackableItemState).ItemsCount.ToString(),
            _ => "1"
        };
    }
}
