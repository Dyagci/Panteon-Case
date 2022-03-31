using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] Vector3 move;
    private Rigidbody rb;
    private float movementAmount;
    private float firstMousePos;
    private Quaternion iniRot;
    public bool passedFinish;
    public GameObject wall;
    public Texture2D wallTexture;
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
        else if (passedFinish)
        {
            //PaintWall();
        }
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
        rb.velocity = move;
    }

    private void LateUpdate()
    {
        transform.rotation = iniRot;
    }

    private void PaintWall()
    {

    }
}
