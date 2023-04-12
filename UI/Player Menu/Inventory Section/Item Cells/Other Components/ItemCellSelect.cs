using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCellSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ItemCellActionMenu itemCellActionMenuPrefab;

    private PlayerMenuManager playerMenuManager;   
    private ItemCellActionMenu currentActionMenu;
    private ItemCellContainer itemCellContainer;
    private bool isPressed;

    public bool IsPressed
    {
        get => isPressed;
        set
        {
            isPressed = value;
            if (value)
            {
                GenerateActionMenu();
            }
            else
            {
                Destroy(currentActionMenu.gameObject);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && EventSystem.current.IsPointerOverGameObject()) 
        {
            if (!itemCellContainer.IsItemPlaceEmpty)
            {
                playerMenuManager.PressItemCell();
            }           
        }
    }

    private void GenerateActionMenu()
    {
        var actionMenu = Instantiate(itemCellActionMenuPrefab, playerMenuManager.transform);  
        var containedInventoryItem = itemCellContainer.ContainedItem;
        actionMenu.GenerateActions(containedInventoryItem.ItemState, gameObject);
        currentActionMenu = actionMenu;
    }

    private void OnEnable()
    {
        playerMenuManager = FindObjectOfType<PlayerMenuManager>();
        itemCellContainer = GetComponent<ItemCellContainer>();
    }
}
