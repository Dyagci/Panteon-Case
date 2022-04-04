using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private int thrust;
    public CinemachineVirtualCamera vcam1;
    public CinemachineVirtualCamera vcam2;
    public GameObject startReference;
    public Animator wallAnimator;
    public ScoreBoard scoreBoard;
    public Text percText;
    private Rigidbody rb;
    private bool finished;
    private Animator animator;
    private float originalY;
    private quaternion ogRot;
    private RaycastHit hit;

    private void Awake()
    {
        if (Screen.height>Screen.width)
        {
            Screen.SetResolution(Screen.width,Screen.height,true);
        }
        else
        {
            Screen.SetResolution(Screen.height,Screen.width,true);
        }
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        finished = false;
    }
    private void Update()
    {
        if (finished)
        {
            if (animator.GetBool("hasWon")||animator.GetBool("hasLost"))
            {
                vcam1.Priority = 0;
                vcam2.Priority = 1;
                rb.isKinematic = true;
                wallAnimator.SetTrigger("isFinished");
                transform.rotation =ogRot;
                if (!(wallAnimator.GetCurrentAnimatorStateInfo(0).length > wallAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime ))
                {
                    percText.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.transform.position = startReference.transform.position;
            animator.SetBool("isFalling",false);
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
            finished = true;
            other.gameObject.SetActive(false);
            if (scoreBoard.FindPlacement(this.gameObject)==1)
            {
                animator.SetBool("hasWon",true);
            }
            else
            {
                animator.SetBool("hasLost",true);
            }
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
