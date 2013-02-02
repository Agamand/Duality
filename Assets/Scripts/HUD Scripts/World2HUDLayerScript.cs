using UnityEngine;
using System.Collections;

public class World2HUDLayerScript : MonoBehaviour {

    private WorldControllerScript wc;
    private MeshRenderer mr;

	// Use this for initialization
	void Start () {
        wc = GameObject.Find("GameWorld").GetComponent<WorldControllerScript>();
        mr = gameObject.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (wc.GetCurrentWorldNumber() == 0)
            mr.enabled = false;
        else
           mr.enabled = true;
	}
}
