using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class SettingSlider : MonoBehaviour
{
    public TMP_Text settingName;
    [SerializeField]
    protected Slider settingSlider;
    [SerializeField]
    protected TMP_Text settingValueText;

    public virtual void ChangeSettingValueText()
    {
        settingValueText.text = settingSlider.value * 100 + "%";
    }
}
