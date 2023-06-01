using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionState : StackableItemState
{
    public PotionEffect Effect => (ItemData as PotionData).Effect;
    public int EffectPower => (ItemData as PotionData).EffectPower;
    public int EffectDuration => (ItemData as PotionData).EffectDurationInSeconds;

    public PotionState(PotionData potionData) : base(potionData) 
    {
        ItemData = potionData;
    }

    protected PotionState() { }

    public override object Clone() => new PotionState()
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
        return new Dictionary<string, string>()
        {
            { "Сила действия", EffectPower.ToString() },
            { "Продолжительность", EffectDuration + " сек." }
        };
    }
}
