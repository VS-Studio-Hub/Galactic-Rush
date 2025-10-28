using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private float rotationSpeed = 200f;

    void Start()
    {
        
    }


    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
