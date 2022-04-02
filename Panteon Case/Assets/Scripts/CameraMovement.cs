using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private CinemachineVirtualCamera camera;
    private Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        rot = camera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rot;
    }
}
