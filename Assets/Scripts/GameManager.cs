using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Useful references
public class GameManager : MonoBehaviour {
    public static GameManager instance;  //not a singleton because i'm lazy
    [HideInInspector]
    public PinballScript player;         //always useful to have one of these

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PinballScript>();
    }
}
