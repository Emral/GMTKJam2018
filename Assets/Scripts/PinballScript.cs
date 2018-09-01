using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------------------------------------------------------------------
   Author: Lena
   Purpose: To prevent the ball from going beyond its maximum speed in the vertical direction. 
  ------------------------------------------------------------------------------------------------------------------------------------------------*/

public class PinballScript : MonoBehaviour {

    public float maxVelocity = 2f;       // This is the maximum speed the ball can fall

    Rigidbody2D rig;                    // Reference to the rigidbody

    private void Start()
    {
        //Set references
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //If the ball is going to fast upwards, set it to its maximum speed
        if (rig.velocity.y > maxVelocity)
        {
            rig.velocity = new Vector2(rig.velocity.x, maxVelocity);
            //Debug.Log(rig.velocity.y);
        }

        //If the ball is going to fast downwards, set it to its maximum speed
        if (rig.velocity.y < -maxVelocity)
        {
            rig.velocity = new Vector2(rig.velocity.x, -maxVelocity);
            //Debug.Log(rig.velocity.y);
        }        
    }
}
