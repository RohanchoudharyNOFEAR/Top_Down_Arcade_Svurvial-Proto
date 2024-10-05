using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player; 
    public Vector3 Offset = new Vector3(0, 10, -10); 
    public float SmoothSpeed = 0.125f; 

    void LateUpdate()
    {
        Vector3 desiredPosition = Player.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(Player);
    }
}
