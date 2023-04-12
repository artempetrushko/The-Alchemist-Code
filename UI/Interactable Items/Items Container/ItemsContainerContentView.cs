using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsContainerContentView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text containerTitle;
    [SerializeField]
    private GameObject itemsContainerContent;
    [SerializeField]
    private Scrollbar viewScrollbar;
    [SerializeField]
    private GameObject containedItemViewPrefab;
    [SerializeField]
    private GameObject background;

    private int currentSelectedItemNumber;

    public int CurrentSelectedItemNumber
    {
        get => currentSelectedItemNumber;
        set
        {
            currentSelectedItemNumber = value;
            itemsContainerContent.transform.GetChild(currentSelectedItemNumber).GetComponent<Button>().Select();
            GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1 - (float)currentSelectedItemNumber / (ItemsCount - 1);
        }
    }

    public string ContainerTitle
    {
        get => containerTitle.text;
        set
        {
            containerTitle.text = value;
        }
    }

    private int ItemsCount => itemsContainerContent.transform.childCount;

    public void SetVisibility(bool isVisible)
    {
        gameObject.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
        background.SetActive(isVisible);
    }

    public void GenerateContainerContent(List<ItemState> spawnedItems, Action<int> pickItemFunction)
    {
        CreateContainedItemViews(spawnedItems, pickItemFunction);
        if (ItemsCount != 0)
        {
            CurrentSelectedItemNumber = 0;
        }
    }

    public void UpdateContainerContent(List<ItemState> spawnedItems, Action<int> pickItemFunction)
    {
        CreateContainedItemViews(spawnedItems, pickItemFunction);
        if (ItemsCount != 0)
        {
            CurrentSelectedItemNumber = currentSelectedItemNumber;
        }
    }

    public void ChangeSelectedContainedItem(Vector2 inputValue)
    {
        switch (inputValue.y)
        {
            case 1:
                if (currentSelectedItemNumber > 0)
                {
                    --CurrentSelectedItemNumber;
                }             
                break;
            case -1:
                if (currentSelectedItemNumber < itemsContainerContent.transform.childCount - 1)
                {
                    ++CurrentSelectedItemNumber;
                }
                break;
        }
    }

    public void PickCurrentItem()
    {
        if (ItemsCount != 0)
        {
            var pickingItem = itemsContainerContent.transform.GetChild(currentSelectedItemNumber).GetComponent<Button>();
            pickingItem.onClick.Invoke();
        }
    }

    private void CreateContainedItemViews(List<ItemState> spawnedItems, Action<int> pickItemFunction)
    {
        for (var i = ItemsCount - 1; i >= 0; i--)
        {
            Destroy(itemsContainerContent.transform.GetChild(i).gameObject);
        }

        for (var i = 0; i < spawnedItems.Count; i++)
        {
            var itemView = Instantiate(containedItemViewPrefab, itemsContainerContent.transform);
            switch (spawnedItems[i])
            {
                case SingleItemState:
                    var singleItem = spawnedItems[i] as SingleItemState;
                    itemView.GetComponent<ContainedItemView>().SetInfo(singleItem);
                    break;
                case StackableItemState:
                    var stackableItem = spawnedItems[i] as StackableItemState;
                    itemView.GetComponent<ContainedItemView>().SetInfo(stackableItem);
                    break;
            }
            var itemNumber = i;
            itemView.GetComponent<Button>().onClick.AddListener(() => pickItemFunction(itemNumber));
        }
    }

    private void Start()
    {
        SetVisibility(false);
    }
}
