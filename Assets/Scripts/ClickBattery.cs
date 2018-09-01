using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Derivative of the battery, but now it reacts to mouse clicks!
public class ClickBattery : Battery {
    Material mat;    //for highlights
    private float intensity = 0.2f;
    private float amplitude = 3f;
    private float timeSince = 0;
    private float cooldown = 0;
    public float cooldownMax = 0;

    private void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    private void LateUpdate()
    {
        cooldown = cooldown - Time.deltaTime;
    }
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
        cooldown = cooldownMax;
    }
    private void OnMouseOver()
    {
        if (GetActive() == defaultActive && cooldown <= 0) {
            timeSince = timeSince + Time.deltaTime;
            mat.SetFloat("_HighlightOpacity", Mathf.Sin(-0.5f + timeSince * amplitude) * intensity + intensity);
        }
    }
    private void OnMouseExit()
    {
        timeSince = 0;
        mat.SetFloat("_HighlightOpacity", 0);
    }
}
