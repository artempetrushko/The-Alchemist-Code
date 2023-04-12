using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerMenuSectionNavigation : MonoBehaviour
{
    protected PlayerMenuSectionSubsection currentSubsection;
    protected PlayerInputManager inputManager;

    public abstract PlayerMenuSectionSubsection CurrentSubsection { get; set; } 

    public void NavigateSubsections(InputAction.CallbackContext context) => CurrentSubsection.Navigate(context);

    protected void OnEnable()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
    }
}
