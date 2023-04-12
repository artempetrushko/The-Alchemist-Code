using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChooseItemsCountPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text actionDescription;
    [SerializeField]
    private Image itemIcon;
    [SerializeField]
    private TMP_Text itemsCounterText;
    [SerializeField]
    private Slider itemsCounterSlider;
    [SerializeField]
    private TMP_Text itemsMinCountText;
    [SerializeField]
    private TMP_Text itemsMaxCountText;
    [SerializeField]
    private GameObject buttonsContainer;
    [Space]
    [SerializeField]
    private ActionButton actionButtonPrefab;

    private PlayerInputManager inputManager;
    private ChooseItemsCountActions currentActions;
    private int itemsCount;
    private int maxItemsCount;

    public int DisplayedItemsCount 
    { 
        get => itemsCount; 
        set
        {
            itemsCount = Mathf.Clamp(value, 1, maxItemsCount);
            itemsCounterText.text = itemsCount.ToString();
            itemsCounterSlider.value = (float)itemsCount / maxItemsCount;
        }
    }

    #region Input System Callbacks
    public void InvokeConfirmAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentActions.Confirm.Invoke();
        }        
    }

    public void InvokeConfirmAllAction(InputAction.CallbackContext context)
    {
        if (context.performed && currentActions.ConfirmAll != null)
        {
            currentActions.ConfirmAll.Invoke();
        }
    }

    public void InvokeCancelAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentActions.Cancel.Invoke();
        }
    }

    public void ChangeItemsCounterValue(InputAction.CallbackContext context)
    {
        var inputValue = context.ReadValue<Vector2>().x;
        if (inputValue == 1 || inputValue == -1)
        {
            DisplayedItemsCount += (int)inputValue;
        }
    }
    #endregion

    public void ChangeItemsCountBySlider() => DisplayedItemsCount = (int)Math.Round(itemsCounterSlider.value * maxItemsCount);

    public void StartItemsCountChoosing(StackableItemState item, ChooseItemsCountActions actions)
    {
        actionDescription.text = "Выберите количество предметов";
        itemIcon.sprite = item.Icon;
        maxItemsCount = item.ItemsCount - 1;
        DisplayedItemsCount = 1;       
        itemsMinCountText.text = "1";
        itemsMaxCountText.text = maxItemsCount.ToString();
        currentActions = actions;
        GenerateActionButtons(inputManager.GetActionTips_ChooseItemsCountPanel(actions));
    }

    private void GenerateActionButtons(List<DetailedActionTip> actionTips)
    {
        for (var i = buttonsContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonsContainer.transform.GetChild(i).gameObject);
        }
        for (var i = 0; i < actionTips.Count; i++)
        {
            var actionButton = Instantiate(actionButtonPrefab, buttonsContainer.transform);
            var actionOrderNumber = i;
            actionButton.GetComponent<Button>().onClick.AddListener(() => currentActions.GetActionByOrderNumber(actionOrderNumber, actionOrderNumber == actionTips.Count - 1)());
            actionButton.GetComponent<DetailedActionTipView>().SetInfo(actionTips[i]);
        }
    }

    private void OnEnable()
    {
        transform.SetAsLastSibling();
    }

    private void Awake()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
        gameObject.SetActive(false);
    }
}
