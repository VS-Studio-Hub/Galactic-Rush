using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnObjects : MonoBehaviour
{
    [SerializeField] private Transform[] leftObject;
    [SerializeField] private Transform[] rightObject;
    [SerializeField] private Transform[] centerObject;
    [SerializeField] private Transform[] downObject;

    [SerializeField] private GameObject[] leftObjectsSpawn;
    [SerializeField] private GameObject[] rightObjectsSpawn;
    [SerializeField] private GameObject[] centerObjectsSpawn;
    [SerializeField] private GameObject[] downObjectsSpawn;


    private SpawnManager spawnManager;
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        GameObject currentLane = spawnManager.lastSpawnedLane;
        LeftObjectsSpawn();
        RightObjectsSpawn();
        CenterObjectsSpawn();
        DownObjectsSpawn();
    }
    private void LeftObjectsSpawn()
    {
        for (int i = 0; i < leftObject.Length; i++)
        {
            int randomIndex = Random.Range(0, leftObjectsSpawn.Length);
            GameObject spawned = Instantiate(leftObjectsSpawn[randomIndex], leftObject[i].position, Quaternion.identity);
            spawned.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }
    private void RightObjectsSpawn()
    {
        for (int i = 0; i < rightObject.Length; i++)
        {
            int randomIndex = Random.Range(0, rightObjectsSpawn.Length);
            GameObject spawned = Instantiate(rightObjectsSpawn[randomIndex], rightObject[i].position, Quaternion.identity);
            spawned.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }
    private void CenterObjectsSpawn()
    {
        for (int i = 0; i < centerObject.Length; i++)
        {
            int randomIndex = Random.Range(0, centerObjectsSpawn.Length);
            GameObject spawned = Instantiate(centerObjectsSpawn[randomIndex], centerObject[i].position, Quaternion.identity);
            spawned.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }
    private void DownObjectsSpawn()
    {
        for (int i = 0; i < downObject.Length; i++)
        {
            int randomIndex = Random.Range(0, downObjectsSpawn.Length);
            GameObject spawned = Instantiate(downObjectsSpawn[randomIndex], downObject[i].position, Quaternion.identity);
            spawned.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }
}