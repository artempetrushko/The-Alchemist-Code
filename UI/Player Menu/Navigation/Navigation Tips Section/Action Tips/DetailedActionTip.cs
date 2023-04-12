using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedActionTip : ActionTip
{
    public string ActionTitle { get; private set; }

    public DetailedActionTip(string actionTitle, string keyName) : base(keyName)
    {
        ActionTitle = actionTitle;
    }

    public DetailedActionTip(string actionTitle, Sprite keyIcon) : base(keyIcon)
    {
        ActionTitle = actionTitle;
    }
}
