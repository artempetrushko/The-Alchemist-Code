using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SettingData
{   
    public abstract int SettingValuesCount { get; }
    public int CurrentSettingValueIndex { get; protected set; }
    public string Title { get; protected set; }

    public abstract void ApplySettingValue();

    public abstract string ChangeSettingValue(int newSettingValueIndex);
}
