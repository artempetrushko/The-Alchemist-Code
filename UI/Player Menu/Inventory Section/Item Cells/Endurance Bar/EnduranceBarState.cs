using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnduranceBarState
{
    public Color BarColor;
    public float BarStateMinPercentage;
    public float BarStateMaxPercentage;

    public EnduranceBarState(Color barColor, float barStateMinValue, float barStateMaxValue)
    {
        BarColor = barColor;
        BarStateMinPercentage = barStateMinValue;    
        BarStateMaxPercentage = barStateMaxValue;
    }
}
