using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveObjectInfoView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text interactiveObjectTitle;
    [SerializeField]
    private PlayerActionsViewContainer playerActionsContainer;

    public void SetInfo(string objectTitle)
    {
        interactiveObjectTitle.text = objectTitle;
    }

    public void SetInfo(string objectTitle, List<DetailedActionTip> actionTips)
    {
        SetInfo(objectTitle);
        playerActionsContainer.ShowPlayerActionsInfo(actionTips);
    }

    public void SetVisibility(bool isVisible)
    {
        gameObject.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
    }
}
