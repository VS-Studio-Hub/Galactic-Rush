using System.Collections;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public SectionTrigger spawner;
    private GameObject lane;

    private Quaternion targetRotation;
    private bool isRotating = false;
    private float rotationSpeed = 6f;
    private bool canRotate = true;
    public bool Deselect = false;
    void Update()
    {
        if (spawner == null)
        {
            spawner = GameObject.Find("Player").GetComponentInChildren<SectionTrigger>();
        }
        if (spawner != null)
        {
            lane = spawner.currentLane;
        }
        if (Input.GetKeyDown(KeyCode.Q) && canRotate && !Deselect)
        {
            StartCoroutine(LeftRotateLane());
            Debug.Log("left");
        }
        if (Input.GetKeyDown(KeyCode.E) && canRotate && !Deselect)
        {
            StartCoroutine(RightRotateLane());
            Debug.Log("left");

        }

        if (isRotating && lane != null)
        {
            lane.transform.rotation = Quaternion.Slerp(lane.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(lane.transform.rotation, targetRotation) < 0.5f)
            {
                lane.transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    IEnumerator LeftRotateLane()
    {
        canRotate = false; 
        if (spawner != null)
        {
            lane = spawner.currentLane;
            if (lane != null)
            {
                targetRotation = lane.transform.rotation * Quaternion.Euler(0, 0, 90);
                isRotating = true;
            }
        }
        yield return new WaitForSeconds(1.5f);
        canRotate = true;
    }

    IEnumerator RightRotateLane()
    {
        canRotate = false;
        if (spawner != null)
        {
            lane = spawner.currentLane;
            if (lane != null)
            {
                targetRotation = lane.transform.rotation * Quaternion.Euler(0, 0, -90);
                isRotating = true;
            }
        }
        yield return new WaitForSeconds(1.5f);
        canRotate = true;
    }
}
