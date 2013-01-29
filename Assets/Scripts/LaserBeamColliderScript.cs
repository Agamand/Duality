/**
 * LaserBeamColliderScript
 *  --> kills the player when touching a laserBeam
 *  
 * Members: 
 * 	- private ControlerScript m_PlayerControler : the ControlerScript associated to the player
 *  
 * Authors: Jean-Vincent Lamberti
 * */

using UnityEngine;
using System.Collections;

public class LaserBeamColliderScript : MonoBehaviour {


    private ControlerScript m_PlayerControler;

        // Use this for initialization
	void Start () {
        m_PlayerControler = GameObject.Find("Player").GetComponent<ControlerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    /**
     * OnTriggerEnter(Collider col)
     *  --> called when the player collides with the beam
     *      - respawns the player
     * */
    void OnTriggerEnter(Collider col)
    {
        m_PlayerControler.RespawnPlayer();
    }
}
