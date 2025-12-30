using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform carTransform;
    public Transform cameraPointTransform;

    Vector3 velocity = Vector3.zero;
    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.LookAt(carTransform);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPointTransform.position, ref velocity, 5f * Time.deltaTime);
    }
}
