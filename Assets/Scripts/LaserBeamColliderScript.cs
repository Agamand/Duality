using UnityEngine;
using System.Collections;

public class LaserBeamColliderScript : MonoBehaviour {


    ControlerScript playerControler;

        // Use this for initialization
	void Start () {
        playerControler = GameObject.Find("Player").GetComponent<ControlerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        playerControler.respawnPlayer();
    }
}
