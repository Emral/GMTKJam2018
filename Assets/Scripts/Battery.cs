using UnityEngine;
using System.Collections;

//Handles "active" state of an object. Useful for buttons and the such.
public class Battery : MonoBehaviour
{
    public enum BatteryType
    {
        Toggleable,
        Continuous
    }

    private bool active;                              //Active state.

    public bool defaultActive = false;                //Default state of the camera.
    public BatteryType type = BatteryType.Continuous; //Whether the battery's state resets to default every frame. Default: yes
    

    private void Start()
    {
        active = defaultActive;
    }

    private void LateUpdate()
    {
        if (type == BatteryType.Continuous)
        {
            active = defaultActive;
        }
    }

    public void PowerOn()
    {
        active = true;
    }

    public void TogglePower()
    {
        active = !active;
    }

    public void PowerOff()
    {
        active = false;
    }

    public bool GetActive()
    {
        return active;
    }
}
