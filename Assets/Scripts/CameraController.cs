using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float smoothing = 1;         // Controls how quickly the camera moves
    public Vector3 targetPosition;      // Where the camera is trying to be
    public float targetSize;            // The size the camera is trying to maintain (This assumes the camera is orthagraphic)
    private Camera cam;                 // Get a reference to the camera to avoid GetComponent twice per frame...

	void Start ()
    {
        // Set the targets to the camera's inital position
        targetPosition = transform.position;
        cam = GetComponent<Camera>();
        targetSize = cam.orthographicSize;
	}
	
    // Update size and location according to the target variables. Target variables are changed by outside scripts
	void Update ()
    {
        if (transform.position != targetPosition && smoothing >= 1){
            Time.timeScale = 0;
            smoothing = 0;
        }
        if (!PauseMenu.paused && smoothing < 1){
            smoothing = Mathf.Min(smoothing + 0.02f, 1);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, smoothing);
            Time.timeScale = smoothing;
        }
    }
}
