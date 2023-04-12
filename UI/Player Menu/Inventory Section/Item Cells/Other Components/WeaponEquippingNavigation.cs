using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquippingNavigation : MonoBehaviour
{
    [SerializeField]
    private WeaponEquippingNavigation leftWeaponCell;
    [SerializeField]
    private WeaponEquippingNavigation rightWeaponCell;

    public WeaponEquippingNavigation LeftWeaponCell => leftWeaponCell;
    public WeaponEquippingNavigation RightWeaponCell => rightWeaponCell;
}
