using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class WorldIndicatorScript : MonoBehaviour {


    private WorldControlerScript wc;
    private TextMesh tm;

	// Use this for initialization
	void Start () {
        wc = GameObject.Find("GameWorld").GetComponent<WorldControlerScript>();
        tm = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        if (wc.getCurrentWorldNumber() == 0)
            tm.text = "World 1";
        else
            tm.text = "World 2";
	}
}
