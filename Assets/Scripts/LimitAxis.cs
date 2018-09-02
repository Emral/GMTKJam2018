using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lets you freeze a gameobject's movement in a certain axis!
public class LimitAxis : MonoBehaviour {
    [Tooltip("Left, Top, Right, Bottom. -1 means no limit.")]
    public Vector4 limits = new Vector4(-1, -1, -1, -1);
    public bool worldSpace = true; //local space not yet supported :(

    private Vector3 startCoords;

	// Use this for initialization
	void Start () {
        startCoords = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 newPos = transform.position;
        float lLim = limits.x == -1 ? newPos.x : startCoords.x - limits.x;
        float rLim = limits.z == -1 ? newPos.x : startCoords.x + limits.z;
        float tLim = limits.y == -1 ? newPos.y : startCoords.y + limits.y;
        float bLim = limits.w == -1 ? newPos.y : startCoords.y - limits.w;

        newPos.y = Mathf.Clamp(newPos.y, bLim, tLim);
        newPos.x = Mathf.Clamp(newPos.x, lLim, rLim);

        transform.position = newPos;
    }
}
