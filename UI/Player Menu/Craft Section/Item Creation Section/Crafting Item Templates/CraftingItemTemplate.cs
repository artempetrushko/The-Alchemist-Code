using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CraftingItemTemplateSubsection))]
public class CraftingItemTemplate : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon;
    [SerializeField]
    private GameObject ingredientCells;

    private CraftManager craftManager;

    public void SendIngredientCellsStates()
    {
        var ingredientStates = ingredientCells.GetComponentsInChildren<CraftingItemTemplateCellView>().ToList();
        craftManager.UpdateRequiredItemsProgress(ingredientStates);
    }

    private void OnEnable()
    {
        craftManager = FindObjectOfType<CraftManager>();
    }
}
