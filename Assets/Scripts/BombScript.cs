using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Runs for the individual bomb
public class BombScript : MonoBehaviour {
    public float fuse = 2f;
    public float radius = 5f;
    public float force = 15f;
    private float fuseTimer = 0;
	
    public void Activate(){
        fuseTimer = 0;
    }

	// Update is called once per frame
	void Update () {
		if (gameObject.activeSelf){
            fuseTimer += Time.deltaTime;
            if (fuseTimer > fuse){
                Transform playerTransform = GameManager.instance.player.transform;
                Vector2 dist = playerTransform.position - transform.position;
                if (dist.magnitude < radius && dist.sqrMagnitude > 0)
                {
                    playerTransform.GetComponent<Rigidbody2D>().AddForce(dist.normalized * (force / dist.sqrMagnitude), ForceMode2D.Impulse);
                }
                gameObject.SetActive(false);
            }
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            fuseTimer = fuse;
        }
    }
}
