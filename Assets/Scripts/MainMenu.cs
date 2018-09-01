using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Events for the buttons on the main menu.
public class MainMenu : MonoBehaviour {
    
    public GameObject main;
    public GameObject howto;
    public GameObject credits;
    public GameObject about;


    public void GoToMain(){
        howto.SetActive(false);
        about.SetActive(false);
        credits.SetActive(false);
        main.SetActive(true);
    }

    public void GotoAbout(){
        main.SetActive(false);
        about.SetActive(true);
    }

    public void GoToCredits()
    {
        main.SetActive(false);
        credits.SetActive(true);
    }

    public void GoToHowTo()
    {
        main.SetActive(false);
        howto.SetActive(true);
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
}
