using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public ObjectManager om;
    public float x = 0;
    public float y = 0;
    public float z = -10;
    public float temp;

    //public int objectWall = 0;
    Vector3 cameraPosition;

    private void Start()
    {
        temp = 0;
    }
    private void LateUpdate()
    {
        if (player.transform.position.y > temp) {

            temp = player.transform.position.y;
        }
        cameraPosition.y = temp;
        cameraPosition.z = player.transform.position.z + z;
        transform.position = cameraPosition;



    }

}
