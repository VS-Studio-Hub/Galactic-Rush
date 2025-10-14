using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSaw : MonoBehaviour
{
    private Rigidbody Rigid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3();
        rotation += Vector3.up;
        if (Rigid == null)
        {
            Rigid = GetComponent<Rigidbody>();
        }
        transform.Rotate(rotation * Time.deltaTime * 100 * -1);
        //Rigid.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        //Rigid.constraints = ;
    }
}
