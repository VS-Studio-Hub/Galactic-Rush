using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLane : MonoBehaviour
{
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Update()
    {
        // Example condition: if lane goes too far behind the player
        if (transform.position.z > 100f)
        {
            spawnManager.OnLaneDestroyed(); // Tell manager to spawn new one
            Destroy(gameObject);            // Remove old lane
        }
    }
}