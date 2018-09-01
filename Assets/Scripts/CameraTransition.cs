using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------------------------------------------------------------------------
   Author: Lena
   Purpose: To move the camera to a new position when a player enters a new section of the board. The size of the camera can also be adjusted
-------------------------------------------------------------------------------------------------------------------------------------------*/

public class CameraTransition : MonoBehaviour
{

    public Vector3 newPosition;     // Position the camera will move to
    public int newSize;             // The new orthographic size of the camera

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraController>().targetPosition = newPosition;
            Camera.main.GetComponent<CameraController>().targetSize = newSize;
        }
    }
}
