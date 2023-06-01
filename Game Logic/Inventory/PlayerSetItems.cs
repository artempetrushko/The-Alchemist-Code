using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum WeaponSelectionState
{
    LeftHandWeaponSelected,
    RightHandWeaponSelected
}

public class PlayerSetItems : MonoBehaviour
{
    private WeaponState leftHandWeapon;
    private WeaponState rightHandWeapon;
    private ABC_Controller.Weapon leftWeaponEntity;
    private ABC_Controller.Weapon rightWeaponEntity;

    private ClothesState hat;
    private ClothesState raincoat;
    private ClothesState boots;
    private ClothesState gloves;
    private ClothesState medallion;
    private ItemState selectedQuickAccessItem;

    private ABC_Controller playerController;
    private WeaponSelectionState weaponSelectionState;

    public WeaponState LeftHandWeapon
    {
        get => leftHandWeapon;
        set
        {
            if (leftHandWeapon != value)
            {
                leftHandWeapon = value;
                if (leftHandWeapon != null)
                {
                    leftWeaponEntity = FindWeapon(leftHandWeapon);
                    if (WeaponSelectionState == WeaponSelectionState.LeftHandWeaponSelected)
                    {
                        playerController.EnableWeapon(leftWeaponEntity.weaponID);
                    }  
                }
                else
                {
                    playerController.DisableWeapon(leftWeaponEntity.weaponID);
                    leftWeaponEntity = null;
                }                
            }
        }
    }
    public WeaponState RightHandWeapon
    {
        get => rightHandWeapon;
        set
        {
            if (rightHandWeapon != value)
            {
                rightHandWeapon = value;
                if (rightHandWeapon != null)
                {
                    rightWeaponEntity = FindWeapon(rightHandWeapon);
                    if (WeaponSelectionState == WeaponSelectionState.RightHandWeaponSelected)
                    {
                        playerController.EnableWeapon(rightWeaponEntity.weaponID);
                    }
                }
                else
                {
                    playerController.DisableWeapon(rightWeaponEntity.weaponID);
                    rightWeaponEntity = null;
                }
            }
        }
    }
    public ClothesState Hat
    {
        get => hat;
        set
        {
            hat = value;
        }
    }
    public ClothesState Raincoat
    {
        get => raincoat;
        set
        {
            raincoat = value;
        }
    }
    public ClothesState Boots
    {
        get => boots;
        set
        {
            boots = value;
        }
    }
    public ClothesState Gloves
    {
        get => gloves;
        set
        {
            gloves = value;
        }
    }
    public ClothesState Medallion
    {
        get => medallion;
        set
        {
            medallion = value;
        }
    }
    public ItemState SelectedQuickAccessItem
    {
        get => selectedQuickAccessItem;
        set
        {
            selectedQuickAccessItem = value;
        }
    }
    private WeaponSelectionState WeaponSelectionState
    {
        get => weaponSelectionState;
        set
        {
            if (weaponSelectionState != value)
            {
                weaponSelectionState = value;
                switch (weaponSelectionState)
                {
                    case WeaponSelectionState.LeftHandWeaponSelected:
                        if (rightWeaponEntity != null)
                        {
                            playerController.DisableWeapon(rightWeaponEntity.weaponID);
                        }                    
                        if (leftWeaponEntity != null)
                        {
                            playerController.EnableWeapon(leftWeaponEntity.weaponID);
                            UpdateWeaponGraphics(LeftHandWeapon, leftWeaponEntity);
                            
                        }
                        break;

                    case WeaponSelectionState.RightHandWeaponSelected:
                        if (leftWeaponEntity != null)
                        {
                            playerController.DisableWeapon(leftWeaponEntity.weaponID);
                        }
                        if (rightWeaponEntity != null)
                        {
                            playerController.EnableWeapon(rightWeaponEntity.weaponID);
                            UpdateWeaponGraphics(RightHandWeapon, rightWeaponEntity);
                                  
                        }
                        break;
                }
            }
        }
    }

    public void Attack(InputAction.CallbackContext context) 
    {
        /*if (context.performed)
        {
            playerController.TriggerAbility("Sword Attack");
        }*/      
    }

    public void SelectLeftHandWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            WeaponSelectionState = WeaponSelectionState.LeftHandWeaponSelected;
        }
    }

    public void SelectRightHandWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            WeaponSelectionState = WeaponSelectionState.RightHandWeaponSelected;
        }       
    }

    private ABC_Controller.Weapon FindWeapon(WeaponState weaponState)
    {
        string weaponTypeName = weaponState.Title switch
        {
            var a when a.Contains("Меч") => "Sword",
            var b when b.Contains("Кинжал") => "Knife",
            var c when c.Contains("Посох") => "Stave",
        };
        var accordingWeapon = playerController.CurrentWeapons
            .Where(weapon => weapon.weaponName.Contains(weaponTypeName))
            .First();
        UpdateWeaponGraphics(weaponState, accordingWeapon);
        return accordingWeapon;
    }

    private void UpdateWeaponGraphics(WeaponState weaponState, ABC_Controller.Weapon weaponEntity)
    {
        /*weaponEntity.weaponGraphics[0].weaponObjMainGraphic.GameObject = weaponState.PhysicalRepresentaionPrefab.gameObject;
        weaponEntity.weaponGraphics[0].CreateGraphicObject();*/
        //weaponEntity.CreateObjectPools();

    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerInput>().GetComponent<ABC_Controller>();
        WeaponSelectionState = WeaponSelectionState.LeftHandWeaponSelected;
    }
}
