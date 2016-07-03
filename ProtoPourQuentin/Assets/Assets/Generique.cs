using UnityEngine;
using System.Collections;

public class Generique : MonoBehaviour {
    Vector2 positionInitiale;
    public GameObject image;
    float timer;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer>2.0f)
            image.transform.position += new Vector3(0, 1.4f, 0)*Screen.width/1600.0f;
	}
}
