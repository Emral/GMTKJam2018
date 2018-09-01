using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Useful references
public class GameManager : MonoBehaviour {
    public static GameManager instance;  //not a singleton because i'm lazy
    [HideInInspector]
    public PinballScript player;         //always useful to have one of these
    [HideInInspector]

    private int _score;

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;

            if (_score < 0)
            {
                _score = 0;
            }
        }
    }

    public Text scoreText;               // Reference to the text that displays the score

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PinballScript>();
        Score = 0;
    }

    private void Update()
    {
        scoreText.text = "Score: " + Score;
    }
}
