using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowMissileState : StackableItemState
{
    public float PenetratingPowerCoefficient { get; set; }
    public List<RequiredComponentPartState> ComponentParts { get; set; } = new List<RequiredComponentPartState>();

    public CrossbowMissileState(CrossbowMissileData missileData) : base(missileData)
    {
        ItemData = missileData;
        
        foreach (var component in missileData.BaseComponentParts)
        {
            ComponentParts.Add(new RequiredComponentPartState(component));
        }
    }

    public override object Clone()
    {
        throw new NotImplementedException();
    }

    public override Dictionary<string, string> GetItemParams()
    {
        return new Dictionary<string, string>();
    }
}
