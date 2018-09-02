using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : MonoBehaviour {

    public int pointValue = 500;
    public float bounceForce = 0f;
    public Transform sprite;
    private float wobbleLerp = 0;
    private AudioSource source;

    private void Update()
    {
        if (wobbleLerp > 0){
            wobbleLerp = wobbleLerp - 0.5f * Time.deltaTime;
            sprite.transform.localScale = Vector3.one * EasingFunction.EaseInElastic(1, 12, wobbleLerp);
        } else {
            sprite.transform.localScale = Vector3.one;
        }
    }

    public float getWobble(){
        return wobbleLerp;
    }
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.rigidbody.velocity.magnitude > 2)
            {
                wobbleLerp = 0.5f;
                GameManager.instance.Score += pointValue;
                ScoreDisplay.instance.Spawn(transform.position, pointValue);
                if (bounceForce > 0){
                    other.rigidbody.velocity = Vector3.zero;
                    other.rigidbody.AddForce(other.contacts[0].normal * -bounceForce, ForceMode2D.Impulse);
                }
                if (source != null){
                    source.Play();
                }
            }
        }
    }

}
