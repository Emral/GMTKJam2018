using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHelper : MonoBehaviour {
    public Texture2D mainTex;
    public Texture2D grabTex;
	// Use this for initialization
	void Start () {
        SetDefault();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = -10;
        RaycastHit2D[] hitResults = new RaycastHit2D[5];
        ContactFilter2D filter = new ContactFilter2D();
        int hitRay = Physics2D.Raycast(worldPos, Vector3.forward, filter, hitResults, 100);
        if (hitRay > 0){
            foreach(RaycastHit2D results in hitResults) {
                if (results.transform == null) continue;
                if (results.transform.gameObject.layer == 9) {
                    SetGrab();
                    if (Input.GetMouseButton(0)){
                        Vector2 position = results.transform.position;
                        position = position - (position - results.point);
                        results.transform.position = new Vector3(position.x, position.y, results.transform.position.z);
                    }
                }
            }
        } else {
            SetDefault();
        }
	}

    public void SetDefault(){
        Cursor.SetCursor(mainTex, new Vector2(9, 1), CursorMode.Auto);
    }

    public void SetGrab()
    {
        Cursor.SetCursor(grabTex, new Vector2(9, 1), CursorMode.Auto);
    }
}
