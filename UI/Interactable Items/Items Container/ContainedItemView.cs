using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainedItemView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text itemTitle;
    [SerializeField]
    private TMP_Text itemDescription;
    [SerializeField]
    private Image itemIcon;
    [SerializeField]
    private GameObject itemsCounter;

    public void SetInfo(SingleItemState item) => SetBaseInfo(item);

    public void SetInfo(StackableItemState item)
    {
        SetBaseInfo(item);
        itemsCounter.SetActive(true);
        itemsCounter.GetComponentInChildren<TMP_Text>().text = "x" + item.ItemsCount.ToString();
    }

    private void SetBaseInfo(ItemState item)
    {
        itemTitle.text = item.Title;
        itemDescription.text = item.Description;
        itemIcon.sprite = item.Icon;
    }
}
