using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject headsUpDisplay;
    [SerializeField]
    private InventoryToolbar inventoryToolbar;

    private PlayerInputManager player;

    private void Start()
    {
        player = FindObjectOfType<PlayerInputManager>();
    }
}
