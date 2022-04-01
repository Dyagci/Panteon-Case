using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField]public int direction;
    [SerializeField]public int rotateAngle;
    public float rotateSpeed;
    
    void Update()
    {
        RotatePlatform();
    }

    private void RotatePlatform()
    {
        gameObject.transform.Rotate(0f,0f,rotateSpeed*Time.deltaTime*rotateAngle* direction);
    }
}
