using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ItemDescriptionPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text itemTitle;
    [SerializeField]
    private TMP_Text itemDescription;
    [SerializeField]
    private GameObject parametersSection;
    [SerializeField]
    private ItemParameterView parameterViewPrefab;

    private float positionOffsetX = Screen.width * 0.15f;
    private float positionOffsetY = Screen.height * 0.3f;
    private bool isPanelExists;

    public IEnumerator Activate_COR(ItemCellView attachedCell)
    {
        var containedItem = attachedCell.GetComponent<ItemCellContainer>().ContainedItem;
        if (containedItem != null)
        {
            SetInfo(containedItem.ItemState);
            yield return new WaitForSeconds(1f);
            if (isPanelExists)
            {
                transform.position = SetPosition(attachedCell);
                GetComponent<CanvasGroup>().alpha = 1;
            }           
        }
    }

    private void SetInfo(ItemState itemState)
    {
        itemTitle.text = itemState.Title;
        itemDescription.text = itemState.Description;

        foreach (var param in itemState.GetItemParams())
        {
            var parameterView = Instantiate(parameterViewPrefab, parametersSection.transform);
            parameterView.SetInfo(param.Key, param.Value);
        }
    }

    private Vector3 SetPosition(ItemCellView attachedCell)
    {
        var itemCellPosition = attachedCell.transform.position;
        var itemCellRect = attachedCell.GetComponent<RectTransform>().rect;
        var itemDescriptionPanelRect = GetComponent<RectTransform>().rect;

        if (Screen.height - (itemCellPosition.y + itemCellRect.height / 2) > itemDescriptionPanelRect.height + positionOffsetY)
        {
            return itemCellPosition + new Vector3(0, positionOffsetY, 0);
        }
        if (Screen.width - (itemCellPosition.x + itemCellRect.width / 2) > itemDescriptionPanelRect.width + positionOffsetX)
        {
            return itemCellPosition + new Vector3(positionOffsetX, itemCellRect.height, 0);
        }
        if (itemCellPosition.x - itemCellRect.width / 2 > itemDescriptionPanelRect.width + positionOffsetX)
        {
            return itemCellPosition + new Vector3(-positionOffsetX, itemCellRect.height, 0);
        }
        return itemCellPosition + new Vector3(0, -positionOffsetY, 0);
    }    

    private void OnEnable()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        isPanelExists = true;
    }

    private void OnDestroy()
    {
        isPanelExists = false;
    }
}
