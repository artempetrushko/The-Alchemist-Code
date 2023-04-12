using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetCellNavigation : MonoBehaviour
{
    [SerializeField]
    private PlayerSetCellNavigation leftNeighboringPlayerSetCell;
    [SerializeField]
    private PlayerSetCellNavigation rightNeighboringPlayerSetCell;
    [SerializeField]
    private PlayerSetCellNavigation topNeighboringPlayerSetCell;
    [SerializeField]
    private PlayerSetCellNavigation bottomNeighboringPlayerSetCell;

    public PlayerSetCellNavigation LeftNeighboringPlayerSetCell => leftNeighboringPlayerSetCell;
    public PlayerSetCellNavigation RightNeighboringPlayerSetCell => rightNeighboringPlayerSetCell;
    public PlayerSetCellNavigation TopNeighboringPlayerSetCell => topNeighboringPlayerSetCell;
    public PlayerSetCellNavigation BottomNeighboringPlayerSetCell => bottomNeighboringPlayerSetCell;
}
