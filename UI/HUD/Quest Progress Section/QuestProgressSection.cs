using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestProgressSection : MonoBehaviour
{
    [SerializeField]
    private TMP_Text questRequirementText;
    
    private Animator animator;
    private bool isVisible;

    public void ShowQuestProgress(InputAction.CallbackContext context)
    {
        if (context.performed && !isVisible)
        {
            StartCoroutine(ShowQuestProgress_COR());
        }
    }

    public void ShowQuestProgress(string currentQuestState)
    {
        questRequirementText.text = currentQuestState;
        StartCoroutine(ShowQuestProgress_COR());
    }

    private IEnumerator ShowQuestProgress_COR()
    {
        isVisible = true;
        var clip = animator.runtimeAnimatorController.animationClips.Where(x => x.name == "Show Quest Progress").First();
        animator.Play(clip.name);
        yield return new WaitForSeconds(clip.length);
        isVisible = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
