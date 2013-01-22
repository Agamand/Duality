using UnityEngine;
using System.Collections;

public class HUDFramerateScript : MonoBehaviour {

    private TextMesh tm;

	// Use this for initialization
	void Start () {
        tm = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        tm.text = (Time.frameCount/Time.realtimeSinceStartup).ToString("f2")+" FPS";
	}
}
