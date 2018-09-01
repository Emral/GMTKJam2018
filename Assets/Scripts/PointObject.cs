using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : MonoBehaviour {

    public int pointValue = 500;

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
                GameManager.instance.score.Score += pointValue;
            }
        }
    }

}
