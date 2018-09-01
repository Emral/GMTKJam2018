using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Applies rigidbody force based on the transform's velocity change.
public class Pusher : MonoBehaviour {
    
    private Vector3 lastPosition;     //derive speed from lastPosition and apply to intersecting bodies.
    private Vector3 lastSpeed;        //use for propelling.

    public float forceMultiplier = 5; //for some extra power

    private void Start()
    {
        lastPosition = transform.position;
    }

    void LateUpdate () {
        lastSpeed = transform.position - lastPosition;
		lastPosition = transform.position;
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D collisionBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collisionBody){
            collisionBody.AddForce(lastSpeed * forceMultiplier, ForceMode2D.Impulse);
        }
    }
}
