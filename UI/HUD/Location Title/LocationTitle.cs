using LeTai.TrueShadow;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LocationTitle : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onLocationTitleShown;

    private TMP_Text locationTitleText;
    private TrueShadow titleShadow;
    private float titleAppearanceTime = 1f;

    public void ShowLocationTitle() => StartCoroutine(ShowLocationTitle_COR());

    private IEnumerator ShowLocationTitle_COR()
    {
        var locationTitle = SceneManager.GetActiveScene().buildIndex switch
        {
            1 => "Убежище",
            2 => "Подземелья",
            _ => "Test Scene"
        };
        titleShadow.enabled = true;
        for (var i = 0; i < locationTitle.Length; i++)
        {
            locationTitleText.text += locationTitle[i];
            yield return new WaitForSecondsRealtime(titleAppearanceTime / locationTitle.Length);
        }
        yield return new WaitForSecondsRealtime(5f);
        for (var i = 0; i < locationTitle.Length; i++)
        {
            locationTitleText.text = locationTitleText.text.Substring(0, locationTitleText.text.Length - 1);
            yield return new WaitForSecondsRealtime(titleAppearanceTime / locationTitle.Length / 2);
        }
        titleShadow.enabled = false;
        onLocationTitleShown.Invoke();
    }

    private void Start()
    {
        titleShadow = GetComponent<TrueShadow>();
        locationTitleText = GetComponent<TMP_Text>();
        locationTitleText.text = "";
    }
}
