using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    private InteractiveObject currentInteractiveObject;
    private InteractiveObjectPanel interactiveObjectPanel;
    private PlayerInputManager player;

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (currentInteractiveObject)
            {
                case ItemsContainer:
                    (currentInteractiveObject as ItemsContainer).OpenContainer();
                    player.SwitchActionMap_UI_ItemsContainer();
                    UpdateActionTips_UI_ItemsContainer(player.GetActionTips_UI_ItemsContainer());
                    break;
            }
        }        
    }

    public void UpdateActionTips_UI_ItemsContainer(List<DetailedActionTip> actionTips) => interactiveObjectPanel.EnablePostInteractionPlayerActionsContainer(actionTips);

    public void UpdateActionTips_InteractiveObject()
    {
        if (currentInteractiveObject != null)
        {
            interactiveObjectPanel.SetInfoAndEnable(currentInteractiveObject.Title, currentInteractiveObject.transform, GetPossiblePlayerActions());
        }       
    }

    #region Items Container Actions
    public void PickItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            (currentInteractiveObject as ItemsContainer).PickItemByPressKey();
        }
    }

    public void PickAll(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            (currentInteractiveObject as ItemsContainer).PickAll();
        }    
    }

    public void CloseContainer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            (currentInteractiveObject as ItemsContainer).CloseContainer();
        }      
    }

    public void NavigateContainedItems(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            (currentInteractiveObject as ItemsContainer).NavigateContainedItemsMenu(context.ReadValue<Vector2>());
        }
    }
    #endregion

    private List<DetailedActionTip> GetPossiblePlayerActions()
    {
        switch (currentInteractiveObject)
        {
            case ItemsContainer:
                return player.GetActionTips_InteractiveObject_ItemContainer();
            default:
                return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var potentialInteractiveObject = other.GetComponent<InteractiveObject>();
        if (potentialInteractiveObject != null && currentInteractiveObject == null)
        {
            currentInteractiveObject = potentialInteractiveObject;
            interactiveObjectPanel.SetInfoAndEnable(currentInteractiveObject.Title, currentInteractiveObject.transform, GetPossiblePlayerActions());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentInteractiveObject = null;
        interactiveObjectPanel.DisableUI();
    }

    private void OnEnable()
    {
        player = GetComponent<PlayerInputManager>();
        interactiveObjectPanel = FindObjectOfType<InteractiveObjectPanel>();
    }
}
