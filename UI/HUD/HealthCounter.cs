using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
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

    private HP playerHP;
    private int currentHealthCount;
    private int currentMaxHealthCount;

    private void Update()
    {
        var playerHealthCount = (int)typeof(HP).GetField("_hP", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(playerHP);
        var playerMaxHealthCount = (int)typeof(HP).GetField("_maxHP", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(playerHP);

        if (playerHealthCount != currentHealthCount || playerMaxHealthCount != currentMaxHealthCount)
        {
            currentHealthCount = playerHealthCount;
            currentMaxHealthCount = playerMaxHealthCount;
            healthCounter.text = string.Format("{0}/{1}", currentHealthCount.ToString(), currentMaxHealthCount.ToString());
            foreach (var state in healthBarStates)
            {
                if (state.TrySetHealthBarState((float)currentHealthCount / currentMaxHealthCount * 100, healthBar, healthBarFillingArea))
                {
                    break;
                }
            }
            healthBarFillingArea.fillAmount = (float)currentHealthCount / currentMaxHealthCount;
        }        
    }

    private void Start()
    {
        playerHP = FindObjectOfType<PlayerInputManager>().GetComponent<HP>();        
    }
}
