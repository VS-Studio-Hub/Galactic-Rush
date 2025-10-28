using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.gemCount += 1;
            Destroy(gameObject);
        }
    }
}
