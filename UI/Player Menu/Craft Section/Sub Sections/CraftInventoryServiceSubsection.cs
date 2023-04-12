using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftInventoryServiceSubsection : CraftInventorySubsection
{
    [Space]
    [SerializeField]
    private CraftInventoryCategoriesNavigation inventoryCategoriesNavigation;

    public override void Navigate(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void ResumeNavigation()
    {
        throw new System.NotImplementedException();
    }

    public override void StartNavigation()
    {
        inventoryCategoriesNavigation.StartCurrentCategoryNavigation(InventoryItemSelectionMode.Normal);
    }

    public override void StopNavigation()
    {
        throw new System.NotImplementedException();
    }
}
