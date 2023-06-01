using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenResolutionSettingData : SettingData
{
    private List<Tuple<Resolution, string>> settingValues;

    public override int SettingValuesCount => settingValues.Count;

    public ScreenResolutionSettingData(int startResolutionWidth, int startResolutionHeight)
    {
        Title = "Разрешение";
        settingValues = Screen.resolutions
            .Where(resolution => Math.Abs((float)resolution.width / resolution.height - (16f / 9)) < 1e-5)
            .Select(resolution => Tuple.Create(resolution, resolution.width + " x " + resolution.height))
            .ToList();
        CurrentSettingValueIndex = settingValues.IndexOf(settingValues
            .Where(value => value.Item1.width == startResolutionWidth && value.Item1.height == startResolutionHeight)
            .First());
    }

    public override void ApplySettingValue()
    {
        var currentResolution = settingValues[CurrentSettingValueIndex].Item1;
        Screen.SetResolution(currentResolution.width, currentResolution.height, true);
        SettingsManager.GraphicsSettings.ScreenResolutionWidth = Screen.currentResolution.width;
        SettingsManager.GraphicsSettings.ScreenResolutionHeight = Screen.currentResolution.height;
    }

    public override string ChangeSettingValue(int newSettingValueIndex)
    {
        CurrentSettingValueIndex = newSettingValueIndex;
        return settingValues[CurrentSettingValueIndex].Item2;
    }
}
