using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingItemTemplateCellPointer : MonoBehaviour, IPointerEnterHandler
{
    private CraftingItemTemplateSubsection templateSubsection;
    private CraftingItemTemplateCellView templateCellView;

    public void OnPointerEnter(PointerEventData eventData)
    {
        templateSubsection.CurrentSelectedCell = templateCellView;
    }

    private void OnEnable()
    {
        templateCellView = GetComponent<CraftingItemTemplateCellView>();
        templateSubsection = GetComponentInParent<CraftingItemTemplateSubsection>();
    }
}
