using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private int thrust;
    private Rigidbody rb;
    [SerializeField] private GameObject startReference;

    public EnemyNav nav;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<EnemyNav>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            nav.isFinished = true;
        }
    }
}
