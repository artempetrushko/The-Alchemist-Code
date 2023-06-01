using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class RunEndingScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject hud;

    private PlayerInputManager inputManager;
    private PlayableDirector playableDirector;
    private bool isPlayerDied = false;

    public IEnumerator ShowGameOverMessage()
    {
        if (!isPlayerDied)
        {
            isPlayerDied = true;
            inputManager.SwitchActionMap_UI_RunEndingScreen();
            yield return new WaitForSeconds(1f);
            hud.SetActive(false);
            transform.localScale = Vector3.one;
            StartCoroutine(PlayTimeline_COR("ShowGameOverScreen"));
        }        
    }

    public void ShowRunCompleteMessage()
    {
        hud.SetActive(false);
        transform.localScale = Vector3.one;
        inputManager.SwitchActionMap_UI_RunEndingScreen();
        StartCoroutine(PlayTimeline_COR("ShowRunCompleteScreen"));
    }

    public void ReturnToHub(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReturnToHub();
        }
    }

    public void ReturnToMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToHub() => StartCoroutine(LeaveCurrentLevel_COR(1));

    public void ReturnToMainMenu() => StartCoroutine(LeaveCurrentLevel_COR(0));

    private IEnumerator LeaveCurrentLevel_COR(int loadingSceneIndex)
    {
        yield return StartCoroutine(PlayTimeline_COR("ExitGame"));
        SceneManager.LoadScene(loadingSceneIndex);
    }

    private IEnumerator PlayTimeline_COR(string playableAssetName)
    {
        playableDirector.playableAsset = Resources.Load<PlayableAsset>("Timelines/UI/Run Ending Screen/" + playableAssetName);
        playableDirector.Play();
        yield return new WaitForSeconds((float)playableDirector.playableAsset.duration);
    }

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        inputManager = FindObjectOfType<PlayerInputManager>();
    }
}
