using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionInfoView : MonoBehaviour
{
    [SerializeField]
    private Image actionKeyImage;
    [SerializeField]
    private TMP_Text actionDescriptionText;

    public void SetInfo(string actionDescription, string actionKeyName)
    {
        actionKeyImage.GetComponentInChildren<TMP_Text>().text = actionKeyName;
        actionDescriptionText.text = actionDescription;
    }
}
