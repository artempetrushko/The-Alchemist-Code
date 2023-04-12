using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleItemState : ItemState
{
    public SingleItemState(SingleItemData item) : base(item) { }

    protected SingleItemState() { }
}
