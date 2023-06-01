using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnergyCellView : CraftItemCellView, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Space]
    [SerializeField]
    private GameObject energyCounterContainer;
    [SerializeField]
    private GameObject itemsCounterContainer;
    [SerializeField]
    private Button clearCellButton;
    [SerializeField]
    private Image energyBackgroundIcon;
    [Space]
    [SerializeField]
    private EnergyCellView leftNeighboringCell;
    [SerializeField]
    private EnergyCellView rightNeighboringCell;
    [SerializeField]
    private EnergyCellView topNeighboringCell;
    [SerializeField]
    private EnergyCellView bottomNeighboringCell;

    private CraftManager craftManager;
    private ItemCreationSectionNavigation itemCreationSectionNavigation;
    private EnergyCellsSubsection energyCellsSubsection;
    private EnergyCellContainer energyCellContainer;

    public EnergyCellView LeftNeighboringCell => leftNeighboringCell;
    public EnergyCellView RightNeighboringCell => rightNeighboringCell;
    public EnergyCellView TopNeighboringCell => topNeighboringCell;
    public EnergyCellView BottomNeighboringCell => bottomNeighboringCell;

    public override void UpdateInfoElementsState(ItemState inventoryItem)
    {
        if (inventoryItem is StackableItemState)
        {
            SetItemsCount((inventoryItem as StackableItemState).ItemsCount);
        }
        energyBackgroundIcon.gameObject.SetActive(false);
        SetEnergyCount(inventoryItem);
        craftManager.GetEnergyCellsData();
    }

    public override void DisableInfoElements()
    {
        energyBackgroundIcon.gameObject.SetActive(true);
        itemsCounterContainer.SetActive(false);
        energyCounterContainer.SetActive(false);
        craftManager.GetEnergyCellsData();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        itemCreationSectionNavigation.SelectEnergyCell();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        energyCellsSubsection.SelectedEnergyCell = this;
        if (!energyCellContainer.IsItemPlaceEmpty)
        {
            clearCellButton.gameObject.SetActive(true);
        }     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        clearCellButton.gameObject.SetActive(false);
    }

    private void SetItemsCount(int itemsCount)
    {
        itemsCounterContainer.SetActive(true);
        itemsCounterContainer.GetComponentInChildren<TMP_Text>().text = itemsCount.ToString();
    }

    private void SetEnergyCount(ItemState item)
    {
        energyCounterContainer.SetActive(true);
        energyCounterContainer.GetComponentInChildren<TMP_Text>().text = item switch
        {
            StackableItemState => (item as StackableItemState).TotalContainedEnergyCount.ToString(),
            _ => item.ContainedEnergyCount.ToString()
        };
    }

    protected override void OnEnable()
    {
        craftManager = FindObjectOfType<CraftManager>();
        energyCellContainer = GetComponent<EnergyCellContainer>();
        energyCellsSubsection = GetComponentInParent<EnergyCellsSubsection>();
        itemCreationSectionNavigation = GetComponentInParent<ItemCreationSectionNavigation>();
    }
}
