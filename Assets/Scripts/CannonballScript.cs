using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour {

    public float speed = 5f;        // Speed at which the ball travels
    public float radius = 5f;       // The radius in which the cannonball affects the player
    public float force = 15f;       // The strength of the cannonball's explosion upon destruction

    Rigidbody2D rig;                // Reference to the rigidbody

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        //Move the ball towards the right
        rig.velocity = rig.transform.right * speed;
	}

    //This is more or less copied and pasted from bombscript. Only difference; object destroyes itself instead of deactivating.
    private void OnCollisionEnter2D(Collision2D other)
    {
        Transform playerTransform = GameManager.instance.player.transform;
        Vector2 dist = playerTransform.position - transform.position;
        if (dist.magnitude < radius && dist.sqrMagnitude > 0)
        {
            playerTransform.GetComponent<Rigidbody2D>().AddForce(dist.normalized * (force / dist.sqrMagnitude), ForceMode2D.Impulse);
        }
        gameObject.SetActive(false);
    }
}
