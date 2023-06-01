using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PortalState
{
    Unstable,
    Stabilizing,
    Stable
}

public class DungeonPortal : InteractiveObject
{
    [Space]
    [SerializeField]
    private GameObject stabilizationEffect;
    [SerializeField]
    private int stabilizationTimeInSeconds;
    [SerializeField]
    private EnemiesSpawner enemiesSpawner;
    [SerializeField]
    private int spawnerAppearanceDelayInSeconds;

    private LevelFinisher levelFinisher;
    private PortalStabilizationProgressSection portalStabilizationProgressSection;
    private GameManager gameManager;
    private InventoryManager inventoryManager;

    public PortalState PortalState { get; private set; }

    public void Interact()
    {
        switch (PortalState)
        {
            case PortalState.Unstable:
                if (gameManager.StabilizerCreatedAndAvailable)
                {
                    inventoryManager.RemoveItemState(inventoryManager.Items.First(item => item.Title == "Стабилизатор портала"));
                    GetComponent<Collider>().enabled = false;
                    FindObjectOfType<InteractiveObjectPanel>().DisableUI();
                    StartCoroutine(StabilizePortal_COR());
                    PortalState = PortalState.Stabilizing;
                }
                else
                {
                    GetComponent<Collider>().enabled = false;
                    FindObjectOfType<InteractiveObjectPanel>().DisableUI();
                    levelFinisher.Activate();
                }
                break;

            case PortalState.Stable:
                GetComponent<Collider>().enabled = false;
                FindObjectOfType<InteractiveObjectPanel>().DisableUI();
                levelFinisher.FinishGame();
                break;
        }
    }

    private IEnumerator StabilizePortal_COR()
    {
        if (stabilizationEffect != null)
        {
            stabilizationEffect.SetActive(true);
        }
        StartCoroutine(portalStabilizationProgressSection.FillProgressBar_COR(stabilizationTimeInSeconds));
        yield return new WaitForSeconds(spawnerAppearanceDelayInSeconds);
        enemiesSpawner.gameObject.SetActive(true);
        yield return new WaitForSeconds(stabilizationTimeInSeconds - spawnerAppearanceDelayInSeconds);
        enemiesSpawner.gameObject.SetActive(false);
        if (stabilizationEffect != null)
        {
            stabilizationEffect.SetActive(false);
        }
        PortalState = PortalState.Stable;
        GetComponent<Collider>().enabled = true;
    }

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        gameManager = FindObjectOfType<GameManager>();
        levelFinisher = GetComponent<LevelFinisher>();
        portalStabilizationProgressSection = FindObjectOfType<PortalStabilizationProgressSection>();
    }
}
