using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour {
    private Quaternion initRotation; //default rotation
	// Use this for initialization
	void Start () {
		initRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = initRotation;
	}
}
