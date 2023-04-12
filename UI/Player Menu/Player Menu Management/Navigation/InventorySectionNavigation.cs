using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySectionNavigation : PlayerMenuSectionNavigation
{
    [SerializeField]
    private ItemsConcatination itemsConcatinationModule;

    public override PlayerMenuSectionSubsection CurrentSubsection
    {
        get => currentSubsection;
        set
        {
            if (currentSubsection != null)
            {
                currentSubsection.StopNavigation();
            }
            currentSubsection = value;
            if (!itemsConcatinationModule.IsInteractionStarted)
            {
                inputManager.SwitchActionMap_UI_Inventory();
            }          
        }
    }
}
