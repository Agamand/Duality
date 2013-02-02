/**
 *  DeathPitScript
 *      --> Script respawning the player when colliding with the object
 *      
 *      Members:
 *          - private ControlerScript m_Controler : the world controler used to respawn the player
 *  
 *  Authors: Jean-Vincent Lamberti
 **/

using UnityEngine;
using System.Collections;

public class DeathPitScript : MonoBehaviour {

    private ControllerScript m_Controler;

	// Use this for initialization
	void Start () {
        m_Controler = GameObject.Find("Player").GetComponent<ControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /**
     *  OnTriggerEnter(Collider col)
     *      --> respanws the player when colliding with the collider of the gameObject the script is attached to
     *      
     * Members: 
     *  - Collider col: the collider of the gameObject the script is attached to
     * */
    void OnTriggerEnter(Collider col)
    {
        m_Controler.RespawnPlayer();
    }
}
