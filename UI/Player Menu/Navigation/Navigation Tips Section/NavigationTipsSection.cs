using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationTipsSection : MonoBehaviour
{
    [SerializeField]
    private DetailedActionTipView actionTipViewPrefab;

    public void UpdateContent(List<DetailedActionTip> actionTips)
    {
        ClearContent();
        foreach (var tip in actionTips)
        {
            var actionTipView = Instantiate(actionTipViewPrefab, transform);
            actionTipView.SetInfo(tip);
        }
    }

    public void ClearContent()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
