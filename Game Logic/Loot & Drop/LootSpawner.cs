using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField]
    private ItemsSpawnChancesTable spawnChancesTable;

    private BoxCollider spawnArea;

    private void SpawnLoot()
    {
        var spawnedItemStates = spawnChancesTable.SpawnItems();
        foreach (var itemState in spawnedItemStates)
        {
            var spawnedItem = Instantiate(itemState.PhysicalRepresentaionPrefab, transform);
            spawnedItem.CurrentItemState = itemState;
            spawnedItem.transform.position = new Vector3(transform.position.x + Random.Range(spawnArea.size.x / 2, -spawnArea.size.x / 2),
                                                         transform.position.y + spawnArea.center.y,
                                                         transform.position.z + Random.Range(spawnArea.size.z / 2, -spawnArea.size.z / 2));
        }
    }

    private void OnEnable()
    {
        spawnArea = GetComponent<BoxCollider>();
        SpawnLoot();
    }
}
