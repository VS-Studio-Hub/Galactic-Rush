using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject[] longLane;
    public GameObject[] smallLane;

    public  GameObject currentLane;
    private bool laneSpawned = false;
    private Vector3 spawnPosition = new Vector3(0, -1.25f, 70);
    private float targetZPosition = 0f;
    int RandomAngle;
    int RandomRotation;
    bool Counter = true;
    private void Start()
    {
        SpawnLane();
    }

    public  void Update()
    {
        if (Counter)
        {
            RandomAngle = Random.Range(0, 4);
            Counter = false;
        }
        if (laneSpawned && currentLane.transform.position.z <= targetZPosition)
        {
            SpawnLane();
        }
        if (RandomAngle == 1)
        {
            RandomRotation = 90;
        }
        else if (RandomAngle == 2)
        {
            RandomRotation = 180;
        }
        else if (RandomAngle == 3)
        {
            RandomRotation = 270;
        }
        else if (RandomAngle == 0)
        {
            RandomRotation = 360;
        }
    }

    public  void SpawnLane()
    {
        
        int randomIndex = Random.Range(0, longLane.Length);
        currentLane = Instantiate(longLane[randomIndex], spawnPosition, Quaternion.Euler(0,0,RandomRotation));
        //currentLane.transform.rotation = Quaternion.identity;

        laneSpawned = true;
        Counter = true;
    }
}