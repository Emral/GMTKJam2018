using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    public GameObject cannonBall;       // The cannonballs that this will shoot out
    public GameObject spawnPoint;       // An empty game object that holds the spawn point of the cannonballs
    public float fireRate;              // How many seconds until the player can shoot off another cannonball?

    float timer;                        // Can fire again once this reaces zero
    bool canFire;                       // Can the player fire the cannon ball?
    ClickBattery bat;                   // Reference to the cannon's ClickBattery script

    // Set up references and enable firing
    private void Start()
    {
        bat = GetComponent<ClickBattery>();
        canFire = true;
    }

    // If the player can't fire, a timer counts down. When it reaches zero, the cannon will power down and the player
    // will be able to fire it again. 
    private void Update()
    {
        if (!canFire)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                canFire = true;
                bat.PowerOff();
            }
        }
    }

    // When the player clicks on the cannon, fire a cannonball, if enabled. Then, disable firing and set the timer according
    // to the fire rate. 
    private void OnMouseDown()
    {
        if (canFire)
        {
            Instantiate(cannonBall, spawnPoint.transform.position, spawnPoint.transform.rotation);
            canFire = false;
            timer = fireRate;
        }
    }

}
