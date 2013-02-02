/**
 *  CheckpointScript
 *      --> Script used to define a checkpoint 
 *          - sets the player respawn position to its center when entering its collision box
 *         
 *  Members: 
 *      - private ControlerScript m_Cs : the ControlerScript attached to the player
 *      - public Quaternion playerRotation : the rotation to give to the player when respawning     
 * 
 *  Authors: Jean-Vincent Lamberti
 **/

using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    private ControllerScript m_Cs = null;
    public Quaternion playerRotation;

	// Use this for initialization
	void Start () {
        m_Cs = GameObject.Find("Player").GetComponent<ControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /**
     *  OnTriggerEnter(Collider col)
     *      -> set the respawn postition and rotation when the player collide with the collider the script is attached to
     *      
     *  Arguments: 
     *      - Collider col: the collider of the gameObject the script is attached to
     **/
    void OnTriggerEnter(Collider col)
    {
            m_Cs.SetRespawnPosition(transform.position);
            m_Cs.SetRespawnRotation(playerRotation);
            gameObject.SetActive(false);
    }
}
