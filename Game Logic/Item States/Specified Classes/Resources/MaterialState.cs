using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialState : ResourceState
{
    public MaterialState(MaterialData material) : base(material)
    {
        ItemData = material;
    }

    protected MaterialState() { }

    public override object Clone() => new MaterialState()
    {
        ItemData = ItemData,
        ItemsCount = ItemsCount,
        MaxStackItemsCount = MaxStackItemsCount,
        Description = Description,
        Aspects = Aspects,
        CastingDamage = CastingDamage,
        Effects = Effects,
    };

    public override Dictionary<string, string> GetItemParams()
    {
        return new Dictionary<string, string>();
    }
}
