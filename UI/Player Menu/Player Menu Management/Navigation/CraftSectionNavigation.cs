using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSectionNavigation : PlayerMenuSectionNavigation
{
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
            switch (currentSubsection)
            {
                case RecipesSubsection:
                    inputManager.SwitchActionMap_UI_CraftSection_Recipes();
                    break;

                case EnergyCellsSubsection:
                    inputManager.SwitchActionMap_UI_CraftSection_EnergyCells();
                    break;

                case CraftingItemTemplateSubsection:
                    inputManager.SwitchActionMap_UI_CraftSection_CraftingItemTemplate();
                    break;

                case CraftInventorySubsection:
                    switch ((currentSubsection as CraftInventorySubsection).CurrentSelectionMode)
                    {
                        case InventoryItemSelectionMode.Normal:
                            inputManager.SwitchActionMap_UI_CraftSection_Inventory();
                            break;

                        case InventoryItemSelectionMode.IngredientSelection:
                            inputManager.SwitchActionMap_UI_CraftSection_Inventory_ItemSelection();
                            break;
                    }
                    break;
            }
        }
    }
}
