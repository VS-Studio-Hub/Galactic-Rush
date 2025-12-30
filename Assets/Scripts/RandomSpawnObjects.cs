using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnObjects : MonoBehaviour
{
    public static RandomSpawnObjects instance;


    [SerializeField] private Transform[] leftObjectTransform;
    [SerializeField] private Transform[] rightObjectTransform;
    [SerializeField] private Transform[] centerObjectTransform;
    [SerializeField] private Transform[] downObjectTransform;
    [SerializeField] private Transform[] gemTransform;
    [SerializeField] private Transform[] pickUpsTransform;

    [SerializeField] private GameObject[] leftObjectsSpawn;
    [SerializeField] private GameObject[] rightObjectsSpawn;
    [SerializeField] private GameObject[] centerObjectsSpawn;
    [SerializeField] private GameObject[] downObjectsSpawn;
    [SerializeField] private GameObject gem;
    [SerializeField] private GameObject[] pickUps;

    public static GameObject gems;

    private SpawnManager spawnManager;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        GameObject currentLane = spawnManager.lastSpawnedLane;
        LeftObjectsSpawn();
        RightObjectsSpawn();
        CenterObjectsSpawn();
        DownObjectsSpawn();
        GemSpawn();
        PickUpsSpawn();
    }
    private void LeftObjectsSpawn()
    {
        for (int i = 0; i < leftObjectTransform.Length; i++)
        {
            int randomIndex = Random.Range(0, leftObjectsSpawn.Length);
            GameObject spawned = Instantiate(leftObjectsSpawn[randomIndex], leftObjectTransform[i].position, Quaternion.identity);
            spawned.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }
    private void RightObjectsSpawn()
    {
        for (int i = 0; i < rightObjectTransform.Length; i++)
        {
            int randomIndex = Random.Range(0, rightObjectsSpawn.Length);
            GameObject spawned = Instantiate(rightObjectsSpawn[randomIndex], rightObjectTransform[i].position, Quaternion.identity);
            spawned.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }
    private void CenterObjectsSpawn()
    {
        for (int i = 0; i < centerObjectTransform.Length; i++)
        {
            int randomIndex = Random.Range(0, centerObjectsSpawn.Length);
            GameObject spawned = Instantiate(centerObjectsSpawn[randomIndex], centerObjectTransform[i].position, Quaternion.identity);
            spawned.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }
    private void DownObjectsSpawn()
    {
        for (int i = 0; i < downObjectTransform.Length; i++)
        {
            int randomIndex = Random.Range(0, downObjectsSpawn.Length);
            GameObject spawned = Instantiate(downObjectsSpawn[randomIndex], downObjectTransform[i].position, Quaternion.identity);
            spawned.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }
    private void GemSpawn()
    {
        for (int i = 0; i < gemTransform.Length; i++)
        {
            //int randomIndex = Random.Range(0, gem.Length);
            gems = Instantiate(gem, gemTransform[i].position, Quaternion.identity);
            gems.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }

    private void PickUpsSpawn()
    {
        for (int i = 0; i < pickUpsTransform.Length; i++)
        {
            int randomIndex = Random.Range(0, pickUps.Length);
            GameObject spawned = Instantiate(pickUps[randomIndex], pickUpsTransform[i].position, Quaternion.identity);
            spawned.transform.SetParent(spawnManager.lastSpawnedLane.transform);
        }
    }
}