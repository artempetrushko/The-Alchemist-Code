using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenModeSettingData : SettingData
{
    private List<Tuple<FullScreenMode, string>> settingValues;

    public override int SettingValuesCount => settingValues.Count;

    public ScreenModeSettingData(FullScreenMode startScreenMode)
    {
        Title = "Оконный режим";
        settingValues = new List<Tuple<FullScreenMode, string>>()
        {
             Tuple.Create(FullScreenMode.ExclusiveFullScreen, "Полноэкранный"),
             Tuple.Create(FullScreenMode.FullScreenWindow, "Оконный (без рамки)"),
             Tuple.Create(FullScreenMode.Windowed, "Оконный")
        };
        CurrentSettingValueIndex = settingValues.IndexOf(settingValues.Where(value => value.Item1 == startScreenMode).First());
    }   

    public override void ApplySettingValue()
    {
        Screen.fullScreenMode = settingValues[CurrentSettingValueIndex].Item1;
        SettingsManager.GraphicsSettings.ScreenMode = Screen.fullScreenMode;
    }

    public override string ChangeSettingValue(int newSettingValueIndex)
    {
        CurrentSettingValueIndex = newSettingValueIndex;
        return settingValues[CurrentSettingValueIndex].Item2;
    }
}
