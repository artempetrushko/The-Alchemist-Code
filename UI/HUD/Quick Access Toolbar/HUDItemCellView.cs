using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDItemCellView : ItemCellView
{
    [Space]
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text itemsCounter;
    [SerializeField]
    protected AspectsPanel aspectIcons;
    [SerializeField]
    protected Image enduranceBar;
    [SerializeField]
    protected EnduranceBarData enduranceBarStates;

    protected Image enduranceFillBar;

    public override void UpdateInfoElementsState(ItemState inventoryItem)
    {
        SetIcon(inventoryItem.Icon);
        if (inventoryItem is StackableItemState)
        {
            SetItemsCount((inventoryItem as StackableItemState).ItemsCount);
        }
        if (inventoryItem is EquipmentState)
        {
            var equipment = inventoryItem as EquipmentState;
            SetEnduranceValue((float)equipment.Endurance / equipment.MaxEndurance);
        }
    }

    public override void DisableInfoElements()
    {
        itemImage.gameObject.SetActive(false);
        itemsCounter.gameObject.SetActive(false);
        enduranceBar.gameObject.SetActive(false);
    }

    private void SetIcon(Sprite icon)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = icon;
    }

    private void SetItemsCount(int itemsCount)
    {
        itemsCounter.gameObject.SetActive(true);
        itemsCounter.text = "x" + itemsCount.ToString();
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
    }
}
