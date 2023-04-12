using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSetItems : ILeftHandWeapon
{
    public event Action<WeaponState> LeftHandWeaponChanged;
    public event Action<WeaponState> RightHandWeaponChanged;
    public event Action<ClothesState> HatChanged;
    public event Action<ClothesState> RaincoatChanged;
    public event Action<ClothesState> BootsChanged;
    public event Action<ClothesState> GlovesChanged;
    public event Action<ClothesState> MedallionChanged;
    public event Action<ItemState> SelectedQuickAccessItemChanged;

    private WeaponState leftHandWeapon;
    private WeaponState rightHandWeapon;
    private ClothesState hat;
    private ClothesState raincoat;
    private ClothesState boots;
    private ClothesState gloves;
    private ClothesState medallion;
    private ItemState selectedQuickAccessItem;

    public WeaponState LeftHandWeapon 
    { 
        get => leftHandWeapon;
        set
        {
            leftHandWeapon = value;
            LeftHandWeaponChanged?.Invoke(value);
        }
    }
    public WeaponState RightHandWeapon 
    { 
        get => rightHandWeapon; 
        set
        {
            rightHandWeapon = value;
            RightHandWeaponChanged?.Invoke(value);
        }
    }
    public ClothesState Hat 
    { 
        get => hat; 
        set
        {
            hat = value;
            HatChanged?.Invoke(value);
        }
    }
    public ClothesState Raincoat 
    { 
        get => raincoat; 
        set
        {
            raincoat = value;
            RaincoatChanged?.Invoke(value);
        }
    }
    public ClothesState Boots 
    { 
        get => boots; 
        set
        {
            boots = value;
            BootsChanged?.Invoke(value);
        }
    }
    public ClothesState Gloves 
    { 
        get => gloves; 
        set
        {
            gloves = value;
            GlovesChanged?.Invoke(value);
        }
    }
    public ClothesState Medallion 
    { 
        get => medallion; 
        set
        {
            medallion = value;
            MedallionChanged?.Invoke(value);
        }
    }
    public ItemState SelectedQuickAccessItem 
    { 
        get => selectedQuickAccessItem; 
        set
        {
            selectedQuickAccessItem = value;
            SelectedQuickAccessItemChanged?.Invoke(value);
        }
    }
}
