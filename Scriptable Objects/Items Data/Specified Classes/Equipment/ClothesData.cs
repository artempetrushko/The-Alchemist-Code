using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClothesType
{
    Hat,
    Raincoat,
    Boots,
    Gloves,
    Medallion
}

[CreateAssetMenu(fileName = "New Clothes", menuName = "Game Entities/Items/Equipment/Clothes", order = 51)]
public class ClothesData : EquipmentData
{
    [Header("Параметры одежды")]
    [SerializeField]
    protected ClothesType clothesType;
    [SerializeField]
    protected Sprite bigIcon;
    [SerializeField]
    protected int defence;

    public ClothesType ClothesType => clothesType;
    public Sprite BigIcon => bigIcon;
    public int BaseDefence => defence;

    public override ItemState GetItemState()
    {
        return new ClothesState(this);
    }
}
