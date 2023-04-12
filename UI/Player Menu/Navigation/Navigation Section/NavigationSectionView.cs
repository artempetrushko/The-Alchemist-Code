using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationSectionView : MonoBehaviour
{
    [SerializeField]
    private ActionTipView leftSwitchSectionTipView;
    [SerializeField]
    private ActionTipView rightSwitchSectionTipView;

    public void UpdateTipViews(List<ActionTip> actionTips)
    {
        leftSwitchSectionTipView.SetInfo(actionTips[0]);
        rightSwitchSectionTipView.SetInfo(actionTips[1]);
    }
}
