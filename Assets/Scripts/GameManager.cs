using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Useful references
public class GameManager : MonoBehaviour {
    public static GameManager instance;
    [HideInInspector]
    public PinballScript player;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PinballScript>();
    }
}
