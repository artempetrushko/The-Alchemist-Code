using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickingMessagesSection : MonoBehaviour
{
    [SerializeField]
    private int maxMessagesCount;
    [SerializeField]
    private int maxShowcaseTimeInSeconds;
    [Space]
    [SerializeField]
    private ItemPickingMessageView itemPickingMessagePrefab;

    private bool isShowcaseStarted = false;
    private int currentShowcaseTime = 0;

    private bool IsShowcaseStarted
    {
        set
        {
            if (value != isShowcaseStarted)
            {
                isShowcaseStarted = value;
                if (isShowcaseStarted)
                {
                    StartCoroutine(CountMessagesShowcaseTime_COR());
                }
                else
                {
                    for (var i = transform.childCount - 1; i >= 0; i--)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                    transform.DetachChildren();
                    currentShowcaseTime = 0;
                }
            }
        }
    }

    public void UpdateContent(ItemState item)
    {
        var newMessage = Instantiate(itemPickingMessagePrefab, transform);
        newMessage.SetInfo(item);
        if (transform.childCount > maxMessagesCount)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        IsShowcaseStarted = true;
        currentShowcaseTime = 0;
    }

    private IEnumerator CountMessagesShowcaseTime_COR()
    {
        while (currentShowcaseTime < maxShowcaseTimeInSeconds)
        {
            yield return new WaitForSeconds(1f);
            currentShowcaseTime++;
        }
        IsShowcaseStarted = false;
    }
}
