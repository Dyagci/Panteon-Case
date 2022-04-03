using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float offSetZ;
    public float offSetX;
    private Quaternion rot;
    
    // Start is called before the first frame update
    void Start()
    {
        offSetZ = transform.position.z - player.transform.position.z;
        rot = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z+offSetZ);
        transform.rotation = rot;
    }
}
