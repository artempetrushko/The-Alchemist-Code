using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlchemistryIngredientCell : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField]
    private Image puzzleArrowPrefab;

    private Image currentPuzzleArrow;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<LineRenderer>().SetPosition(0, gameObject.transform.position);
        //currentPuzzleArrow = Instantiate(puzzleArrowPrefab, gameObject.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<LineRenderer>().SetPosition(1, Input.mousePosition);
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
