using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelFinisher : MonoBehaviour
{
    [SerializeField]
    private int nextSceneIndex;
    [Tooltip("Отметить, если текущий уровень последний в забеге")]
    [SerializeField]
    private bool isLastLevel;

    private GameManager gameManager;
    private RunEndingScreen runEndingScreen;
    private LoadingScreen loadingScreen;

    public void Activate()
    {
        if (!gameManager.PlayerFinishedLevelEver)
        {
            gameManager.PlayerFinishedLevelEver = true;
        }
        gameManager.SavePlayerProgress();
        if (isLastLevel)
        {
            runEndingScreen.ShowRunCompleteMessage();
        }
        else
        {
            StartCoroutine(loadingScreen.LoadLevelAsync_COR(nextSceneIndex));
        }
        Destroy(GetComponent<Collider>());
    }

    public void FinishGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        runEndingScreen = FindObjectOfType<RunEndingScreen>();
        loadingScreen= FindObjectOfType<LoadingScreen>();
    }
}
