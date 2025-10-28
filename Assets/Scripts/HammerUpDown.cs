using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerUpDown : MonoBehaviour
{
    private float moveSpeed = 1f;

    private float bottomY = 1f;
    private float topY = 6f;

    private float ramdomT;

    private Vector3 upPosition;
    private Vector3 downPosition;

    void Start()
    {
        upPosition = new Vector3(transform.position.x, bottomY, transform.position.z);
        downPosition = new Vector3(transform.position.x, topY, transform.position.z);

        ramdomT = Random.Range(0f, 100f);

        float startY = Random.Range(bottomY, topY);
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }

    void Update()
    {
        float t = Mathf.PingPong((Time.time + ramdomT) * moveSpeed, 1f);
        float newY = Mathf.Lerp(upPosition.y, downPosition.y, t);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
