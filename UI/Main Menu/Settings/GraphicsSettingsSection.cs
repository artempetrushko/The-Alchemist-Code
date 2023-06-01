using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphicsSettingsSection : MonoBehaviour
{
    [SerializeField]
    private SettingPanel settingPanelPrefab;

    private List<SettingData> settingDatas;

    public void SendSettingsData()
    {
        settingDatas = new List<SettingData>()
        {
            new ScreenResolutionSettingData(SettingsManager.GraphicsSettings.ScreenResolutionWidth, SettingsManager.GraphicsSettings.ScreenResolutionHeight),
            new ScreenModeSettingData(SettingsManager.GraphicsSettings.ScreenMode),
            new GraphicsQualitySettingData(SettingsManager.GraphicsSettings.QualityLevel),
            new VSyncSettingData(SettingsManager.GraphicsSettings.VSyncState),
            new BrightnessSettingData(SettingsManager.GraphicsSettings.Brightness),
        };
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        transform.DetachChildren();
        for (var i = 0; i < settingDatas.Count; i++)
        {
            var settingPanel = Instantiate(settingPanelPrefab, transform);
            settingPanel.SetData(settingDatas[i]);
        }
    }

    public void ApplySettings()
    {
        foreach (var settingData in settingDatas)
        {
            settingData.ApplySettingValue();
        }
        SettingsManager.SaveSettings();
    }

    private void OnEnable()
    {
        SendSettingsData();
    }
}
