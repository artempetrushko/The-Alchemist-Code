using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObjectPanel : MonoBehaviour
{
    [SerializeField]
    private ItemsContainerContentView itemsContainerContentView;
    [SerializeField]
    private InteractiveObjectInfoView interactiveObjectInfo;
    [SerializeField]
    private PlayerActionsViewContainer postInteractionPlayerActionsContainer;

    private string currentObjectName;
    private Transform currentObjectTransform;
    private float currentObjectHeight;
    private float panelOffsetMultiplier = 3f;

    public bool IsEnable => gameObject.transform.localScale == Vector3.one;

    public void SetInfoAndEnable(string objectName, Transform objectTransform, List<DetailedActionTip> possibleActionTip)
    {
        EnableUI(objectTransform);
        currentObjectName = objectName;
        interactiveObjectInfo.SetInfo(objectName, possibleActionTip);
    }

    public void EnableContainerContentView(List<ItemState> spawnedItems, Action<int> pickItemFunction)
    {
        interactiveObjectInfo.SetVisibility(false);
        itemsContainerContentView.SetVisibility(true);
        itemsContainerContentView.ContainerTitle = currentObjectName;
        itemsContainerContentView.GenerateContainerContent(spawnedItems, pickItemFunction);
    }

    public void UpdateContainerContentView(List<ItemState> spawnedItems, Action<int> pickItemFunction)
    {
        itemsContainerContentView.UpdateContainerContent(spawnedItems, pickItemFunction);
    }

    public void ChangeSelectedContainedItem(Vector2 inputValue) => itemsContainerContentView.ChangeSelectedContainedItem(inputValue);

    public void PickItemByPressKey() => itemsContainerContentView.PickCurrentItem();

    public void DisableContainerContentView(bool isContainerLooted = false)
    {
        itemsContainerContentView.SetVisibility(false);
        postInteractionPlayerActionsContainer.SetVisibility(false);
        if (!isContainerLooted)
        {
            interactiveObjectInfo.SetVisibility(true);
        }    
    }

    public void EnablePostInteractionPlayerActionsContainer(List<DetailedActionTip> actionTips)
    {
        postInteractionPlayerActionsContainer.ShowPlayerActionsInfo(actionTips);
    }

    public void EnableUI(Transform objectTransform)
    {
        currentObjectTransform = objectTransform;
        currentObjectHeight = currentObjectTransform.gameObject.GetComponent<MeshRenderer>() != null
                                ? currentObjectTransform.gameObject.GetComponent<MeshRenderer>().bounds.size.y / 2
                                : 0;
        gameObject.transform.localScale = Vector3.one;
        interactiveObjectInfo.SetVisibility(true);
    }

    public void DisableUI()
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (IsEnable)
        {
            Vector2 objectScreenPosition = Camera.main.WorldToScreenPoint(new Vector3(currentObjectTransform.position.x, 
                                                                                      currentObjectTransform.position.y + currentObjectHeight * panelOffsetMultiplier,
                                                                                      currentObjectTransform.position.z));
            gameObject.transform.position = objectScreenPosition;
        }
    }

    private void Start()
    {
        DisableUI();
    }
}
