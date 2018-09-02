using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Events for the buttons on the main menu.
public class MainMenu : MonoBehaviour {
    
    public GameObject main;
    public GameObject howto;
    public GameObject credits;
    public GameObject about;
    public GameObject hs;

    public Text[] HSObj;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        List<int> scores = GameManager.instance.GetHS();
        for(int i=0; i < 3; i++){
            HSObj[i].text = scores[i].ToString();
        }
    }

    public void GoToMain(){
        howto.SetActive(false);
        about.SetActive(false);
        credits.SetActive(false);
        hs.SetActive(false);
        main.SetActive(true);
        source.Play();
    }

    public void GotoAbout(){
        main.SetActive(false);
        about.SetActive(true);
        source.Play();
    }

    public void GoToHS()
    {
        main.SetActive(false);
        hs.SetActive(true);
        source.Play();
    }

    public void GoToCredits()
    {
        main.SetActive(false);
        credits.SetActive(true);
        source.Play();
    }

    public void GoToHowTo()
    {
        main.SetActive(false);
        howto.SetActive(true);
        source.Play();
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
        source.Play();
    }
    public void ExitGame()
    {
        source.Play();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
}
