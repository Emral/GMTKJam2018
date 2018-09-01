using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float smoothing = .025f;     // Controls how quickly the camera moves
    public Vector2 targetPosition;      // Where the camera is trying to be
    public float targetSize;            // The size the camera is trying to maintain (This assumes the camera is orthagraphic)

	void Start ()
    {
        // Set the targets to the camera's inital position
        targetPosition = transform.position;
        targetSize = GetComponent<Camera>().orthographicSize;
	}
	
    // Update size and location according to the target variables. Target variables are changed by outside scripts
	void Update ()
    {
        transform.position = Vector2.Lerp(transform.position, targetPosition, smoothing);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetSize, smoothing);
    }
}
