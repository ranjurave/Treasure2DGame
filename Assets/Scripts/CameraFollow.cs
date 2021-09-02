using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 cameraOffset;


    void Update()
    {
        PlayerMovement p = player.GetComponent<PlayerMovement>();
        if (p.isAlive) {
        Vector3 newPosition = player.transform.position + cameraOffset ;
        Vector3 smoothedCamera = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
        transform.position = smoothedCamera;
        }
    }
}

