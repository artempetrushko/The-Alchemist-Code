using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemsCategoryButton : MonoBehaviour
{
    [SerializeField]
    private Button defaultButtonState;
    [SerializeField]
    private Image pressedButtonState;

    public void ToggleButtonState(bool isPressed)
    {
        defaultButtonState.gameObject.SetActive(!isPressed);
        pressedButtonState.gameObject.SetActive(isPressed);
    }
}
