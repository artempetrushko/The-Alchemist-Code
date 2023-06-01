using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int requiredStabilizerPartsCount;
    [SerializeField]
    private ItemData stabilizerPartDataTemplate;
    [SerializeField]
    private ItemData stabilizerDataTemplate;

    private QuestProgressSection questProgressSection;
    private InventoryManager inventoryManager;
    private int stabilizerPartsCount;
    private bool stabilizerCreatedAndAvailable;

    public ItemData StabilizerPartDataTemplate => stabilizerPartDataTemplate;
    public ItemData StabilizerDataTemplate => stabilizerDataTemplate;
    public int StabilizerPartsCount
    {
        get => stabilizerPartsCount;
        set
        {
            if (value != stabilizerPartsCount)
            {
                stabilizerPartsCount = value;
                ShowCurrentQuestRequirement();
            }            
        }
    }
    public bool PlayerFinishedLevelEver { get; set; } = true;
    public bool StabilizerCreatedAndAvailable
    {
        get => stabilizerCreatedAndAvailable;
        set
        {
            if (value != stabilizerCreatedAndAvailable)
            {
                stabilizerCreatedAndAvailable = value;
                ShowCurrentQuestRequirement();
            }         
        }
    }
    public string QuestRequirement
    {
        get
        {
            string requirement;
            if (PlayerFinishedLevelEver)
            {
                if (StabilizerCreatedAndAvailable)
                {
                    requirement = "Стабилизируйте портал и сбегите";
                }
                else if (StabilizerPartsCount >= requiredStabilizerPartsCount) 
                {
                    requirement = "Изготовьте стабилизатор";
                }
                else
                {
                    requirement = string.Format("Найдите части стабилизатора для портала ({0}/{1})", StabilizerPartsCount, requiredStabilizerPartsCount);
                }              
            }
            else
            {
                requirement = "Найдите способ выбраться из подземелий";
            }
            return requirement;
        }
    }

    public List<ItemState> CreateStartSpecialItems()
    {
        var specialItems = new List<ItemState>();
        if (stabilizerPartsCount > 0)
        {
            var stabilizerParts = stabilizerPartDataTemplate.GetItemState();
            (stabilizerParts as ResourceState).ItemsCount = StabilizerPartsCount;
            specialItems.Add(stabilizerParts);
        }
        if (stabilizerCreatedAndAvailable)
        {
            specialItems.Add(stabilizerDataTemplate.GetItemState());
        }
        return specialItems;
    }

    public void CountSpecialItems()
    {
        var stabilizerParts = inventoryManager.Items.Where(item => item is ResourceState && item.Title == "Часть стабилизатора").ToList();
        StabilizerPartsCount = stabilizerParts.Count > 0
            ? stabilizerParts.Sum(item => (item as ResourceState).ItemsCount)
            : 0;
        StabilizerCreatedAndAvailable = inventoryManager.Items.Any(item => item is ResourceState && item.Title == "Стабилизатор портала");
    }

    public void ShowCurrentQuestRequirement() => questProgressSection.ShowQuestProgress(QuestRequirement);

    public void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("PlayerFinishedLevelEver", PlayerFinishedLevelEver ? 1 : 0);
        PlayerPrefs.SetInt("StabilizerCreatedAndAvailable", StabilizerCreatedAndAvailable ? 1 : 0);
        PlayerPrefs.SetInt("StabilizerPartsCount", stabilizerPartsCount);
        PlayerPrefs.Save();
    }

    private void LoadPlayerProgress()
    {
        PlayerFinishedLevelEver = PlayerPrefs.GetInt("PlayerFinishedLevelEver") == 1;
        stabilizerCreatedAndAvailable = PlayerPrefs.GetInt("StabilizerCreatedAndAvailable") == 1;
        stabilizerPartsCount = PlayerPrefs.GetInt("StabilizerPartsCount");
    }

    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        questProgressSection = FindObjectOfType<QuestProgressSection>();
        LoadPlayerProgress();
    }
}
