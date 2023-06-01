using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingInfoSection;
    [SerializeField]
    private Image progressBarFillAreaContainer;
    [SerializeField]
    private GameObject continueButtonSection;

    private InputManager inputManager;
    private Image progressBarFillArea;
    private Rect progressBarFillAreaContainerRect;
    private float loadingProgress;
    private bool isContinueButtonPressed = false;

    private float LoadingProgress
    {
        set
        {
            loadingProgress = value;
            progressBarFillArea.rectTransform.sizeDelta = new Vector2(progressBarFillAreaContainerRect.width * (loadingProgress + 0.1f), progressBarFillArea.rectTransform.rect.height);
        }
    }

    public IEnumerator LoadLevelAsync_COR(int loadingSceneIndex)
    {
        inputManager.SwitchActionMap_UI_LoadingScreen();
        var operation = SceneManager.LoadSceneAsync(loadingSceneIndex);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                LoadingProgress = 1f;
                loadingInfoSection.GetComponent<CanvasGroup>().alpha = 0f;
                continueButtonSection.SetActive(true);
                if (isContinueButtonPressed)
                {
                    operation.allowSceneActivation = true;
                }
            }
            else
            {
                LoadingProgress = operation.progress;
            }
            yield return null;
        }
    }

    public void ContinueSceneLoadingByInput(InputAction.CallbackContext context)
    {
        if (context.performed && continueButtonSection.activeInHierarchy)
        {
            ContinueSceneLoading();
        }
    }

    public void ContinueSceneLoading() => isContinueButtonPressed = true;

    private void OnEnable()
    {
        inputManager = FindObjectOfType<InputManager>();
        progressBarFillArea = progressBarFillAreaContainer.transform.GetChild(0).GetComponent<Image>();
        progressBarFillAreaContainerRect = progressBarFillAreaContainer.rectTransform.rect;
    }
}
