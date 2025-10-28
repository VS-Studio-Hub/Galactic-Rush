using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject[] longLane;
    public GameObject[] smallLane;

    public GameObject currentLane;
    private bool laneSpawned = false;

    private Vector3 spawnPosition = new Vector3(0, -1.25f, 70);
    private float targetZPosition = 0f;

    private int randomRotation;
    private bool counter = true;

    private void Start()
    {
        SpawnLane();
    }

    public void Update()
    {
        if (laneSpawned && currentLane.transform.position.z <= targetZPosition)
        {
            SpawnLane();
        }
    }

    public void SpawnLane()
    {
        // Choose a random rotation (0, 90, 180, 270)
        int randomAngle = Random.Range(0, 4);
        randomRotation = randomAngle * 90;

        int randomIndex = Random.Range(0, longLane.Length);
        currentLane = Instantiate(longLane[randomIndex], spawnPosition, Quaternion.Euler(0, 0, randomRotation));

        laneSpawned = true;
        counter = true;
    }
}
