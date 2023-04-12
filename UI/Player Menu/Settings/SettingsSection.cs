using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsSection : PlayerMenuSection
{
    public override void SetVisibility(bool isVisible)
    {
        gameObject.transform.localScale = isVisible ? Vector3.one : Vector3.zero;
        if (isVisible)
        {
            GetComponentInChildren<Button>().Select();
        }
    }

    public void ReturnToAlchemistHub() => SceneManager.LoadScene(1);

    public void ReturnToMainMenu() => SceneManager.LoadScene(0);

    private void OnEnable()
    {
        SetVisibility(false);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            transform.GetComponentsInChildren<Button>()[1].gameObject.SetActive(false);
        }
    }
}
