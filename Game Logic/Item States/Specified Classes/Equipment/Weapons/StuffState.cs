using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffState : RangedWeaponState
{
    public StickAttackType AttackType { get; set; }

    public StuffState(StuffData stick) : base(stick)
    {
        ItemData = stick;
        AttackType = stick.BaseAttackType;
    }

    protected StuffState() { }

    public override object Clone() => new StuffState()
    {
        ItemData = ItemData,
        Description = Description,
        Aspects = Aspects,
        CastingDamage = CastingDamage,
        Effects = Effects,

        ComponentParts = ComponentParts,
        Endurance = Endurance,
        EnergyCapacity = EnergyCapacity,
        ImposedRune = ImposedRune,
        PoweredEnergyCount = PoweredEnergyCount,
        MaxRuneSize = MaxRuneSize,

        Damage = Damage,
        Range = Range,
        Accuracy = Accuracy,
        AttackSpeed = AttackSpeed,
        CooldownTime = CooldownTime,
        PenetratingPower = PenetratingPower,

        MissileFlightSpeed = MissileFlightSpeed,

        AttackType = AttackType,
    };
}
