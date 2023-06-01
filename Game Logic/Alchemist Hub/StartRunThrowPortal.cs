using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartRunThrowPortal : MonoBehaviour
{
    [SerializeField]
    private LoadingScreen loadingScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>() != null)
        {
            loadingScreen.gameObject.SetActive(true);
            StartCoroutine(loadingScreen.LoadLevelAsync_COR(2));
        }
    }
}
