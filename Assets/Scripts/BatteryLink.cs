using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows for establishing a link to another GameObject's battery. Useful for switches and triggers.
[RequireComponent(typeof(Battery))]
public class BatteryLink : MonoBehaviour {

    public Battery[] batteries;                //All linked batteries.
    public bool hardOverride = false;          //Whether an active state override should be forced or not. If yes, the link's active state will be applied to all connections. If no, it will simulate switches based on the default active state of the linked batteries.

    private bool[] active;                     //Active state for all linked batteries.
    private Battery self;                      //This is you.

    void Start()
    {
        active = new bool[batteries.Length];
        self = GetComponent<Battery>();
    }

    // Update is called once per frame
    void Update () {
		for (int i = 0; i < batteries.Length; i++)
        {
            if (self.GetActive())
            {
                if (hardOverride)
                {
                    batteries[i].PowerOn();
                } else if (batteries[i].defaultActive)
                {
                    batteries[i].PowerOff();
                } else
                {
                    batteries[i].PowerOn();
                }
            } else
            {
                if (hardOverride)
                {
                    batteries[i].PowerOff();
                }
                else if (batteries[i].defaultActive)
                {
                    batteries[i].PowerOn();
                }
                else
                {
                    batteries[i].PowerOff();
                }
            }

            active[i] = batteries[i].GetActive();
        }
    }
    public bool[] GetActive()
    {
        return active;
    }
    public bool GetActive(int index)
    {
        return active[index];
    }
}
