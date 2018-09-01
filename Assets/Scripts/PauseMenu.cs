using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseOverlay; // Overlay while the game is paused.
    public static bool paused = false;

    void Start()
    {
        pauseOverlay.SetActive(paused);
    }

	public void Toggle() {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        pauseOverlay.SetActive(paused);
    }
    public void Exit(){
        SceneManager.LoadScene(0);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        paused = false;
        Time.timeScale = 1;
        pauseOverlay.SetActive(paused);
    }

    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
