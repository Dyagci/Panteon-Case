using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private Vector3 move;
    public GameObject finishline;
    public Rigidbody rb;
    private NavMeshAgent agent;
    private Vector3 startPos;
    [SerializeField] private GameObject[] frontSensors;
    [SerializeField]private Vector3 agentVelocity;
    private float agentHorizontalSpeed;
    private bool blocked = false;
    private bool blockedCrossRight = false;
    private bool blockedCrossLeft = false;
    private bool blockedRight = false;
    private bool blockedLeft = false;
    [SerializeField]private float avoidance;
    private NavMeshHit hit;
    public Vector3 curVelocity;
    private Vector3 dest;
    public bool isFinished;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        
        dest = finishline.transform.position;
        dest.y = transform.position.y;
        agent.destination = dest;
    }

    // Update is called once per frame
    void Update()
    {
        blocked = NavMesh.Raycast(transform.position, transform.position+2*Vector3.forward, out hit, NavMesh.AllAreas);
        blockedCrossLeft = NavMesh.Raycast(transform.position, transform.position+Vector3.forward+Vector3.left, out hit, NavMesh.AllAreas);
        blockedCrossRight = NavMesh.Raycast(transform.position, transform.position+Vector3.forward+Vector3.right, out hit, NavMesh.AllAreas);
        blockedLeft = NavMesh.Raycast(transform.position, transform.position+Vector3.left, out hit, NavMesh.AllAreas);
        blockedRight = NavMesh.Raycast(transform.position, transform.position+Vector3.right, out hit, NavMesh.AllAreas);
        Debug.DrawLine(transform.position, transform.position+Vector3.forward*2+Vector3.left, blockedCrossLeft ? Color.red : Color.green);
        Debug.DrawLine(transform.position, transform.position+Vector3.forward*2+Vector3.right, blockedCrossRight ? Color.red : Color.green);
        Debug.DrawLine(transform.position, transform.position+2*Vector3.forward, blocked ? Color.red : Color.green);
        Debug.DrawLine(transform.position, transform.position+Vector3.left, blockedLeft ? Color.red : Color.green);
        Debug.DrawLine(transform.position, transform.position+Vector3.right, blockedRight ? Color.red : Color.green);
    }   

    private void FixedUpdate()
    {
        if (!isFinished)
        {
            agentVelocity.z = forwardSpeed;
            agentVelocity.y = 0;
            if (blocked&& blockedCrossLeft)
            {
                agentVelocity.x = horizontalSpeed;
            }
            else if (blocked&& blockedCrossRight)
            {
                agentVelocity.x = -horizontalSpeed;
            }
            else if (blockedRight)
            {
                agentVelocity.x = -horizontalSpeed;
            }
            else if (blockedLeft)
            {
                agentVelocity.x = horizontalSpeed;
            }
            else
            {
                agentVelocity.x = 0;
            }
            rb.velocity = agentVelocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    private void EnemeyMovement()
    {
        if (agent.nextPosition.x < agent.transform.position.x)
        {
            move = Vector3.left * horizontalSpeed + Vector3.forward * forwardSpeed;
        }
        else if(agent.nextPosition.x > agent.transform.position.x)
        {
            move = Vector3.right * horizontalSpeed + Vector3.forward * forwardSpeed;
        }
        else
        {
            move =Vector3.forward * forwardSpeed;
        }
        rb.velocity = move;
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(agentVelocity), Time.deltaTime * rotateSpeed);
    }
}
