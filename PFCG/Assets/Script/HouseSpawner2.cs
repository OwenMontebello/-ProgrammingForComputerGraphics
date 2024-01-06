using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner2 : MonoBehaviour
{
    public GameObject[] housePrefabs; // Assign your house prefabs here
    public int numberOfHouses = 5; // Number of houses to spawn
    public Vector2 spawnArea = new Vector2(100, 100); // Define spawn area size

    private void Start()
    {
        SpawnHouses();
    }

    void SpawnHouses()
    {
        for (int i = 0; i < numberOfHouses; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            bool canSpawnHere = false;

            while (!canSpawnHere)
            {
                spawnPosition = new Vector3(
                    Random.Range(-spawnArea.x / 2, spawnArea.x / 2), 0, 
                    Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
                );

                canSpawnHere = CheckIfPositionIsFree(spawnPosition);
            }

            // Randomly select a house prefab
            GameObject selectedHousePrefab = housePrefabs[Random.Range(0, housePrefabs.Length)];

            Instantiate(selectedHousePrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool CheckIfPositionIsFree(Vector3 position)
    {
        // You might want to adjust this radius based on the size of your houses
        float checkRadius = 100f;

        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius);
        if (hitColliders.Length > 0) // We hit something, can't spawn here
        {
            return false;
        }

        return true;
    }
}