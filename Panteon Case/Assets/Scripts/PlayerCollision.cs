using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private int thrust;
    public GameObject startReference;
    private Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("RotPlat"))
        {
            //gameObject.transform.parent = null;
            //gameObject.transform.rotation= Quaternion.Euler(0,0,0);
        }
    }
}
