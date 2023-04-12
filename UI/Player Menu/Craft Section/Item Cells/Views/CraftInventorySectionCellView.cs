using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CraftInventorySectionCellView : CraftItemCellView
{
    [Space]
    [SerializeField]
    protected SimpleItemCellView linkedMainInventoryItemCellView;
    [SerializeField]
    protected GameObject energyCounterContainer;
    [SerializeField]
    protected Image enduranceBar;
    [SerializeField]
    protected EnduranceBarData enduranceBarStates;
    [SerializeField]
    protected ItemDescriptionPanel itemDescriptionPanelPrefab;

    protected ItemDescriptionPanel currentDescriptionPanel;
    protected Image enduranceFillBar;

    public SimpleItemCellView LinkedMainInventoryItemCellView
    {
        get => linkedMainInventoryItemCellView;
        set => linkedMainInventoryItemCellView = value;
    }

    public void EnableDescriptionPanel()
    {
        currentDescriptionPanel = Instantiate(itemDescriptionPanelPrefab, uiManager.transform);
        StartCoroutine(currentDescriptionPanel.Activate_COR(this));
    }

    public void DisableDescriptionPanel()
    {
        if (currentDescriptionPanel != null)
        {
            Destroy(currentDescriptionPanel.gameObject);
        }
    }

    public void ToggleDescriptionPanelState(bool isEnable)
    {
        if (isEnable)
        {
            EnableDescriptionPanel();
        }
        else
        {
            DisableDescriptionPanel();
        }
    }

    protected void SetEnduranceValue(float endurancePercentage)
    {
        enduranceBar.gameObject.SetActive(true);
        enduranceFillBar.fillAmount = endurancePercentage;
        SetEnduranceBarColor();
    }

    protected void SetEnduranceBarColor() => enduranceFillBar.color = enduranceBarStates.GetEnduranceBarColor(enduranceFillBar.fillAmount * 100);

    protected void SetEnergyCount(ItemState item)
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
        enduranceFillBar = enduranceBar.transform.GetChild(0).GetComponent<Image>();
        if (GetComponent<ItemCellContainer>().IsItemPlaceEmpty)
        {
            DisableInfoElements();
        }
    }
}
