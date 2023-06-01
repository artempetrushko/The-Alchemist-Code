using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemistrySection : PlayerMenuSection
{
    public override void SetVisibility(bool isVisible)
    {
        gameObject.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
    }

    private void OnEnable()
    {
        SetVisibility(false);
    }
}
