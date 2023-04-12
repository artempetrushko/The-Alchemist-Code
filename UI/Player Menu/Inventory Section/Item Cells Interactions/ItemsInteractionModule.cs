using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class ItemsInteractionModule : MonoBehaviour
{
    [SerializeField]
    protected Image interactionProcessBackground;

    protected PlayerInputManager inputManager;
    protected ItemCellContainer currentItemCell;

    public bool IsInteractionStarted { get; protected set; } = false;

    public abstract void StartInteraction(ItemCellContainer currentItemCell);

    public abstract void Execute();

    protected abstract void FinishInteraction();

    public void Execute(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Execute();
        }
    }

    public void FinishInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FinishInteraction();
        }
    }

    protected virtual void Start()
    {
        inputManager = FindObjectOfType<PlayerInputManager>();
    }
}
