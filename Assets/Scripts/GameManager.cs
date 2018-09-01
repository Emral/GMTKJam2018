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

    //public int score;                    // Reference to the score... obviously

    public ScoreClass score = new ScoreClass();


    public Text scoreText;               // Reference to the text that displays the score

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PinballScript>();
        score.Score = 0;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.Score;
    }

    public class ScoreClass
    {
        private int score;

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score += value;

                if(score < 0)
                {
                    score = 0;
                }
            }
        }
    }
}
