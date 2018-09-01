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
            //GameManager.instance.score += pointValue;
            GameManager.instance.score.Score += pointValue;

        }
    }

}
