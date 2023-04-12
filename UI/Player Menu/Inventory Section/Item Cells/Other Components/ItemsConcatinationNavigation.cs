using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsConcatinationNavigation : MonoBehaviour
{
    [SerializeField]
    private InventorySectionSubsection leftSubsection;
    [SerializeField]
    private InventorySectionSubsection rightSubsection;

    public InventorySectionSubsection LeftSubsection => leftSubsection;
    public InventorySectionSubsection RightSubsection => rightSubsection;
}
