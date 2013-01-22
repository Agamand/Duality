using UnityEngine;
using System.Collections;

public class HUDColorInvertScript : MonoBehaviour {

    private WorldControlerScript wc;
    private MeshRenderer mr;
    public bool inverted = false;

	// Use this for initialization
	void Start () {
        wc = GameObject.Find("GameWorld").GetComponent<WorldControlerScript>();
        mr = gameObject.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!inverted)
        {
            if (wc.getCurrentWorldNumber() == 0)
                mr.material.color = Color.white;
            else
                mr.material.color = Color.black;
        }
        else
        {
            if (wc.getCurrentWorldNumber() == 1)
                mr.material.color = Color.white;
            else
                mr.material.color = Color.black;
        }
	}
}
