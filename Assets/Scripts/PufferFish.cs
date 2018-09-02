using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* I tried my best with this, and it seems to be functional. I was having a hard time understanding how you're moving object was deactivating
   in your test scene; I hope what I did here was okay. I think this will do what we intend for the pufferfish just fine, but I've left detailed 
   comments so you can figure out what's going on and change anything if necessary. 
*/

public class PufferFish : MonoBehaviour
{
    public float Timer;                 // Controls how long the puffer fish stays inflated
    public float inflatedSize = 20;     // How big the puffer fish gets when inflated

    float timer;                        // Counts down. Puffer fish deflates when this reaches zero
    float initialSize;                  // The default size of the puffer fish; what it is when deflated
    bool inflated;                      // Is the pufferfish deflated?
    ClickBattery bat;                   // Reference to the click battery script on this object

    //Set references
    private void Start()
    {
        initialSize = transform.localScale.x;
        bat = GetComponent<ClickBattery>();
    }

    // If inflated, begin the countdown. When it reaches zero, set inflated to false and return the object to it's
    // normal size. Turn off the power of the battery script to ensure it will glow when the mouse it over it again.  
    private void LateUpdate()
    {
        if (inflated)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                inflated = false;
                transform.localScale = new Vector2(initialSize, initialSize);
                bat.PowerOff();
            }
        }
    }

    //When clicked, inflate the object, setting "inflated" to true and it's local scale to the inflated size. 
    //Set the timer in order to begin the countdown. 
    private void OnMouseDown()
    {
        if (!inflated)
        {
            transform.localScale = new Vector2(inflatedSize, inflatedSize);
            inflated = true;
            timer = Timer;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        BouncePlayer(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        BouncePlayer(other.gameObject);
    }


    // If the object is inflated, find the vector from the player to the pufferfish and use it as a basis for adding force. Note that
    // the force seems have a bigger impact if the player was on top of the object beforehand; this might need adjustment later. 
    void BouncePlayer(GameObject other)
    {
        if (other.gameObject.CompareTag("Player") && inflated)
        {
            Vector3 direction = other.transform.position - transform.position;

            other.GetComponent<Rigidbody2D>().AddForce(direction * 50);
        }
    }
}
