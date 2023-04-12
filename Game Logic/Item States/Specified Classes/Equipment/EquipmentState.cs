using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentState : SingleItemState
{
    public Sprite BigIcon => (ItemData as EquipmentData).BigIcon;
    public RuneState ImposedRune { get; set; }
    public List<RequiredComponentPartState> ComponentParts { get; set; } = new List<RequiredComponentPartState>();
    public int Endurance { get; set; }
    public int MaxEndurance { get; }
    public int MaxRuneSize { get; set; }
    public int PoweredEnergyCount { get; set; }
    public int EnergyCapacity { get; set; }

    public EquipmentState(EquipmentData equipment) : base(equipment)
    {
        Endurance = equipment.BaseEndurance;
        MaxEndurance = equipment.BaseEndurance;
        MaxRuneSize = equipment.BaseMaxRuneSize;
        PoweredEnergyCount = equipment.BasePoweredEnergyCount;
        EnergyCapacity = equipment.BaseEnergyCapacity;

        foreach (var component in equipment.BaseComponentParts)
        {
            ComponentParts.Add(new RequiredComponentPartState(component));
        }
    }

    protected EquipmentState() { }
}
