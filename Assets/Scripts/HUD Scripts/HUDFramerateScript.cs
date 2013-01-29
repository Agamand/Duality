using UnityEngine;
using System.Collections;

public class HUDFramerateScript : MonoBehaviour {

    private TextMesh m_Tm;

	// Use this for initialization
	void Start () {
        m_Tm = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        m_Tm.text = (Time.frameCount/Time.realtimeSinceStartup).ToString("f2")+" FPS";
	}
}
