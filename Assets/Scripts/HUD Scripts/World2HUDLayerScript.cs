using UnityEngine;
using System.Collections;

public class World2HUDLayerScript : MonoBehaviour {

    private WorldControlerScript wc;
    private MeshRenderer mr;

	// Use this for initialization
	void Start () {
        wc = GameObject.Find("GameWorld").GetComponent<WorldControlerScript>();
        mr = gameObject.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (wc.getCurrentWorldNumber() == 0)
            mr.enabled = false;
        else
           mr.enabled = true;
	}
}
