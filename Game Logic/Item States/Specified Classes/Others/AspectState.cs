using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectState : SingleItemState
{
    public float ProgressionState;
    public float MaxProgression;

    /*public AspectState(ItemData itemData) : base(itemData)
    {
    }*/

    public override object Clone()
    {
        throw new System.NotImplementedException();
    }

    public override Dictionary<string, string> GetItemParams()
    {
        throw new NotImplementedException();
    }

    /*public AspectState(AspectData aspectData)
    {
        //ItemData = aspectData;
        ProgressionState = 25;
        MaxProgression = 100;
    }*/
}
