using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputManager : MonoBehaviour
{
    [Header("Gamepad Buttons Icons")]
    [SerializeField]
    protected GamepadButtonsIcons gamepadButtonsIcons;

    protected PlayerInput playerInput;
    protected PlayerInputActions playerActions;
    protected NavigationSectionView navigationSection;
    protected NavigationTipsSection navigationTipsSection;
    protected PlayerInteractor playerInteractor;
    protected List<Tuple<string, InputAction>> currentInputActions;

    public PlayerInputActions PlayerActions => playerActions;

    public abstract void UpdateCurrentActionTips();

    public void SwitchActionMap_UI_LoadingScreen() => SwitchActionMap("Loading Screen");

    protected void SwitchActionMap_PlayerMenu(string newActionMapName, List<Tuple<string, InputAction>> actions)
    {
        SwitchActionMap(newActionMapName, actions);
        navigationTipsSection.UpdateContent(CreateDetailedActionTips(currentInputActions));
    }

    protected void SwitchActionMap(string newActionMapName)
    {
        playerInput.SwitchCurrentActionMap(newActionMapName);
        Time.timeScale = newActionMapName.Contains("Player Menu") ? 0 : 1;
        Debug.Log("Текущая Action Map: " + playerInput.currentActionMap.name);
    }

    protected void SwitchActionMap(string newActionMapName, List<Tuple<string, InputAction>> actions)
    {
        SwitchActionMap(newActionMapName);
        currentInputActions = actions;
    }

    /*protected void UpdateActionTips_NavigationSection()
    {
        navigationSection.UpdateTipViews(new List<ActionTip>()
        {
            playerActions.PlayerMenuInventory.NavigatePlayerMenu
        });
    }

    protected List<ActionTip> CreateActionTips(List<InputAction> actions)
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

    protected List<DetailedActionTip> CreateDetailedActionTips(List<Tuple<string, InputAction>> actions)
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

    protected Sprite GetGamepadButtonIcon(InputAction inputAction)
    {
        var actionControl = inputAction.controls.Where(x => x.device.name.Contains("Gamepad")).First();
        return actionControl.displayName switch
        {
            "Button North" => gamepadButtonsIcons.NorthButtonIcon,
            "Triangle" => gamepadButtonsIcons.NorthButtonIcon,

            "Button South" => gamepadButtonsIcons.SouthButtonIcon,
            "Cross" => gamepadButtonsIcons.SouthButtonIcon,

            "Button East" => gamepadButtonsIcons.EastButtonIcon,
            "Circle" => gamepadButtonsIcons.EastButtonIcon,

            "Button West" => gamepadButtonsIcons.WestButtonIcon,
            "Square" => gamepadButtonsIcons.WestButtonIcon,

            "D-Pad Up" => gamepadButtonsIcons.DPadUpButtonIcon,
            "D-Pad Down" => gamepadButtonsIcons.DPadDownButtonIcon,
            "D-Pad Left" => gamepadButtonsIcons.DPadLeftButtonIcon,
            "D-Pad Right" => gamepadButtonsIcons.DPadRightButtonIcon
        };
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
