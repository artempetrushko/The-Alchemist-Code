using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMenuManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInputManager playerInputManager;
    [SerializeField]
    private GameObject sectionButtons;
    [SerializeField]
    private GameObject actionBackground;
    [SerializeField]
    private InventoryView inventorySection;
    [SerializeField]
    private CraftSection craftSection;
    [SerializeField]
    private AlchemistrySection alchemistrySection;
    [SerializeField]
    private SettingsSection settingsSection;

    private PlayerMenuSection currentSection;
    private int currentSectionButtonIndex;
    private ItemCellActionMenu currentItemCellActionMenu;

    public PlayerMenuSection CurrentSection
    {
        get => currentSection;
        private set
        {
            if (currentSection != null)
            {
                currentSection.SetVisibility(false);
            }
            currentSection = value;
            currentSection.SetVisibility(true);
        }
    }

    public int CurrentSectionButtonIndex
    {
        get => currentSectionButtonIndex;
        private set
        {
            sectionButtons.transform.GetChild(currentSectionButtonIndex).GetChild(1).gameObject.SetActive(false);
            sectionButtons.transform.GetChild(currentSectionButtonIndex).GetChild(0).gameObject.SetActive(true);
            currentSectionButtonIndex = value;
            sectionButtons.transform.GetChild(currentSectionButtonIndex).GetChild(1).gameObject.SetActive(true);
            sectionButtons.transform.GetChild(currentSectionButtonIndex).GetChild(0).gameObject.SetActive(false);
            ShowCurrentPlayerMenuSection(currentSectionButtonIndex);
        }
    }

    #region Вызовы секций меню
    public void ShowDefaultPlayerMenuSection(InputAction.CallbackContext context) => ShowPlayerMenuSection(context, 0);

    public void ShowInventorySection(InputAction.CallbackContext context) => ShowPlayerMenuSection(context, 0);

    public void ShowCraftSection(InputAction.CallbackContext context) => ShowPlayerMenuSection(context, 1);

    public void ShowAlchemistrySection(InputAction.CallbackContext context) => ShowPlayerMenuSection(context, 2);

    public void ShowSettingsSection(InputAction.CallbackContext context) => ShowPlayerMenuSection(context, 3);

    public void ShowInventorySection() => ShowPlayerMenuSection(0);

    public void ShowCraftSection() => ShowPlayerMenuSection(1);

    public void ShowAlchemistrySection() => ShowPlayerMenuSection(2);

    public void ShowSettingsSection() => ShowPlayerMenuSection(3);
    #endregion

    public void ShowPlayerMenuSection(InputAction.CallbackContext context, int sectionButtonIndex)
    {
        if (context.performed)
        {
            ShowPlayerMenuSection(sectionButtonIndex);
        }       
    }

    public void ShowPlayerMenuSection(int sectionButtonIndex)
    {
        SetVisibility(true);
        CurrentSectionButtonIndex = sectionButtonIndex;
    }

    public void ChangePlayerMenuSection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var inputValue = context.ReadValue<Vector2>().x;
            switch (inputValue)
            {
                case 1:
                    if (CurrentSectionButtonIndex + inputValue >= sectionButtons.transform.childCount)
                    {
                        CurrentSectionButtonIndex = 0;
                    }
                    else
                    {
                        CurrentSectionButtonIndex += (int)inputValue;
                    }
                    break;
                case -1:
                    if (CurrentSectionButtonIndex + inputValue < 0)
                    {
                        CurrentSectionButtonIndex = sectionButtons.transform.childCount - 1;
                    }
                    else
                    {
                        CurrentSectionButtonIndex += (int)inputValue;
                    }
                    break;
            }
        }
    }

    public void HidePlayerMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSection.SetVisibility(false);
            SetVisibility(false);
            playerInputManager.SwitchActionMap_Player();
        }        
    }

    public void PressItemCell(InputAction.CallbackContext context) => ChangeCellPressStateByInput(context, true);

    public void QuitItemCellActionMenu(InputAction.CallbackContext context) => ChangeCellPressStateByInput(context, false);

    public void PressItemCell()
    {
        ChangeCellPressState(true);
    }

    public void QuitItemCellActionMenu()
    {
        ChangeCellPressState(false);
    }

    public void StartItemCellMoving(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inventorySection.GetComponent<ItemCellsMoving>().TryStartCellMoving())
            {
                playerInputManager.SwitchActionMap_UI_ItemCellMoving();
            }          
        }
    }

    public void FinishItemCellMoving(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventorySection.GetComponent<ItemCellsMoving>().FinishCellMoving();
            playerInputManager.SwitchActionMap_UI_Inventory();
        }
    }

    public void NavigateActionsMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentItemCellActionMenu == null)
            {
                currentItemCellActionMenu = FindObjectOfType<ItemCellActionMenu>();
            }
            currentItemCellActionMenu.SelectOtherButton(context.ReadValue<Vector2>());
        }
    }

    public void PressSelectedButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentItemCellActionMenu == null)
            {
                currentItemCellActionMenu = FindObjectOfType<ItemCellActionMenu>();
            }
            currentItemCellActionMenu.CurrentActionButton.onClick.Invoke();
            currentItemCellActionMenu = null;
        }
    }

    public void ChangeCellPressState(bool isPressed)
    {
        var currentCellsSection = GetComponentsInChildren<InventorySectionSubsection>().Where(section => section.SelectedCell != null
                                                                                            && !section.SelectedCell.GetComponent<ItemCellContainer>().IsItemPlaceEmpty)
                                                                             .FirstOrDefault();
        if (currentCellsSection != null)
        {
            currentCellsSection.ChangeCellPressState(isPressed);
            actionBackground.SetActive(isPressed);
            if (isPressed)
            {
                playerInputManager.SwitchActionMap_UI_ItemCellActionsMenu();
            }
            else
            {
                playerInputManager.SwitchActionMap_UI_Inventory();
            }
        }
    }

    private void ShowCurrentPlayerMenuSection(int sectionIndex)
    {
        switch (sectionIndex)
        {
            case 0:
                CurrentSection = inventorySection;
                playerInputManager.SwitchActionMap_UI_Inventory();
                break;

            case 1:
                CurrentSection = craftSection;
                break;

            case 2:
                CurrentSection = alchemistrySection;
                playerInputManager.SwitchActionMap_UI_AlchemistrySection_Recipes();
                break;

            case 3:
                CurrentSection = settingsSection;
                playerInputManager.SwitchActionMap_UI_Settings();
                break;
        }
    }

    private void ChangeCellPressStateByInput(InputAction.CallbackContext context, bool isPressed)
    {
        if (context.performed)
        {
            ChangeCellPressState(isPressed);
        }
    }

    private void SetVisibility(bool isVisible)
    {
        transform.localScale = isVisible ? Vector3.one : Vector3.zero;
    }

    private void OnEnable()
    {
        if (playerInputManager == null)
        {
            playerInputManager = FindObjectOfType<PlayerInputManager>();
        }
        SetVisibility(false);
    }
}
