using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private DetailedActionTipView tipView;
    [SerializeField]
    private Color normalStateContentColor;
    [SerializeField]
    private Color selectedStateContentColor;    

    public void OnPointerEnter(PointerEventData eventData)
    {
        tipView.ChangeContentColor(selectedStateContentColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tipView.ChangeContentColor(normalStateContentColor);
    }

    private void OnEnable()
    {
        tipView.ChangeContentColor(normalStateContentColor);
    }
}
