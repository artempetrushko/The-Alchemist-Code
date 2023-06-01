using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VSyncSettingData : SettingData
{
    private List<Tuple<int, string>> settingValues;

    public override int SettingValuesCount => settingValues.Count;

    public VSyncSettingData(int startVSyncState)
    {
        Title = "Вертикальная синхронизация";
        settingValues = new List<Tuple<int, string>>()
        {
            Tuple.Create(0, "Выкл."),
            Tuple.Create(1, "Вкл.")
        };
        CurrentSettingValueIndex = settingValues.IndexOf(settingValues.Where(value => value.Item1 == startVSyncState).First());
    }

    public override void ApplySettingValue()
    {
        QualitySettings.vSyncCount = settingValues[CurrentSettingValueIndex].Item1;
        SettingsManager.GraphicsSettings.VSyncState = QualitySettings.vSyncCount;
    }

    public override string ChangeSettingValue(int newSettingValueIndex)
    {
        CurrentSettingValueIndex = newSettingValueIndex;
        return settingValues[CurrentSettingValueIndex].Item2;
    }
}
