using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    GameObject player;
    public float radius = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        ActiveMagnet();
    }

    private void ActiveMagnet()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (PowerUps.magnetActive)
        {
            if (distance <= radius)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, 25f * Time.deltaTime);
            }
            
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.gemCount += 1;
            Destroy(gameObject);
        }
    }
}
