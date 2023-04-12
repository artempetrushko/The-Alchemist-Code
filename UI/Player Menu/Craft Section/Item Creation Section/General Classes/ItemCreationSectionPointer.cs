using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ItemCreationSectionPointer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float progressBarFillingTimeInSecond;
    [SerializeField]
    private float stepTimeInSecond;

    private CraftManager craftManager;
    private ItemCreationSection itemCreationSection;
    private bool isCreationProceed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (craftManager.IsCreationAvailable)
        {
            isCreationProceed = true;
            StartCoroutine(FillCreationProgressBar_COR());
        }        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isCreationProceed = false;
        itemCreationSection.CraftProgressRound.fillAmount = 0;
        itemCreationSection.CraftProgressRound.gameObject.SetActive(false);
    }

    public void CreateItem(InputAction.CallbackContext context)
    {
        if (craftManager.IsCreationAvailable)
        {
            if (context.performed)
            {
                if (!isCreationProceed)
                {
                    isCreationProceed = true;
                    StartCoroutine(FillCreationProgressBar_COR());
                }
            }
            else if (context.canceled)
            {
                isCreationProceed = false;
                itemCreationSection.CraftProgressRound.fillAmount = 0;
                itemCreationSection.CraftProgressRound.gameObject.SetActive(false);
            }
        }      
    }

    private IEnumerator FillCreationProgressBar_COR()
    {
        itemCreationSection.CraftProgressRound.gameObject.SetActive(true);

        var remainingTimeToFillProgressBar = progressBarFillingTimeInSecond;
        while (isCreationProceed && remainingTimeToFillProgressBar > 0)
        {
            itemCreationSection.CraftProgressRound.fillAmount += 1f / progressBarFillingTimeInSecond * stepTimeInSecond;
            remainingTimeToFillProgressBar -= stepTimeInSecond;
            yield return new WaitForSeconds(stepTimeInSecond);
        }
        if (isCreationProceed)
        {
            craftManager.CreateNewItem();
        }
    }

    private void OnEnable()
    {
        craftManager = FindObjectOfType<CraftManager>();
        itemCreationSection = GetComponent<ItemCreationSection>();
    }

    private void OnDisable()
    {
        itemCreationSection.CraftProgressRound.fillAmount = 0;
        itemCreationSection.CraftProgressRound.gameObject.SetActive(false);
    }
}
