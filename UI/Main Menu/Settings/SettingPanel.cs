using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text settingValueText;
    [SerializeField]
    protected Button leftChooseSettingValueButton;
    [SerializeField]
    protected Button rightChooseSettingValueButton;

    protected List<string> settingValues;
    protected Action<string> applySettingAction;
    protected int currentSettingValueIndex;

    public int CurrentSettingValueIndex
    {
        get => currentSettingValueIndex;
        private set
        {
            if (value >= 0 && value < settingValues.Count)
            {
                currentSettingValueIndex = value;
                settingValueText.text = settingValues[currentSettingValueIndex];
                ToggleChooseSettingValueButtonState(leftChooseSettingValueButton, value > 0);
                ToggleChooseSettingValueButtonState(rightChooseSettingValueButton, value < settingValues.Count - 1);
            }
        }
    }

    public void ShiftNeighboringSettingValue(int offset) => CurrentSettingValueIndex += offset;

    public void ApplySettingValue() => applySettingAction.Invoke(settingValues[CurrentSettingValueIndex]);

    public void SetData(SettingData settingData)
    {
        settingValues = settingData.SettingValues;
        applySettingAction = settingData.ApplySettingAction;
    }  

    protected void ToggleChooseSettingValueButtonState(Button button, bool isEnable)
    {
        button.enabled = isEnable;
        button.GetComponent<Image>().enabled = isEnable;
    }
}
