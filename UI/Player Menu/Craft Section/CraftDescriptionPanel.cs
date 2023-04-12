using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CreationAvailabilityState
{
    Available,
    NotEnoughItems,
    NotEnoughEnergy
}


public class CraftDescriptionPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text energyCounterLabel;
    [SerializeField]
    private TMP_Text energyCounter;
    [SerializeField]
    private TMP_Text creationAvailabilityStateText;
    [Space]
    [SerializeField]
    private Color creationAvailabilityColor;
    [SerializeField]
    private Color creationUnavailabilityColor;

    public void SetExtractedEnergyCountInfo(int currentEnergyCount, int requiredEnergyCount)
    {
        energyCounterLabel.text = "Выделено энергии:";
        energyCounter.text = string.Format("{0}/{1}", currentEnergyCount, requiredEnergyCount);
    }

    public void SetCreationAvailabilityState(CreationAvailabilityState state)
    {
        creationAvailabilityStateText.text = state switch
        {
            CreationAvailabilityState.Available => "Готово к созданию!",
            CreationAvailabilityState.NotEnoughItems => "Недостаточно ингредиентов!",
            CreationAvailabilityState.NotEnoughEnergy => "Недостаточно энергии!"
        };
        creationAvailabilityStateText.color = state switch
        {
            CreationAvailabilityState.Available => creationAvailabilityColor,
            _ => creationUnavailabilityColor
        };
    }

    public void SetDefaultInfo()
    {
        energyCounter.text = "";
        energyCounterLabel.text = "";
        creationAvailabilityStateText.text = "";
    }
}
