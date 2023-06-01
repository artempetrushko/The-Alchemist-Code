using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphicsQualitySettingData : SettingData
{
    private List<Tuple<int, string>> settingValues;

    public override int SettingValuesCount => settingValues.Count;

    public GraphicsQualitySettingData(int startQualityLevel)
    {
        Title = "Качество графики";
        settingValues = new List<Tuple<int, string>>()
        {
             Tuple.Create(0, "Низкое"),
             Tuple.Create(1, "Среднее"),
             Tuple.Create(2, "Высокое")
        };
        CurrentSettingValueIndex = settingValues.IndexOf(settingValues.Where(value => value.Item1 == startQualityLevel).First());
    }

    public override void ApplySettingValue()
    {
        QualitySettings.SetQualityLevel(settingValues[CurrentSettingValueIndex].Item1);
        SettingsManager.GraphicsSettings.QualityLevel = QualitySettings.GetQualityLevel();
    }

    public override string ChangeSettingValue(int newSettingValueIndex)
    {
        CurrentSettingValueIndex = newSettingValueIndex;
        return settingValues[CurrentSettingValueIndex].Item2;
    }
}
