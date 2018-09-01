using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bombs : MonoBehaviour {
    
    public float cooldownMax = 3;    //the cooldown will be set to this when a bomb is dropped
    
    public GameObject bombPrefab;    //prefab that's being initialized

    private GameObject bomb;         //permanent reference to the bomb prefab
    private float cooldown = 0;      //current timer
    public Image cooldownBar;        //UI element


	// Use this for initialization
	void Start () {
		bomb = Instantiate(bombPrefab, transform);
        bomb.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (cooldown <= 0 && !PauseMenu.paused){
            if (Input.GetMouseButtonDown(1)){
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10f;
                bomb.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
                bomb.SetActive(true);
                GameManager.instance.score -= 50;
                bomb.GetComponent<BombScript>().Activate();
                cooldown = cooldownMax;
            }
        } else {
            cooldown = cooldown - Time.deltaTime;
        }
        cooldownBar.fillAmount = (cooldownMax - cooldown) / cooldownMax;

    }
}
