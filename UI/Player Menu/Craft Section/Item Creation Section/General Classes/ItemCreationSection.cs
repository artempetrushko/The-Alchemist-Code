using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreationSection : MonoBehaviour
{
    [SerializeField]
    private GameObject creatingItemPlace;
    [SerializeField]
    private Image craftProgressRound;
    [SerializeField]
    private GameObject energyCells;
    [SerializeField]
    private CraftDescriptionPanel craftDescriptionPanel;

    private CraftingItemTemplate currentCraftingItemTemplate;

    public CraftDescriptionPanel CraftDescriptionPanel => craftDescriptionPanel;
    public Image CraftProgressRound => craftProgressRound;

    public List<ItemInfo> EnergyCellsItems => energyCells.GetComponentsInChildren<EnergyCellContainer>()
        .Select(container => container.ContainedItem)
        .Where(cell => cell != null)
        .ToList();

    public CraftingItemTemplate CurrentCraftingItemTemplate
    {
        get => currentCraftingItemTemplate;
        set => currentCraftingItemTemplate = value;
    }

    public void ShowCraftTemplate(RecipeData recipe)
    {
        DeleteCraftTemplate();
        CurrentCraftingItemTemplate = Instantiate(recipe.Template, creatingItemPlace.transform);
        GetComponent<ItemCreationSectionPointer>().enabled = false;
    }

    public void DeleteCraftTemplate()
    {
        var currentTemplate = creatingItemPlace.GetComponentInChildren<CraftingItemTemplate>();
        if (currentTemplate != null)
        {
            Destroy(currentTemplate.gameObject);
        }       
    }
}
