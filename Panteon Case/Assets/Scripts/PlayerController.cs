using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] Vector3 move;
    public bool passedFinish;
    private float movementAmount;
    private float firstMousePos;
    private Rigidbody rb;
    private RaycastHit hit;
    private Vector3 pos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        move = Vector3.forward * forwardSpeed;
        passedFinish = false;
    }
    private void Update()
    {
        if (!passedFinish)
        {
            Movement();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = move *Time.deltaTime;
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePos = Input.mousePosition.x;
        }
        else if(Input.GetMouseButton(0))
        {
            movementAmount = Input.mousePosition.x - firstMousePos;
            movementAmount = Mathf.Clamp(movementAmount, -2, 2);
            move = new Vector3(movementAmount, 0, 0);
            move = (move * horizontalSpeed + Vector3.forward*forwardSpeed);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            move = Vector3.forward * forwardSpeed;
        }

    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotateSpeed);
    }
}
