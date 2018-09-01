using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Useful references
public class GameManager : MonoBehaviour {
    public static GameManager instance;  //i stopped being lazy
    public GameObject UIElements;
    [HideInInspector]
    public PinballScript player;         //always useful to have one of these

    [HideInInspector]
    public Bombs bombInput;

    private int _score;

    [HideInInspector]
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

    public int StartingLives = 3;        // How many lives the player starts with
    public Image gameOverMenu;           // Reference to the menu that appears when the game has ended. 
    [HideInInspector]
    public int lives;                    // How many lives the player has at the moment

    private void Awake()
    {
        if (instance != null){
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        bombInput = GetComponentInChildren<Bombs>();
    }

    private void Update()
    {
        scoreText.text = "Score: " + Score;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UIElements.SetActive(scene.buildIndex != 0);
        player = FindObjectOfType<PinballScript>();
        Score = 0;
        bombInput.Reset();
    }

    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Reset()
    {
        lives = StartingLives;
        SceneManager.LoadScene(0);
    }
}
