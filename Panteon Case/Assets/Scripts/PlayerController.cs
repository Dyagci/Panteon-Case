using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] Vector3 move;
    private Rigidbody rb;
    private float movementAmount;
    private float firstMousePos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        move = Vector3.forward * forwardSpeed;
    }

    private void Update()
    {
        Movement();
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
}
