using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotatingPlatform : MonoBehaviour
{
    public float rotateSpeed;

    public int direction;

    [SerializeField]public int rotateAngle;
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
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlatform();
    }

    private void RotatePlatform()
    {
        gameObject.transform.Rotate(0f,0f,rotateSpeed*Time.deltaTime*rotateAngle* direction);
    }
}
