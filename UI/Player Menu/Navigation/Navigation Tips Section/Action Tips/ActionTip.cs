using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionTip
{
    public string KeyName { get; private set; }
    public Sprite KeyIcon { get; private set; }

    public ActionTip(string keyName) 
    {
        KeyName = keyName;
    }

    public ActionTip(Sprite keyIcon)
    {
        KeyIcon = keyIcon;
    }
}
