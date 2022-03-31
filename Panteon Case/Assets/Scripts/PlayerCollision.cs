using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private int thrust;
    public GameObject startReference;
    public GameObject paintPosRef;
    private Vector3 paintPos;
    private Rigidbody rb;
    private bool finished;
    public CinemachineVirtualCamera vcam1;
    public CinemachineVirtualCamera vcam2;
    Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        paintPos = paintPosRef.transform.position;
        finished = false;
    }

    private void Update()
    {
        if (finished && !animator.GetBool("isIdle"))
        {
            MovePaintPos();
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.transform.position = startReference.transform.position;
        }
        else if (collision.gameObject.CompareTag("RotPlat"))
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
        else if(collision.gameObject.CompareTag("RotStick"))
        {
            moveDirection = collision.gameObject.transform.parent.GetComponent<RotatingStick>().direction*collision.transform.right;
            moveDirection.y = 0;
            rb.AddRelativeForce( moveDirection.normalized *Time.deltaTime*thrust,ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine"))
        {
            GetComponent<PlayerController>().passedFinish = true;
            rb.velocity = Vector3.zero;
            finished = true;
        }
        else if (other.gameObject.CompareTag("PaintPos"))
        {
            animator.SetBool("isIdle",true);
        }
    }

    private void MovePaintPos()
    {
        transform.position = Vector3.MoveTowards(transform.position,paintPos,0.2f*Time.deltaTime);
    }
}
