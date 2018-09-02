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
    public GameObject livesImagePrefab;  // UI Image for lives.
    public GameObject livesContainer;    // UI Container for lives.
    public Image gameOverMenu;           // Reference to the menu that appears when the game has ended. 

    private GameObject[] livesImages;    // References to those UI elements.
    private int _lives;                  // How many lives the player has at the moment

    [HideInInspector]
    public int lives {
        get {return _lives;}
        set {
            _lives = value;
            for (int i=0; i < StartingLives; ++i){
                livesImages[i].SetActive(i < _lives);
            }
        }
    }

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
        if (livesImages != null){
            for (int i = 0; i < StartingLives; i++)
            {
                Destroy(livesImages[i]);
            }
        }
        livesImages = new GameObject[StartingLives];
        for (int i=0; i < StartingLives; i++){
            livesImages[i] = Instantiate(livesImagePrefab, livesContainer.transform);
        }
        lives = StartingLives;
        bombInput.Reset();
    }

    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void GameOver(){

        // If the game over scene is not active, make it so. Else, deactivate it and load the main menu

        if(gameOverMenu.isActiveAndEnabled == false)
        {
            gameOverMenu.gameObject.SetActive(true);
            PauseMenu.paused = true;
            Time.timeScale = 0;
        }
        else
        {
            PauseMenu.paused = false;
            Time.timeScale = 1;
            gameOverMenu.gameObject.SetActive(false);
            SceneManager.LoadScene(0);
        }
    }
}
