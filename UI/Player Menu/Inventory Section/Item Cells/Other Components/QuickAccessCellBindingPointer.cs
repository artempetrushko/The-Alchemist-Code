using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickAccessCellBindingPointer : MonoBehaviour, IPointerDownHandler
{
    private QuickAccessCellBinding cellBindingModule;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (cellBindingModule.IsInteractionStarted)
        {
            cellBindingModule.Execute();
        }      
    }

    private void OnEnable()
    {
        cellBindingModule = GetComponentInParent<QuickAccessCellBinding>();
    }
}
