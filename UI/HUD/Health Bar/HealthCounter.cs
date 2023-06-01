using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthCounter;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image healthBarFillingArea;
    [SerializeField]
    private List<HealthBarState> healthBarStates;
    [Space]
    [SerializeField]
    private RunEndingScreen runEndingScreen;

    private ABC_StateManager playerStateManager;
    private int displayedHealthCount;
    private int displayedMaxHealthCount;

    private void Update()
    {
        if (playerStateManager.currentHealth != displayedHealthCount || playerStateManager.currentMaxHealth != displayedMaxHealthCount)
        {
            displayedHealthCount = (int)Mathf.Clamp(playerStateManager.currentHealth, 0, playerStateManager.currentMaxHealth);
            displayedMaxHealthCount = (int)playerStateManager.currentMaxHealth;
            healthCounter.text = string.Format("{0}/{1}", displayedHealthCount.ToString(), displayedMaxHealthCount.ToString());
            foreach (var state in healthBarStates)
            {
                if (state.TrySetHealthBarState((float)displayedHealthCount / displayedMaxHealthCount * 100, healthBar, healthBarFillingArea))
                {
                    break;
                }
            }
            healthBarFillingArea.fillAmount = (float)displayedHealthCount / displayedMaxHealthCount;
        }        
    }

    private void Start()
    {
        playerStateManager = FindObjectOfType<PlayerInput>().GetComponent<ABC_StateManager>();  
    }
}
