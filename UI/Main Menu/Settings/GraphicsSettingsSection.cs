using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphicsSettingsSection : MonoBehaviour
{
    private void SendSettingsData()
    {
        var settingsData = new List<SettingData>()
        {
            GetResolutionSettingData(),
            GetScreenModeSettingData(),
            GetGraphicsQualitySettingData(),
            GetVSyncSettingData(),
            GetBrightnessSettingData()
        };
        var settingPanels = GetComponentsInChildren<SettingPanel>();
        for (var i = 0; i < settingPanels.Length; i++)
        {
            settingPanels[i].SetData(settingsData[i]);
        }
    }

    private void SetStartGraphicsSettingsValues()
    {
        
    }

    private SettingData GetResolutionSettingData()
    {
        return new SettingData(Screen.resolutions.Select(resolution => resolution.width + " x " + resolution.height).ToList(), new Action<string>(
                (resolution) =>
                {
                    var resolutionData = resolution.Split(" x ");
                    Screen.SetResolution(int.Parse(resolutionData[0]), int.Parse(resolutionData[1]), true);
                    SaveManager.GraphicsSettings.ScreenResolution = Screen.currentResolution;
                }));
    }

    private SettingData GetScreenModeSettingData()
    {
        return new SettingData(new List<string>()
        {
            "Полноэкранный",
            "Оконный (без рамки)",            
            "Оконный"
        }, new Action<string>(
            (screenMode) =>
            {
                Screen.fullScreenMode = screenMode switch
                {
                    "Полноэкранный" => FullScreenMode.ExclusiveFullScreen,
                    "Оконный (без рамки)" => FullScreenMode.FullScreenWindow,                   
                    "Оконный" => FullScreenMode.Windowed
                };
                SaveManager.GraphicsSettings.ScreenMode = Screen.fullScreenMode;
            }));
    }

    private SettingData GetGraphicsQualitySettingData()
    {
        return new SettingData(new List<string>()
        {
            "Низкое",
            "Среднее",
            "Высокое"
        }, new Action<string>(
            (graphicsQuality) =>
            {
                QualitySettings.SetQualityLevel(graphicsQuality switch
                {
                    "Низкое" => 0,
                    "Среднее" => 1,
                    "Высокое" => 2
                });
                SaveManager.GraphicsSettings.QualityLevel = QualitySettings.GetQualityLevel();
            }));
    }

    private SettingData GetVSyncSettingData()
    {
        return new SettingData(new List<string>()
        {
            "Выкл.",
            "Вкл.",
        }, new Action<string>(
            (vsyncState) =>
            {
                QualitySettings.vSyncCount = vsyncState switch
                {
                    "Выкл." => 0,
                    "Вкл." => 1,
                };
                SaveManager.GraphicsSettings.VSyncState = QualitySettings.vSyncCount;
            }));
    }

    private SettingData GetBrightnessSettingData()
    {
        var brightnessLevelsCount = 10;
        var brightnessSettingValues = new List<string>();
        for (var i = 1; i <= brightnessLevelsCount; i++)
        {
            brightnessSettingValues.Add(i.ToString());
        }
        return new SettingData(brightnessSettingValues, new Action<string>(
            (brightness) =>
            {
                Screen.brightness = 1 / brightnessLevelsCount * int.Parse(brightness);
                SaveManager.GraphicsSettings.Brightness = Screen.brightness;
            }));
    }

    private void Start()
    {
        SendSettingsData();
    }
}
