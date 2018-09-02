using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* I tried my best with this, and it seems to be functional. I was having a hard time understanding how your moving object in your test 
   scene was deactivating; I hope what I did here was okay. I think this will do what we intend for the pufferfish just fine, but I've left detailed 
   comments so you can figure out what's going on and change anything if necessary. 
*/

public class PufferFish : MonoBehaviour
{
    public float Timer;                 // Controls how long the puffer fish stays inflated
    public float inflatedSize = 20;     // How big the puffer fish gets when inflated
    public float power = 10;            // How strong is the push force?

    float timer;                        // Counts down. Puffer fish deflates when this reaches zero
    float easeTimer = 0;                // Used to ease to the maximum size;
    float initialSize;                  // The default size of the puffer fish; what it is when deflated
    bool inflated;                      // Is the pufferfish deflated?
    ClickBattery bat;                   // Reference to the click battery script on this object
    Animator anim;
    //Set references
    private void Start()
    {
        initialSize = transform.localScale.x;
        bat = GetComponent<ClickBattery>();
        anim = GetComponent<Animator>();
    }

    private void Inflate(){
        inflated = true;
        timer = Timer;
        anim.SetBool("puffed", true);
    }

    // If inflated, begin the countdown. When it reaches zero, set inflated to false and return the object to it's
    // normal size. Turn off the power of the battery script to ensure it will glow when the mouse it over it again.  
    private void LateUpdate()
    {
        anim.SetBool("puffed", inflated);
        if (inflated)
        {
            if (easeTimer < 1){
                easeTimer += 2* Time.deltaTime;
                easeTimer = Mathf.Min(easeTimer, 1);
                transform.localScale = Vector3.one * EasingFunction.EaseOutElastic(initialSize, inflatedSize, easeTimer);
            }
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                inflated = false;
                anim.SetBool("puffed", false);
                bat.PowerOff();
            }
        } else if (easeTimer > 0)
        {
            easeTimer -= Time.deltaTime;
            easeTimer = Mathf.Max(easeTimer, 0);
            transform.localScale = Vector3.one * EasingFunction.EaseInElastic(initialSize, inflatedSize, easeTimer);
        }
        if (bat.GetActive()) {
            Inflate();
            bat.PowerOff();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BouncePlayer(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        BouncePlayer(other);
    }


    // If the object is inflated, find the vector from the player to the pufferfish and use it as a basis for adding force. Note that
    // the force seems have a bigger impact if the player was on top of the object beforehand; this might need adjustment later. 
    void BouncePlayer(Collider2D other)
    {
        Inflate();
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 direction = other.transform.position - transform.position;
            other.attachedRigidbody.velocity = Vector3.zero;
            other.attachedRigidbody.AddForce(direction * power, ForceMode2D.Impulse);
        }
    }
}
