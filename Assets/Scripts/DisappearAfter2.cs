using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisappearAfter2 : MonoBehaviour {
    float timer = 0;
    Text t;
	// Use this for initialization
	void Awake () {
		t = GetComponent<Text>();
	}
	
    public void Activate(string tx){
        t.text = "+" + tx;
        timer = 0;
    }

	// Update is called once per frame
	void Update () {
	    timer = timer + Time.deltaTime;
        if (timer > 2){
            gameObject.SetActive(false);
        }
	}
}
