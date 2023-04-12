using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] 
    private GameObject buttons;
    [SerializeField] 
    private Button returnToMenuButton;

    private GameObject currentOpenedMenuSection = null;

    public void ShowMenuSection(GameObject menuSection)
    {
        buttons.SetActive(false);
        menuSection.SetActive(true);
        returnToMenuButton.gameObject.SetActive(true);
        currentOpenedMenuSection = menuSection;
    }

    public void ReturnToMainMenu()
    {
        currentOpenedMenuSection.SetActive(false);
        returnToMenuButton.gameObject.SetActive(false);
        buttons.SetActive(true);
    }

    public void StartGame() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}
