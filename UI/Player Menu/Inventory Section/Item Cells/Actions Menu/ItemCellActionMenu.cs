using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuItem
{
    public string ItemDescription;
    public Sprite ItemIcon;
    public Action ItemAction;

    public ActionMenuItem(string itemDescription, Sprite icon, Action action)
    {
        ItemDescription = itemDescription;
        ItemIcon = icon;
        ItemAction = action;
    }
}

public class ItemCellActionMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject actionButtonPrefab;
    [Header("Action Icons")]
    [SerializeField]
    private Sprite quickAccessActionIcon;
    [SerializeField]
    private Sprite useActionIcon;
    [SerializeField]
    private Sprite joinActionIcon;
    [SerializeField]
    private Sprite splitActionIcon;
    [SerializeField]
    private Sprite equipActionIcon;
    [SerializeField]
    private Sprite takeOffActionIcon;
    [SerializeField]
    private Sprite dropActionIcon;

    private InventoryCellInteractions inventoryCellInteractions;
    private List<Button> actionButtons = new List<Button>();
    private int currentActionButtonNumber;    
    private float actionMenuPanelOffsetX = Screen.width * 0.14f;
    private float actionMenuPanelOffsetY = Screen.height * 0.15f;

    public Button CurrentActionButton => actionButtons[currentActionButtonNumber];

    private int CurrentActionButtonNumber
    {
        get => currentActionButtonNumber;
        set
        {
            if (value >= 0 && value < actionButtons.Count)
            {
                currentActionButtonNumber = value;
                actionButtons[currentActionButtonNumber].Select();
            }            
        }
    }

    public void GenerateActions(ItemState item, GameObject linkedItemCell)
    {
        switch (item)
        {
            case SingleItemState:
                var currentItemCellContainer = item.CurrentInventoryItemCellView.GetComponent<ItemCellContainer>();
                switch (currentItemCellContainer)
                {
                    case WeaponSetItemCellContainer:
                        GenerateActionsForSelectedWeapon();
                        break;

                    case PlayerSetItemCellContainer:
                        GenerateActionsForSelectedEquipment();
                        break;

                    case QuickAccessItemCellContainer:
                        GenerateActionsForQuickAccessItem(item);
                        break;

                    default:
                        GenerateActionsForSingleItem(item as SingleItemState);
                        break;
                }
                break;

            case StackableItemState:
                GenerateActionForStackableItem(item as StackableItemState);
                break;
        }
        SetPosition(linkedItemCell);
        CurrentActionButtonNumber = 0;
    } 

    public void SelectOtherButton(Vector2 inputValue)
    {
        if (inputValue.y == 1 || inputValue.y == -1)
        {
            CurrentActionButtonNumber -= (int)inputValue.y;
        }    
    }
    private void SetPosition(GameObject linkedItemCell)
    {
        var itemCellPosition = linkedItemCell.transform.position;
        var itemCellRect = linkedItemCell.GetComponent<RectTransform>().rect;
        var actionMenuRect = GetComponent<RectTransform>().rect;

        transform.position = itemCellPosition;
        if (Screen.width - (itemCellPosition.x + itemCellRect.width / 2) > actionMenuRect.width + actionMenuPanelOffsetX)
        {
            transform.position += new Vector3(actionMenuPanelOffsetX, itemCellRect.height, 0);
        }
        else if (itemCellPosition.x - itemCellRect.width / 2 > actionMenuRect.width + actionMenuPanelOffsetX)
        {
            transform.position += new Vector3(-actionMenuPanelOffsetX, itemCellRect.height, 0);
        }
        else if (Screen.height - (itemCellPosition.y + itemCellRect.height / 2) > actionMenuRect.height + actionMenuPanelOffsetY)
        {
            transform.position += new Vector3(0, actionMenuPanelOffsetY, 0);
        }
        else transform.position += new Vector3(0, -actionMenuPanelOffsetY, 0);
    }

    private void GenerateActionsForSelectedWeapon()
    {
        GenerateActionButtons(new List<ActionMenuItem>()
        {   
            new ActionMenuItem("Поменять руку", takeOffActionIcon, () => inventoryCellInteractions.EquipWeaponInOtherHand()),
        });
        GenerateActionsForSelectedEquipment();
    }

    private void GenerateActionsForSelectedEquipment()
    {
        GenerateActionButtons(new List<ActionMenuItem>()
        {
            new ActionMenuItem("Снять", takeOffActionIcon, () => inventoryCellInteractions.TakeEquipmentOff()),
            new ActionMenuItem("Выбросить", dropActionIcon, () => inventoryCellInteractions.DropItems()),
        });
    }

    private void GenerateActionsForQuickAccessItem(ItemState item)
    {
        GenerateActionButtons(new List<ActionMenuItem>()
        {
            new ActionMenuItem("Убрать", equipActionIcon, null),
        });
        switch (item)
        {
            case SingleItemState:
                GenerateActionsForSingleItem(item as SingleItemState); 
                break;

            case StackableItemState:
                GenerateActionForStackableItem(item as StackableItemState);
                break;
        }
    }

    private void GenerateActionsForSingleItem(SingleItemState item)
    {
        if (item is EquipmentState)
        {
            GenerateActionButtons(new List<ActionMenuItem>()
            {
                new ActionMenuItem("Экипировать", equipActionIcon, () => inventoryCellInteractions.EquipItem()),
            });
        }
        GenerateActionButtons(new List<ActionMenuItem>()
        {
            new ActionMenuItem("Быстрый доступ", quickAccessActionIcon, () => inventoryCellInteractions.StartQuickAccessCellChoosing()),
            new ActionMenuItem("Выбросить", dropActionIcon, () => inventoryCellInteractions.DropItems()),
        });
    }

    private void GenerateActionForStackableItem(StackableItemState item)
    {
        GenerateActionButtons(new List<ActionMenuItem>()
        {
            new ActionMenuItem("Быстрый доступ", quickAccessActionIcon, () => inventoryCellInteractions.StartQuickAccessCellChoosing()),
            new ActionMenuItem("Объединить", joinActionIcon, () => inventoryCellInteractions.StartItemsConcatination()),
            new ActionMenuItem("Отделить", splitActionIcon, () => inventoryCellInteractions.StartItemsSplitting()),
            new ActionMenuItem("Выбросить", dropActionIcon, () => inventoryCellInteractions.DropItems()),
        });
    }

    private void GenerateActionButtons(List<ActionMenuItem> buttonsData)
    {
        foreach (var buttonData in buttonsData)
        {
            var actionButton = Instantiate(actionButtonPrefab, transform).GetComponent<Button>();
            actionButton.GetComponentInChildren<TMP_Text>().text = buttonData.ItemDescription;
            actionButton.GetComponentInChildren<Image>().sprite = buttonData.ItemIcon;
            actionButton.onClick.AddListener(() => buttonData.ItemAction());
            actionButtons.Add(actionButton);
        }
    }

    private void OnEnable()
    {
        inventoryCellInteractions = FindObjectOfType<InventoryCellInteractions>();
    }
}
