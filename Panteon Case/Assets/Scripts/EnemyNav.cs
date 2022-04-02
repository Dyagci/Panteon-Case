using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private Vector3 move;
    public GameObject finishline;
    public Rigidbody rb;
    private NavMeshAgent agent;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //EnemeyMovement();
        rb.velocity = Vector3.forward * forwardSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.transform.position = startPos;
        }
        else if (collision.gameObject.CompareTag("RotPlat"))
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            transform.parent = null;
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
}
