using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    [SerializeField]
    protected string title;
    protected InteractiveObjectPanel interactiveObjectPanel;

    public string Title => title;

    private void OnEnable()
    {
        interactiveObjectPanel = FindObjectOfType<InteractiveObjectPanel>();
    }

    private void Start()
    {
        interactiveObjectPanel = FindObjectOfType<InteractiveObjectPanel>();
    }
}
