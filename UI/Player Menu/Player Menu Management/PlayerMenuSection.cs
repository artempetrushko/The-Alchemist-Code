using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMenuSection : MonoBehaviour
{
    public abstract void SetVisibility(bool isVisible);

    public void ToggleVisibility() => SetVisibility(gameObject.transform.localScale == Vector3.zero);
}
