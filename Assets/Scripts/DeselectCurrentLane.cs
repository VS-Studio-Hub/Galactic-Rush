using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectCurrentLane : MonoBehaviour
{

    // Start is called before the first frame update
    public RotatingPlatform CurrentLane = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentLane == null)
        {
            CurrentLane = GetComponentInParent<RotatingPlatform>();
        }

    }
    void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.name == "Player")
        {
            CurrentLane.Deselect = true;
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
        }
        
    }
}
