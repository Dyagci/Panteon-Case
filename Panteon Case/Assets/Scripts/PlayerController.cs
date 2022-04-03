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
    public GameObject wall;
    public Texture2D wallTexture;
    public bool passedFinish;
    private float movementAmount;
    private float firstMousePos;
    private Quaternion iniRot;
    private Rigidbody rb;
    private RaycastHit hit;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        move = Vector3.forward * forwardSpeed;
        iniRot = transform.rotation;
        passedFinish = false;
    }
    private void Update()
    {
        if (!passedFinish)
        {
            Movement();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = move ;
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
