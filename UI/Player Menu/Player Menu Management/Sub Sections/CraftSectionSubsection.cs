using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class CraftSectionSubsection : PlayerMenuSectionSubsection
{
    protected CraftSectionNavigation sectionNavigation;

    public CraftSectionNavigation SectionNavigation => sectionNavigation;

    protected virtual void Start()
    {
        sectionNavigation = GetComponentInParent<CraftSectionNavigation>();
    }
}
