using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyAnim : MonoBehaviour {
    
    private PointObject po;
    private Animator an;
	// Use this for initialization
	void Start () {
		po = GetComponent<PointObject>();
        an = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		an.SetBool("wobbly", po.getWobble() > 0);
	}
}
