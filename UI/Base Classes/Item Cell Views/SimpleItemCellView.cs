using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SimpleItemCellView : ItemCellView
{
    [Space]
    [SerializeField]
    protected MechanicsItemCellView linkedMechanicsItemCellView;
    [SerializeField]
    protected AspectsPanel aspectIcons;
    [SerializeField]
    protected Image enduranceBar;
    [SerializeField]
    protected EnduranceBarData enduranceBarStates;
    [SerializeField]
    protected ItemDescriptionPanel itemDescriptionPanelPrefab;

    protected ItemDescriptionPanel currentDescriptionPanel;
    protected Image enduranceFillBar;

    public GameObject AspectIcons => aspectIcons.gameObject;
    public MechanicsItemCellView LinkedMechanicsItemCellView
    {
        get => linkedMechanicsItemCellView;
        set => linkedMechanicsItemCellView = value;
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

    protected void SetAspectsValues(List<AspectState> aspects) => aspectIcons.UpdateAspectsValues(aspects);

    protected void SetEnduranceBarColor() => enduranceFillBar.color = enduranceBarStates.GetEnduranceBarColor(enduranceFillBar.fillAmount * 100);

    protected override void OnEnable()
    {
        enduranceFillBar = enduranceBar.transform.GetChild(0).GetComponent<Image>();
        if (GetComponent<ItemCellContainer>().IsItemPlaceEmpty)
        {
            DisableInfoElements();
        }
    }
}
