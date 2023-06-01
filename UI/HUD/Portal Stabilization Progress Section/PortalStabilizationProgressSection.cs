using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PortalStabilizationProgressSection : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;

    private Animator animator;
    private Image progressBarFillingArea;
    private Rect progressBarRect;

    public IEnumerator FillProgressBar_COR(int fillingTimeInSeconds)
    {
        yield return StartCoroutine(PlayAnimation_COR("Show Section"));
        progressBarFillingArea.gameObject.SetActive(true);
        for (var i = 1; i <= fillingTimeInSeconds; i++)
        {
            progressBarFillingArea.rectTransform.sizeDelta = i <= fillingTimeInSeconds 
                ? new Vector2(progressBarRect.width * (0.9f * i / fillingTimeInSeconds + 0.1f), progressBarFillingArea.rectTransform.rect.height)
                : new Vector2(progressBarRect.width * 1.1f, progressBarFillingArea.rectTransform.rect.height);
            yield return new WaitForSecondsRealtime(1f);
        }
        yield return StartCoroutine(PlayAnimation_COR("Hide Section"));
        progressBarFillingArea.gameObject.SetActive(false);
    }

    private IEnumerator PlayAnimation_COR(string animationName)
    {
        var clip = animator.runtimeAnimatorController.animationClips.Where(x => x.name == animationName).First();
        animator.Play(clip.name);
        yield return new WaitForSeconds(clip.length);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        progressBarFillingArea = progressBar.transform.GetChild(0).GetComponent<Image>();
        progressBarFillingArea.gameObject.SetActive(false);
        progressBarRect = progressBar.rectTransform.rect;
    }
}
