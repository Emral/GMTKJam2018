using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitScript : MonoBehaviour {

    Vector3 playerSpawnPosition;        // Reference to the position the player should spawn 
    Vector3 cameraInitialPosition;      // Reference to where the camera should be when the player is spawned. 
    float cameraInitialSize;            // Reference to size of the camera upon spawining a player

    CameraController cam;               // Reference to the camera controller

	// Use this for initialization
	void Start ()
    {
        playerSpawnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        cameraInitialPosition = Camera.main.transform.position;
        cameraInitialSize = Camera.main.orthographicSize;
        cam = Camera.main.GetComponent<CameraController>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.lives -= 1;

            if(GameManager.instance.lives <= 0)
            {
                GameManager.instance.Reset();
            }
            else
            {
                other.transform.position = playerSpawnPosition;
                cam.targetPosition = cameraInitialPosition;
                cam.targetSize = cameraInitialSize;
            }
        }
        Debug.Log(GameManager.instance.lives);
    }

}
