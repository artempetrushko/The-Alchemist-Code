using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrightnessSettingData : SettingData
{
    private List<Tuple<int, string>> settingValues;

    public override int SettingValuesCount => settingValues.Count;

    public BrightnessSettingData(float startBrightnessValue)
    {
        Title = "яркость";
        settingValues = CreateBrightnessValues(10);
        CurrentSettingValueIndex = settingValues.IndexOf(settingValues.Where(value => value.Item1 == Math.Floor(startBrightnessValue * settingValues.Count)).First());
    }

    public override void ApplySettingValue()
    {
        var brightness = 1f / settingValues.Count * settingValues[CurrentSettingValueIndex].Item1;
        RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1);
    }

    public override string ChangeSettingValue(int newSettingValueIndex)
    {
        CurrentSettingValueIndex = newSettingValueIndex;
        return settingValues[CurrentSettingValueIndex].Item2;
    }

    private List<Tuple<int, string>> CreateBrightnessValues(int brightnessLevelsCount)
    {
        var brightnessValues = new List<Tuple<int, string>>();
        for (var i = 1; i <= brightnessLevelsCount; i++)
        {
            brightnessValues.Add(Tuple.Create(i, i.ToString()));
        }
        return brightnessValues;
    }  
}
