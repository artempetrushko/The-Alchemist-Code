using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseItemsCountActions
{
    public Action Confirm { get; private set; }
    public Action ConfirmAll { get; private set; }
    public Action Cancel { get; private set; }

    public ChooseItemsCountActions(Action confirmAction, Action cancelAction)
    {
        Confirm = confirmAction;
        Cancel = cancelAction;
    }

    public ChooseItemsCountActions(Action confirmAction, Action confirmAllAction, Action cancelAction) : this(confirmAction, cancelAction)
    {
        ConfirmAll = confirmAllAction;
    }

    public Action GetActionByOrderNumber(int orderNumber, bool isLastNumber)
    {
        return isLastNumber
            ? Cancel
            : orderNumber switch
            {
                0 => Confirm,
                1 => ConfirmAll,
            };
    }
}
