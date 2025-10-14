using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSpawnManager : MonoBehaviour
{
    public GameObject lane;
    public GameObject currentLane;
    private bool laneSpawned = false;
    private Vector3 spawnPosition = new Vector3(0, 0, 0);
    private float targetZPosition = 0f;
    private float spawnDelay = .21f; // Delay between spawns in seconds


    private void Start()
    {
        currentLane = Instantiate(lane, spawnPosition, Quaternion.identity);
        laneSpawned = true;
        StartCoroutine(SpawnLaneRoutine()); // Start the coroutine to spawn lanes over time
    }


    private IEnumerator SpawnLaneRoutine()
    {
        while (true) // Infinite loop to keep spawning lanes
        {
            yield return new WaitForSeconds(spawnDelay); // Wait for the delay

            if (laneSpawned && currentLane.transform.position.z <= targetZPosition)
            {
                currentLane = Instantiate(lane, spawnPosition, Quaternion.identity);
            }
        }
    }
}
