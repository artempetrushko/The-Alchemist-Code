using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EffectApplyingActions
{
    public Action<PotionState> TimerStartAction;
    public Action<PotionState> TimerProceedAction;
    public Action<PotionState> TimerEndAction;
}

public class PotionApplyer : MonoBehaviour
{
    private ABC_StateManager playerStateManager;
    private InventoryManager inventoryManager;
    private PlayerSetItems playerSetItems;

    public void ApplyCurrentPotion(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ApplyCurrentPotion();
        }
    }

    public void ApplyCurrentPotion()
    {
        var currentPotion = playerSetItems.SelectedQuickAccessItem is PotionState 
            ? playerSetItems.SelectedQuickAccessItem as PotionState 
            : null;
        if (currentPotion != null)
        {
            currentPotion.ItemsCount--;
            if (currentPotion.ItemsCount == 0)
            {
                inventoryManager.RemoveItemState(currentPotion);
            }
            switch (currentPotion.Effect)
            {
                case PotionEffect.Heal:
                    if (currentPotion.EffectDuration > 0)
                    {
                        StartCoroutine(ApplyEffect_COR(currentPotion, new EffectApplyingActions()
                        {
                            TimerStartAction = null,
                            TimerProceedAction = (potion) => playerStateManager.AdjustHealth(potion.EffectPower),
                            TimerEndAction = null,
                        }));
                    }
                    else
                    {
                        playerStateManager.AdjustHealth(currentPotion.EffectPower);
                    }                    
                    break;

                case PotionEffect.Protection:
                    if (currentPotion.EffectDuration > 0)
                    {
                        StartCoroutine(ApplyEffect_COR(currentPotion, new EffectApplyingActions()
                        {
                            TimerStartAction = (potion) => playerStateManager.AdjustMeleeDamageMitigationPercentage(potion.EffectPower),
                            TimerProceedAction = null,
                            TimerEndAction = (potion) => playerStateManager.AdjustMeleeDamageMitigationPercentage(-potion.EffectPower),
                        }));
                    }
                    else
                    {
                        playerStateManager.AdjustMeleeDamageMitigationPercentage(currentPotion.EffectPower);
                    }                
                    break;

                case PotionEffect.Invul:
                    if (currentPotion.EffectDuration > 0)
                    {
                        StartCoroutine(ApplyEffect_COR(currentPotion, new EffectApplyingActions()
                        {
                            TimerStartAction = (potion) => playerStateManager.ToggleEffectProtection(true),
                            TimerProceedAction = null,
                            TimerEndAction = (potion) => playerStateManager.ToggleEffectProtection(false),
                        }));
                    }
                    else
                    {
                        playerStateManager.ToggleEffectProtection(true);
                    }
                    break;
            }
        }
    }

    private IEnumerator ApplyEffect_COR(PotionState currentPotion, EffectApplyingActions effectApplyingActions)
    {
        effectApplyingActions.TimerStartAction?.Invoke(currentPotion);
        var effectRemainingTime = currentPotion.EffectDuration;
        while (effectRemainingTime > 0)
        {
            effectApplyingActions.TimerProceedAction?.Invoke(currentPotion);
            effectRemainingTime--;
            yield return new WaitForSeconds(1f);
        }
        effectApplyingActions.TimerEndAction?.Invoke(currentPotion);
    }

    private void Start()
    {
        playerStateManager = GetComponent<ABC_StateManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        playerSetItems = FindObjectOfType<PlayerSetItems>();
    }
}
