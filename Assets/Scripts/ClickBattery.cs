using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Derivative of the battery, but now it reacts to mouse clicks!
public class ClickBattery : Battery {
    Material mat;    //for highlights
    private float intensity = 0.2f;   //glow intensity
    private float frequency = 3f;     //speed of the sine wave glow
    private float timeSince = 0;      //how long have we hovered?
    private float cooldown = 0;       //current cooldown
    public float cooldownMax = 0;     //maximum cooldown for this object

    private AudioSource source;

    private void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        source = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        cooldown = cooldown - Time.deltaTime;
    }
    private void OnMouseDrag()
    {
        if (type == BatteryType.Continuous && !PauseMenu.paused && cooldown <= 0)
        {
            Activate();
            cooldown = cooldownMax;
        }
    }
    private void OnMouseDown()
    {
        if (!PauseMenu.paused && cooldown <= 0) {
            Activate();
            source.Play();
            cooldown = cooldownMax;
            timeSince = 0;
        }
    }
    private void OnMouseOver()
    {
        if (GetActive() == defaultActive && cooldown <= 0 && !PauseMenu.paused) {
            timeSince = timeSince + Time.deltaTime;
            mat.SetFloat("_HighlightOpacity", Mathf.Sin(-0.5f + timeSince * frequency) * intensity + intensity);
        }
    }
    private void OnMouseExit()
    {
        timeSince = 0;
        mat.SetFloat("_HighlightOpacity", 0);
    }
}
