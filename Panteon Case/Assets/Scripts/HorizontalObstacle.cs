using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    [SerializeField] float floorLength;
    [SerializeField] float obstacleSpeed;
    int direction;
    public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
        int randomNum = Random.Range(0, 2);
        if (randomNum == 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        floorLength = floor.GetComponent<MeshRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleMovement();
    }

    private void ObstacleMovement()
    {

        transform.position += transform.right * obstacleSpeed * Time.deltaTime * direction;
        if(transform.position.x >= floorLength || transform.position.x <= -floorLength)
        {
            direction *= -1;
        }
    }
}
