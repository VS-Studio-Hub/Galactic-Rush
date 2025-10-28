using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject lane;
    //[SerializeField] private GameObject[] SpLane;
    public int startLanes = 5;

    private Transform lastLaneEnd;

    public GameObject lastSpawnedLane;

    void Start()
    {
        // Spawn the first lane at (0,0,0)
        GameObject firstLane = Instantiate(lane, Vector3.zero, Quaternion.identity);
        lastLaneEnd = firstLane.transform.Find("LaneEnd");
        lastSpawnedLane = firstLane;
        // Spawn the rest
        for (int i = 0; i < startLanes - 1; i++)
        {
            SpawnNextLane();
        }
    }

    public void SpawnNextLane()
    {
        GameObject newLane = Instantiate(lane, Vector3.zero, Quaternion.identity);

        Transform laneStart = newLane.transform.Find("LaneStart");
        Transform laneEnd = newLane.transform.Find("LaneEnd");

        // Move lane so it aligns with the previous one
        Vector3 offset = newLane.transform.position - laneStart.position;
        newLane.transform.position = lastLaneEnd.position + offset;

        // Update last lane reference
        lastLaneEnd = laneEnd;
        lastSpawnedLane = newLane;
    }

    // ?? Called by a lane when it's destroyed
    public void OnLaneDestroyed()
    {
        SpawnNextLane();
    }
}