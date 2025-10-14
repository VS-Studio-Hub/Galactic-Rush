using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzzsaw : MonoBehaviour
{
    private bool Attached = false;
    private Transform Handle;
    private Rigidbody Rigid;
    private bool moveRight = true;
    Vector2 DesiredMoving = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3();
        rotation += Vector3.forward;
        if ( Rigid == null)
        {
            Rigid = GetComponent<Rigidbody>();
        }
        if (Attached)
        {
            transform.position = new Vector3 (Handle.transform.position.x + 1.16f, Handle.transform.position.y - 0.12f, Handle.transform.position.z - 0.039f);
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, (transform.rotation.z + 100) * Time.deltaTime);
            //transform.Rotate(rotation * Time.deltaTime * 100 * -1);
            transform.localEulerAngles += new Vector3(0, 0, 6*100*Time.deltaTime);
            Rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            Rigid.constraints = RigidbodyConstraints.FreezeRotation;
            //Rigid.constraints = ;
        }
        else if(!Attached)
        {
            if (moveRight)
            {
                DesiredMoving = new Vector2(1, 0) * Time.deltaTime * 10;
                Move(DesiredMoving);
                transform.localEulerAngles += new Vector3(0, 0, 6 * 100 * Time.deltaTime);
                Rigid.constraints = RigidbodyConstraints.FreezePositionY;
                Rigid.constraints = RigidbodyConstraints.FreezeRotation;
            }
            if (!moveRight)
            {
                DesiredMoving = new Vector2(-1, 0) * Time.deltaTime * 10;
                Move(DesiredMoving);
                transform.localEulerAngles += new Vector3(0, 0, 6 * 100 * Time.deltaTime);
                Rigid.constraints = RigidbodyConstraints.FreezePosition;
                Rigid.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Handle")
        {
            Attached = true;
            Handle = collision.gameObject.GetComponent<Transform>();
        }
        if (collision.gameObject.CompareTag("SawBarrier"))
        {
            moveRight = !moveRight;
        }
    }
    public void Move(Vector2 Velocity)
    {
        transform.position += new Vector3(Velocity.x, Velocity.y);
    }
}
