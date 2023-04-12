using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialType
{
    Metal,
    Crystal,
    Other
}

[CreateAssetMenu(fileName = "New Material", menuName = "Game Entities/Items/Resources/Material", order = 51)]
public class MaterialData : ResourceData
{
    [Header("��������� ���������")]
    [SerializeField]
    protected MaterialType materialType;
    [SerializeField]
    protected CharacteristicCoeffsSet coefficientsSet;
    [SerializeField]
    protected List<ItemEffect> features = new List<ItemEffect>();   

    public MaterialType MaterialType => materialType;
    public List<ItemEffect> Features => features;
    public CharacteristicCoeffsSet CoefficientsSet => coefficientsSet;

    public override ItemState GetItemState()
    {
        return new MaterialState(this);
    }
}
