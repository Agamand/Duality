using UnityEngine;
using System.Collections;

public class DeathPitScript : MonoBehaviour {

    ControlerScript controler;

	// Use this for initialization
	void Start () {
        controler = GameObject.Find("Player").GetComponent<ControlerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        controler.respawnPlayer();
    }
}
