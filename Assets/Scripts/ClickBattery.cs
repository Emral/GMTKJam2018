using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Derivative of the battery, but now it reacts to mouse clicks!
public class ClickBattery : Battery {

    private void OnMouseDrag()
    {
        if (type == BatteryType.Continuous)
        {
            Activate();
        }
    }
    private void OnMouseDown()
    {
        Activate();
    }
}
