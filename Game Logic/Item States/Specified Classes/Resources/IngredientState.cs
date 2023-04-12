using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientState : ResourceState
{
    public IngredientState(IngredientData ingredient) : base(ingredient)
    {
        ItemData = ingredient;
    }

    protected IngredientState() { }

    public override object Clone() => new IngredientState()
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
