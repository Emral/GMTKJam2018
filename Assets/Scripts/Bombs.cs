using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bombs : MonoBehaviour {
    
    public float cooldownMax = 3;
    
    public GameObject bombPrefab;

    private GameObject bomb;
    private float cooldown = 0;
    public Image cooldownBar;


	// Use this for initialization
	void Start () {
		bomb = Instantiate(bombPrefab, transform);
        bomb.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (cooldown <= 0){
            if (Input.GetMouseButtonDown(1)){
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10f;
                bomb.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
                bomb.SetActive(true);
                bomb.GetComponent<BombScript>().Activate();
                cooldown = cooldownMax;
            }
        } else {
            cooldown = cooldown - Time.deltaTime;
        }
        cooldownBar.fillAmount = (cooldownMax - cooldown) / cooldownMax;

    }
}
