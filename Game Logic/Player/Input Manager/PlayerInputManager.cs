using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Gamepad Buttons Icons")]
    [SerializeField]
    private Sprite northButtonIcon;
    [SerializeField]
    private Sprite southButtonIcon;
    [SerializeField]
    private Sprite westButtonIcon;
    [SerializeField]
    private Sprite eastButtonIcon;
    [SerializeField]
    private Sprite dPadArrowUpButtonIcon;
    [SerializeField]
    private Sprite dPadArrowDownButtonIcon;
    [SerializeField]
    private Sprite dPadArrowLeftButtonIcon;
    [SerializeField]
    private Sprite dPadArrowRightButtonIcon;

    private PlayerInput playerInput;
    private PlayerInputActions playerActions;
    private NavigationSectionView navigationSection;
    private NavigationTipsSection navigationTipsSection;
    private PlayerInteractor playerInteractor;
    private List<Tuple<string, InputAction>> currentInputActions;

    public PlayerInputActions PlayerActions => playerActions;

    #region Action Maps Switch (Player)
    public void SwitchActionMap_Player() => SwitchActionMap("Player");

    public void SwitchActionMap_Player(InputAction.CallbackContext context) => SwitchActionMap("Player");
    #endregion

    #region Action Maps Switch (HUD)
    public void SwitchActionMap_UI_ItemsContainer() => SwitchActionMap("UI (Items Container)");
    #endregion

    #region Action Maps Switch (UI Player Menu)
    public void SwitchActionMap_UI_Inventory(InputAction.CallbackContext context) => SwitchActionMap_UI_Inventory();

    public void SwitchActionMap_UI_Inventory() => SwitchActionMap_PlayerMenu("Player Menu (Inventory)", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Взаимодействовать", playerActions.PlayerMenuInventory.PressItemCell),
        Tuple.Create("Переместить предмет", playerActions.PlayerMenuInventory.StartMovingItemCell)
    });

    public void SwitchActionMap_UI_Inventory_QuickAccessCellBinding() => SwitchActionMap_PlayerMenu("Player Menu (Inventory (Quick Access Cell Binding))", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Отменить", playerActions.PlayerMenuInventoryQuickAccessCellBinding.FinishCellBinding),
        Tuple.Create("Назначить", playerActions.PlayerMenuInventoryQuickAccessCellBinding.BindCell)       
    });

    public void SwitchActionMap_UI_Inventory_WeaponEquipping() => SwitchActionMap_PlayerMenu("Player Menu (Inventory (Weapon Equipping))", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Отменить", playerActions.PlayerMenuInventoryWeaponEquipping.FinishCellSelection),
        Tuple.Create("Снарядить", playerActions.PlayerMenuInventoryWeaponEquipping.SelectCell)       
    });

    public void SwitchActionMap_UI_Inventory_ItemsConcatination() => SwitchActionMap_PlayerMenu("Player Menu (Inventory (Items Concatination))", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Отменить", playerActions.PlayerMenuInventoryItemsConcatination.FinishCellSelection),
        Tuple.Create("Объединить", playerActions.PlayerMenuInventoryItemsConcatination.SelectCell)        
    });

    public void SwitchActionMap_UI_ItemCellActionsMenu() => SwitchActionMap_PlayerMenu("Player Menu (Item Cell Actions Menu)", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Назад", playerActions.PlayerMenuItemCellActionsMenu.QuitItemCellActionsMenu),
        Tuple.Create("Выбрать", playerActions.PlayerMenuItemCellActionsMenu.Press)
    });

    public void SwitchActionMap_UI_ItemCellMoving() => SwitchActionMap_PlayerMenu("Player Menu (Item Cell Moving)", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Положить", playerActions.PlayerMenuItemCellMoving.PutCellDown)
    });

    public void SwitchActionMap_UI_ChooseItemsCountPanel() => SwitchActionMap("Player Menu (Choose Items Count Panel)");

    public void SwitchActionMap_UI_CraftSection_Recipes() => SwitchActionMap_PlayerMenu("Player Menu (Craft Section (Recipes))", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Выбрать", playerActions.PlayerMenuCraftSectionRecipes.Select)
    });

    public void SwitchActionMap_UI_CraftSection_EnergyCells() => SwitchActionMap_PlayerMenu("Player Menu (Craft Section (Energy Cells))", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Выбрать", playerActions.PlayerMenuCraftSectionEnergyCells.Select),
        Tuple.Create("Перейти к ингредиентам", playerActions.PlayerMenuCraftSectionEnergyCells.GoToCraftingItemTemplate),
        Tuple.Create("(удерж.) Изготовить", playerActions.PlayerMenuCraftSectionEnergyCells.CreateItem)
    });

    public void SwitchActionMap_UI_CraftSection_CraftingItemTemplate() => SwitchActionMap_PlayerMenu("Player Menu (Craft Section (Crafting Item Template))", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Назад", playerActions.PlayerMenuCraftSectionCraftingItemTemplate.ReturnToEnergyCells),
        Tuple.Create("Очистить", playerActions.PlayerMenuCraftSectionCraftingItemTemplate.ReturnItemToInventory),
        Tuple.Create("Выбрать", playerActions.PlayerMenuCraftSectionCraftingItemTemplate.Select),      
        Tuple.Create("(удерж.) Изготовить", playerActions.PlayerMenuCraftSectionCraftingItemTemplate.CreateItem)
    });

    public void SwitchActionMap_UI_CraftSection_Inventory() => SwitchActionMap_PlayerMenu("Player Menu (Craft Section (Inventory))", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Выбрать", playerActions.PlayerMenuCraftSectionInventory.Select)
    });

    public void SwitchActionMap_UI_CraftSection_Inventory_ItemSelection() => SwitchActionMap_PlayerMenu("Player Menu (Craft Section (Inventory (Item Selection)))", new List<Tuple<string, InputAction>>()
    {
        Tuple.Create("Назад", playerActions.PlayerMenuCraftSectionInventoryItemSelection.ReturnToItemCreationSubsection),
        Tuple.Create("Выбрать", playerActions.PlayerMenuCraftSectionInventoryItemSelection.Select)
    });

    public void SwitchActionMap_UI_AlchemistrySection_Recipes() => SwitchActionMap("Player Menu (Alchemistry Section (Recipes))");

    public void SwitchActionMap_UI_Settings() => SwitchActionMap("Player Menu (Settings)");
    #endregion

    public void UpdateCurrentActionTips()
    {
        if (currentInputActions != null)
        {
            switch (playerInput.currentActionMap.name)
            {
                case "Player":
                    playerInteractor.UpdateActionTips_InteractiveObject();
                    break;

                case "UI (Items Container)":
                    playerInteractor.UpdateActionTips_UI_ItemsContainer(CreateDetailedActionTips(currentInputActions)); 
                    break;

                default:
                    navigationTipsSection.UpdateContent(CreateDetailedActionTips(currentInputActions));
                    break;
            }
        }      
    }

    public List<DetailedActionTip> GetActionTips_UI_ItemsContainer()
    {
        currentInputActions = new List<Tuple<string, InputAction>>()
        {
            Tuple.Create("Взять", playerActions.UIItemsContainer.Take),
            Tuple.Create("Взять всё", playerActions.UIItemsContainer.TakeAll),
            Tuple.Create("Закрыть", playerActions.UIItemsContainer.CloseContainer)
        };
        return CreateDetailedActionTips(currentInputActions);
    }

    public List<DetailedActionTip> GetActionTips_InteractiveObject_ItemContainer()
    {
        currentInputActions = new List<Tuple<string, InputAction>>()
        {
            Tuple.Create("Открыть", playerActions.Player.Interact)
        };
        return CreateDetailedActionTips(currentInputActions);       
    }

    public List<DetailedActionTip> GetActionTips_ChooseItemsCountPanel(ChooseItemsCountActions actions)
    {
        currentInputActions = new List<Tuple<string, InputAction>>()
        {
            Tuple.Create("Выбрать", playerActions.PlayerMenuChooseItemsCountPanel.Confirm),
            Tuple.Create("Закрыть", playerActions.PlayerMenuChooseItemsCountPanel.Cancel)
        };
        if (actions.ConfirmAll != null)
        {
            currentInputActions.Insert(1, Tuple.Create("Выбрать всё", playerActions.PlayerMenuChooseItemsCountPanel.ConfirmAll));
        }
        return CreateDetailedActionTips(currentInputActions);
    }

    private void SwitchActionMap_PlayerMenu(string newActionMapName, List<Tuple<string, InputAction>> actions)
    {
        SwitchActionMap(newActionMapName, actions);
        navigationTipsSection.UpdateContent(CreateDetailedActionTips(currentInputActions));
    }

    private void SwitchActionMap(string newActionMapName)
    {       
        playerInput.SwitchCurrentActionMap(newActionMapName);
        Debug.Log("Текущая Action Map: " + playerInput.currentActionMap.name);
    }

    private void SwitchActionMap(string newActionMapName, List<Tuple<string, InputAction>> actions)
    {
        SwitchActionMap(newActionMapName);
        currentInputActions = actions;     
    }

    /*private void UpdateActionTips_NavigationSection()
    {
        navigationSection.UpdateTipViews(new List<ActionTip>()
        {
            playerActions.PlayerMenuInventory.NavigatePlayerMenu
        });
    }

    private List<ActionTip> CreateActionTips(List<InputAction> actions)
    {
        var actionTips = new List<DetailedActionTip>();
        Action<InputAction> actionTipAddingFunction = playerInput.currentControlScheme switch
        {
            "Gamepad" => (action) =>
            {
                actionTips.Add(new ActionTip(GetGamepadButtonIcon(action.Item2)));
            }
            ,
            "KeyboardMouse" => (action) =>
            {
                actionTips.Add(new ActionTip(action.Item2.controls[0].displayName));
            }
        };
        foreach (var action in actions)
        {
            actionTipAddingFunction.Invoke(action);
        }
        return actionTips;
    }*/

    private List<DetailedActionTip> CreateDetailedActionTips(List<Tuple<string, InputAction>> actions)
    {
        var actionTips = new List<DetailedActionTip>();
        Action<Tuple<string, InputAction>> actionTipAddingFunction = playerInput.currentControlScheme switch
        {
            "Gamepad" => (action) =>
            {
                actionTips.Add(new DetailedActionTip(action.Item1, GetGamepadButtonIcon(action.Item2)));
            }
            ,
            "KeyboardMouse" => (action) =>
            {
                actionTips.Add(new DetailedActionTip(action.Item1, action.Item2.controls[0].displayName));
            }
        };       
        foreach (var action in actions)
        {
            actionTipAddingFunction.Invoke(action);
        }
        return actionTips;
    }

    private Sprite GetGamepadButtonIcon(InputAction inputAction)
    {
        var actionControl = inputAction.controls.Where(x => x.device.name.Contains("Gamepad")).First();
        return actionControl.displayName switch
        {
            "Button North" => northButtonIcon,
            "Triangle" => northButtonIcon,

            "Button South" => southButtonIcon,
            "Cross" => southButtonIcon,

            "Button East" => eastButtonIcon,
            "Circle" => eastButtonIcon,

            "Button West" => westButtonIcon,
            "Square" => westButtonIcon,

            "D-Pad Up" => dPadArrowUpButtonIcon,
            "D-Pad Down" => dPadArrowDownButtonIcon,
            "D-Pad Left" => dPadArrowLeftButtonIcon,
            "D-Pad Right" => dPadArrowRightButtonIcon
        };
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
        playerInput.actions["Close Container"].performed += SwitchActionMap_Player;
        playerInput.actions["Quit Item Cell Actions Menu"].performed += SwitchActionMap_UI_Inventory;
    }

    private void OnDisable()
    {
        playerActions.Disable();
        playerInput.actions["Close Container"].performed -= SwitchActionMap_Player;
        playerInput.actions["Quit Item Cell Actions Menu"].performed -= SwitchActionMap_UI_Inventory;
    }

    private void Awake()
    {      
        playerActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        navigationTipsSection = FindObjectOfType<NavigationTipsSection>();
        navigationSection = FindObjectOfType<NavigationSectionView>();
        playerInteractor = FindObjectOfType<PlayerInteractor>();
    }
}
