using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    [SerializeField] float floorLength;
    [SerializeField] float obstacleSpeed;
    [SerializeField] private int direction;
    public GameObject floor;
    
    void Start()
    {
        floorLength = floor.GetComponent<MeshRenderer>().bounds.extents.x;
    }
    void Update()
    {
        ObstacleMovement();
    }

    private void ObstacleMovement()
    {
        transform.position += transform.right * obstacleSpeed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sides"))
        {
            direction *= -1;
        }
    }
}
