using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingItemTemplateCellView : CraftItemCellView
{
    [Space]
    [SerializeField]
    private Image requiredItemsIcon;
    [SerializeField]
    private CraftingItemTemplateCellCounterView itemsCounterView;
    [SerializeField]
    private Button returnToInventoryButton;

    public CraftingItemTemplateCellCounterView ItemsCounterView => itemsCounterView;

    public override void UpdateInfoElementsState(ItemState inventoryItem)
    {
        requiredItemsIcon.gameObject.SetActive(false);
        returnToInventoryButton.gameObject.SetActive(true);
        GetComponentInParent<CraftingItemTemplate>().SendIngredientCellsStates();
    }

    public override void DisableInfoElements()
    {
        requiredItemsIcon.gameObject.SetActive(true);
        returnToInventoryButton.gameObject.SetActive(false);
        GetComponentInParent<CraftingItemTemplate>().SendIngredientCellsStates();
    }

    protected override void OnEnable()
    {
        returnToInventoryButton.onClick.AddListener(() => GetComponentInParent<ItemCreationSectionNavigation>().ReturnItemFromCraftingTemplateToInventory());
    }
}
