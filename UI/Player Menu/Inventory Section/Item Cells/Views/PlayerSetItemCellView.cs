using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSetItemCellView : SimpleItemCellView
{
    [Space]
    [SerializeField]
    protected HUDItemCellView linkedItemCellView;

    public HUDItemCellView LinkedItemCellView => linkedItemCellView;
}
