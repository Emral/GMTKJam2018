using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : MonoBehaviour {

    public int pointValue = 500;
    public float bounceForce = 0f;

	// Use this for initialization
	void Start ()
    {
	}
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.rigidbody.velocity.magnitude > 2)
            {
                GameManager.instance.Score += pointValue;
                if (bounceForce > 0){
                    other.rigidbody.velocity = Vector3.zero;
                    other.rigidbody.AddForce(other.contacts[0].normal * -bounceForce, ForceMode2D.Impulse);
                }
            }
        }
    }

}
