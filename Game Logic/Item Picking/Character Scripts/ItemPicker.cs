using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPicker : MonoBehaviour
{
    [SerializeField]
    protected List<PickableItem> pickableItems = new List<PickableItem>();

    public abstract void PickItems();

    private void OnTriggerEnter(Collider other)
    {
        var pickableItem = other.gameObject.GetComponent<PickableItem>();
        if (pickableItem != null && !pickableItems.Contains(pickableItem))
        {
            pickableItems.Add(pickableItem);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickableItems.Remove(other.gameObject.GetComponent<PickableItem>());
    }  
}
