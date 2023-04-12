using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class WeaponState : EquipmentState
{
    public int Damage { get; set; }
    public float Range { get; set; }
    public float Accuracy { get; set; }
    public float AttackSpeed { get; set; }
    public float CooldownTime { get; set; }
    public int PenetratingPower { get; set; }
    public bool IsCombable { get; set; }

    public AnimatorOverrideController Animations { get; set; }

    public WeaponState(WeaponData weapon) : base(weapon)
    {
        Damage = weapon.BaseDamage;
        Range = weapon.BaseRange;
        Accuracy = weapon.BaseAccuracy;
        AttackSpeed = weapon.BaseAttackSpeed;
        CooldownTime = weapon.BaseCooldownTime;
        PenetratingPower = weapon.BasePenetratingPower;
        Animations = weapon.Animations;
        IsCombable = weapon.IsCombable;
    }

    protected WeaponState() { }

    public override Dictionary<string, string> GetItemParams()
    {
        return new Dictionary<string, string>()
        {
            { "Урон", Damage.ToString() },
            { "Прочность", string.Format("{0}/{1}", Endurance, MaxEndurance) },
            { "Составных частей", string.Format("{0}/{1}", ComponentParts.Where(part => part.ComponentPartState != null).Count().ToString(), ComponentParts.Count.ToString()) },
        };
    }
}
