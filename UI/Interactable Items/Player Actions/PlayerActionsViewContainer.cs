using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsViewContainer : MonoBehaviour
{
    [SerializeField]
    private DetailedActionTipView playerActionInfoViewPrefab;

    public void ShowPlayerActionsInfo(List<DetailedActionTip> actionTips)
    {
        SetVisibility(true);
        ClearPlayerActionsInfo();

        foreach (var actionTip in actionTips)
        {
            var playerActionInfoView = Instantiate(playerActionInfoViewPrefab, transform);
            playerActionInfoView.SetInfo(actionTip);
        }
    }

    public void ClearPlayerActionsInfo()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void SetVisibility(bool isVisible)
    {
        gameObject.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
    }

    private void Start()
    {
        SetVisibility(false);
    }
}
