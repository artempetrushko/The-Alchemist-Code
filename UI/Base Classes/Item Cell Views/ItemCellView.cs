using LeTai.TrueShadow;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CellViewState
{
    Normal,
    Selected
}

public abstract class ItemCellView : MonoBehaviour
{
    [SerializeField]
    protected Image background;
    [SerializeField]
    protected Color selectedStateColor;
    
    protected UIManager uiManager;

    public abstract void UpdateInfoElementsState(ItemState inventoryItem);

    public abstract void DisableInfoElements();

    public void ToggleBackgroundState(CellViewState state)
    {
        var backgroundShadow = background.GetComponent<TrueShadow>();
        backgroundShadow.enabled = state != CellViewState.Normal;
        if (backgroundShadow.enabled)
        {
            backgroundShadow.Color = state switch
            {
                CellViewState.Selected => selectedStateColor
            };
        }       
    } 

    protected abstract void OnEnable();

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
}
