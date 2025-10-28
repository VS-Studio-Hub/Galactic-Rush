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
        // Find SectionTrigger once
        if (spawner == null)
        {
            var player = GameObject.Find("Player");
            if (player != null)
                spawner = player.GetComponentInChildren<SectionTrigger>();
        }

        // Track current lane
        if (spawner != null)
            lane = spawner.currentLane;

        // Handle input
        if (!Deselect && canRotate)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                StartCoroutine(RotateLane(90));
            else if (Input.GetKeyDown(KeyCode.E))
                StartCoroutine(RotateLane(-90));
        }

        // Smooth rotation
        if (isRotating && lane != null)
        {
            lane.transform.rotation = Quaternion.Slerp(
                lane.transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );

            // Finish when almost at target
            if (Quaternion.Angle(lane.transform.rotation, targetRotation) < 0.5f)
            {
                lane.transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    private IEnumerator RotateLane(float angle)
    {
        canRotate = false;
        if (lane != null)
        {
            targetRotation = lane.transform.rotation * Quaternion.Euler(0, 0, angle);
            isRotating = true;
        }
        yield return new WaitForSeconds(1.5f);
        canRotate = true;
    }
}
