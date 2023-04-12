using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceState : StackableItemState
{
    public ResourceState(ResourceData resourceData) : base(resourceData) { }

    protected ResourceState() { }
}
