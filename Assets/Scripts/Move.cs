using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * GameManager.instance.MoveSpeed());
        if (transform.position.z > 105)
        {
            Destroy(gameObject);
        }
    }
}
