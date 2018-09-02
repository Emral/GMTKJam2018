using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We need this one
[RequireComponent(typeof(ClickBattery))]
public class CannonScript : MonoBehaviour {

    public GameObject cannonBall;       // The cannonballs that this will shoot out
    public GameObject[] cannonballs;    // An array of cannonballs.
    public int poolSize = 5;            // How many cannonballs we have available for spawning.
    
    ClickBattery bat;                   // Reference to the cannon's ClickBattery script

    // Set up references and enable firing
    private void Start()
    {
        bat = GetComponent<ClickBattery>();
        //load all cannonballs into the object pool
        cannonballs = new GameObject[poolSize];
        for (int i=0; i < poolSize; ++i){
            cannonballs[i] = Instantiate(cannonBall);
            cannonballs[i].transform.SetParent(transform);
            cannonballs[i].SetActive(false);
        }
    }

    // Using the ClickBattery we can forego keeping our own timer and instead use the cooldown variable from ClickBattery.
    private void Update()
    {
        if (bat.GetActive())
        {
            for (int i=0; i < poolSize; i++){
                if (cannonballs[i].activeSelf) continue;

                cannonballs[i].transform.position = transform.position + Vector3.forward;
                cannonballs[i].transform.rotation = transform.rotation;
                cannonballs[i].SetActive(true);
                break;
            }
            bat.PowerOff();
        }
    }

}
