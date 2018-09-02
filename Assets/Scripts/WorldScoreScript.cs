using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldScoreScript : MonoBehaviour {

    public Text worldScoreText;
    public float showTime = 1f;

    private int lastScore;
    private float timer;

	// Use this for initialization
	void Start ()
    {
        lastScore = GameManager.instance.Score;
        worldScoreText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(GameManager.instance.Score > lastScore)
        {           
            timer = showTime;
            transform.position = GameManager.instance.player.transform.position + new Vector3 (0, 3, 0);
            worldScoreText.text = (GameManager.instance.Score - lastScore).ToString();
            worldScoreText.gameObject.SetActive(true);
            lastScore = GameManager.instance.Score;
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            worldScoreText.gameObject.SetActive(false);
        }
	}
}
