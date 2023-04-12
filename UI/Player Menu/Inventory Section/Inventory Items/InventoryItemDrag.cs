using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject DraggingItem;

    [SerializeField]
    private Vector2 draggingItemViewSize = new Vector2(83, 83);

    private CanvasGroup canvasGroup;
    private UIManager uiManager;
    private ItemInfo inventoryItem;

    public Transform StartParent { get; private set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartParent = transform.parent;
        DraggingItem = gameObject;
        DraggingItem.GetComponent<Image>().rectTransform.sizeDelta = draggingItemViewSize;
        inventoryItem.AttachedItemCellView = null;

        transform.SetParent(uiManager.transform);
        transform.SetAsLastSibling();
        ToggleCanvasGroupState(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ToggleCanvasGroupState(false);
        if (transform.parent == uiManager.transform)
        {
            transform.SetParent(StartParent);
            inventoryItem.AttachedItemCellView = GetComponentInParent<ItemCellView>();
        }
        transform.localPosition = Vector3.zero;
        DraggingItem = null;
    }

    private void ToggleCanvasGroupState(bool isDraggingStarted)
    {
        canvasGroup.alpha = isDraggingStarted ? 0.6f : 1f;
        canvasGroup.blocksRaycasts = !isDraggingStarted;
    }

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        uiManager = FindObjectOfType<UIManager>();
        inventoryItem = GetComponent<ItemInfo>();
    } 
}
