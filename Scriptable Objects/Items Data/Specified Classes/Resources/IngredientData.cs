using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Game Entities/Items/Resources/Ingredient", order = 51)]
public class IngredientData : ResourceData
{
    [Header("Параметры ингредиента")]
    [SerializeField]
    protected List<ItemEffect> potionContainedEffects = new List<ItemEffect>();
    [SerializeField]
    protected List<ItemEffect> bombContainedEffects = new List<ItemEffect>();

    public List<ItemEffect> PotionContainedEffects => potionContainedEffects;
    public List<ItemEffect> BombContainedEffects => bombContainedEffects;

    public override ItemState GetItemState()
    {
        throw new System.NotImplementedException();
    }
}
