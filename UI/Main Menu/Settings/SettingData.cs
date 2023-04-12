using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingData
{
    public List<string> SettingValues { get; private set; }
    public Action<string> ApplySettingAction { get; private set; }

    public SettingData(List<string> settingValues, Action<string> applySettingAction)
    {
        SettingValues = settingValues;
        ApplySettingAction = applySettingAction;
    }
}
