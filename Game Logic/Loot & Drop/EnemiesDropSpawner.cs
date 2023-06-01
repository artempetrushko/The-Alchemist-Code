using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDropSpawner : MonoBehaviour
{
    [SerializeField]
    private ItemsSpawnChancesTable spawnChancesTable;
    [SerializeField]
    private Transform itemsSpawnPosition;

    private void OnDisable()
    {
        var spawnedItemStates = spawnChancesTable.SpawnItems();
        foreach (var itemState in spawnedItemStates) 
        {
            var spawnedItem = Instantiate(itemState.PhysicalRepresentaionPrefab);
            spawnedItem.transform.position = new Vector3(itemsSpawnPosition.position.x + Random.Range(0, 2), 
                                                         itemsSpawnPosition.position.y,
                                                         itemsSpawnPosition.position.z + Random.Range(0, 2));
            spawnedItem.CurrentItemState = itemState;
        }
    }
}
