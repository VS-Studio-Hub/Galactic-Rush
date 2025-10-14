using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 originalPosition;

    private CapsuleCollider capsuleCollider;
    private Rigidbody playerRb;

    private float originalHeight;
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale = new Vector3(1, 1f, 1);
    private float jumpForce = 5f;

    private float laneDistance = 2.0f; // Distance between lanes
    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right

    private bool isGrounded = true;
    private Rigidbody Lane;
    private EnergyBar energyBar;
    private GameObject Invincible;
    private 
    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;

        capsuleCollider = GetComponent<CapsuleCollider>();
        playerRb = GetComponent<Rigidbody>();

        originalHeight = capsuleCollider.height;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StandUp();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }

    private void MoveRight()
    {
        if (currentLane < 2) 
        {
            currentLane++;
            MoveToLane();
        }
    }

    private void MoveLeft()
    {
        if (currentLane > 0) 
        {
            currentLane--;
            MoveToLane();
        }
    }

    private void MoveToLane()
    {
        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }

    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void Crouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x,transform.position.y - 0.5f, transform.position.z);
    }

    void StandUp()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube" || collision.gameObject.name == "Cube (1)")
        {
            Lane= collision.gameObject.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(GetComponent<Collider>(), Lane.gameObject.GetComponent<Collider>(), true);
        }
        if (collision.gameObject.CompareTag("Collectible"))
        {
            energyBar = GameObject.Find("EnergyBar").GetComponent<EnergyBar>();
            energyBar.current += 10;
            Destroy(collision.gameObject);
            if (energyBar.current == energyBar.maximum)
            {

            }
        }
        if (collision.gameObject.name == "Cube (2)")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), Lane.gameObject.GetComponent<Collider>(), true);
        }
    }
    
}
