using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ItemState : ICloneable
{
    protected SimpleItemCellView currentInventoryItemCellView;
    protected MechanicsItemCellView currentMechanicsItemCellView;
    
    public int ID => ItemData.ID;
    public PickableItem PhysicalRepresentaionPrefab => ItemData.PhysicalRepresentation;
    public Sprite Icon => ItemData.Icon;
    public string Title => ItemData.Title;
    public string Description { get; set; }
    public int CastingDamage { get; set; }
    public List<AspectData> Aspects { get; set; } = new List<AspectData>();
    public List<ItemEffect> Effects { get; set; } = new List<ItemEffect>();
    public int ContainedEnergyCount => Aspects.Count > 0
        ? Aspects.Select(aspect => aspect.ContainedEnergyCount).Sum()
        : 0;
    public SimpleItemCellView CurrentInventoryItemCellView
    {
        get => currentInventoryItemCellView;
        set
        {
            if (currentInventoryItemCellView != null && currentInventoryItemCellView != value)
            {
                currentInventoryItemCellView.DisableInfoElements();
            }
            currentInventoryItemCellView = value;
            if (currentInventoryItemCellView != null)
            {
                currentInventoryItemCellView.UpdateInfoElementsState(this);
            }
        }
    }
    public MechanicsItemCellView CurrentMechanicsItemCellView
    {
        get => currentMechanicsItemCellView;
        set
        {
            if (currentMechanicsItemCellView != null && currentMechanicsItemCellView != value)
            {
                currentMechanicsItemCellView.DisableInfoElements();
            }
            currentMechanicsItemCellView = value;
            if (currentMechanicsItemCellView != null)
            {
                currentMechanicsItemCellView.UpdateInfoElementsState(this);
                if (currentMechanicsItemCellView is CraftInventorySectionCellView && (currentMechanicsItemCellView as CraftInventorySectionCellView).LinkedMainInventoryItemCellView != CurrentInventoryItemCellView)
                {
                    (currentMechanicsItemCellView as CraftInventorySectionCellView).LinkedMainInventoryItemCellView.GetComponent<ItemCellContainer>()
                        .PlaceItem(CurrentInventoryItemCellView.GetComponent<ItemCellContainer>().ContainedItem);
                }
            }
        }
    }
    protected ItemData ItemData { get; set; }

    public ItemState(ItemData item)
    {
        Description = item.BaseDescription;
        CastingDamage = item.BaseCastingDamage;
        Aspects = item.BaseContainedAspects;
        Effects = item.BaseEffects;
    }

    protected ItemState() { }

    public abstract object Clone();

    public abstract Dictionary<string, string> GetItemParams();

    public static bool Compare<T, P>(T firstItem, P secondItem) where T : ItemState 
                                                                where P : ItemState
    {
        return typeof(T) == typeof(P) 
            && firstItem.ID == secondItem.ID;
    }

    public bool CompareItemData(ItemData comparingItemData) => ItemData.Equals(comparingItemData);

    protected void UpdateItemCellViewContent<T>(ref T currentItemCellView, T newItemCellView) where T : ItemCellView
    {
        if (currentItemCellView != null && currentItemCellView != newItemCellView)
        {
            currentItemCellView.DisableInfoElements();
        }
        currentItemCellView = newItemCellView;
        if (currentItemCellView != null)
        {
            currentItemCellView.UpdateInfoElementsState(this);
        }
    }
}
