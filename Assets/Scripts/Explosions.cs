using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawns explosions when they're requested
public class Explosions : MonoBehaviour {
    public static Explosions instance;

    public GameObject explosionPrefab;
    public int poolSize;

    private GameObject[] explosions;
    private AudioSource audioS;

	// Use this for initialization
	void Awake () {
		instance = this;
        //load all explosions into the object pool
        explosions = new GameObject[poolSize];
        for (int i = 0; i < poolSize; ++i)
        {
            explosions[i] = Instantiate(explosionPrefab);
            explosions[i].transform.SetParent(transform);
            explosions[i].SetActive(false);
        }
        audioS = GetComponent<AudioSource>();
    }

    // Call to get your own explosion!
    public void Spawn(Vector3 position)
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (explosions[i].activeSelf)
                continue;

            explosions[i].transform.position = position - Vector3.forward;
            explosions[i].SetActive(true);
            audioS.Play();
            break;
        }
    }
}
