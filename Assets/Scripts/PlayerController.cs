using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float laneDistance = 1.5f;
    private int currentLane = 1;

    private float moveSpeed = 10.0f;
    private Vector3 targetPosition;

    public bool canMove = true;
    public bool isJumping = false;
    public bool comingDown = false;
    public bool isSliding = false;
    public bool comingUp = false;

    public bool isJump = true;

    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private Vector3 originalCenter;
    private float originalHeight;

    void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        originalCenter = capsuleCollider.center;
        originalHeight = capsuleCollider.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver)
            return;
        float speed = 10;
        animator.SetFloat("Speed", speed);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJump)
        {
            if (isJumping == false)
            {
                isJumping = true;
                animator.SetBool("IsJumping", true);
                StartCoroutine(JumpSequence());
            }
        }


        if (isJumping == true)
        {
            if (comingDown == false)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 10, Space.World);
            }
            if (comingDown == true)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 5, Space.World);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(isSliding == false)
            {
                isSliding = true;
                SlideStart();
                animator.SetBool("IsSliding", true);
                StartCoroutine(SlideSequence());
            }
        }

        transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    

    private void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            targetPosition.x = (currentLane - 1) * laneDistance;
        }
    }

    private void MoveRight()
    {
        if (currentLane < 2)
        {
            currentLane++;
            targetPosition.x = (currentLane - 1) * laneDistance;
        }
    }

    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(0.55f);
        comingDown = true;
        yield return new WaitForSeconds(0.30f);
        isJumping = false;
        comingDown = false;
        animator.SetBool("IsJumping", false);
    }

    IEnumerator SlideSequence()
    {
        yield return new WaitForSeconds(0.55f);
        comingUp = true;
        yield return new WaitForSeconds(0.30f);
        isSliding = false;
        comingUp = false;
        animator.SetBool("IsSliding", false);
        SlideEnd();
    }
    public void SlideStart()
    {
        capsuleCollider.height = originalHeight / 2f;
        capsuleCollider.center = new Vector3(originalCenter.x, originalCenter.y / 2f, originalCenter.z);
        isJump = false;
    }

    public void SlideEnd()
    {
        capsuleCollider.height = originalHeight;
        capsuleCollider.center = originalCenter;
        isJump = true;
    }
}