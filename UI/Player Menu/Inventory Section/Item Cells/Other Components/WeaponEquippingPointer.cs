using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponEquippingPointer : MonoBehaviour, IPointerDownHandler
{
    private WeaponEquipping weaponEquippingModule;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (weaponEquippingModule.IsInteractionStarted)
        {
            weaponEquippingModule.Execute();
        }      
    }

    private void Start()
    {
        weaponEquippingModule = GetComponentInParent<WeaponEquipping>();
    }
}
