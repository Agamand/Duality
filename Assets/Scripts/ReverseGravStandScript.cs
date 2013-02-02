/**
 * ReverseGravStandScript
 *  --> Change the gravity of the gameObject whose collider is touching the collider of the GameObject the script
 *      is attached to, if the gameObject can have his gravity changed
 * 
 *  Members: 
 *      - public Vector3 m_Gravity: the Vector3 representing the gravity of the gameObject
 *      
 * Authors: Cyril Basset
 * */

using UnityEngine;
using System.Collections;

public class ReverseGravStandScript : MonoBehaviour {

    public Vector3 m_Gravity = new Vector3();

    void OnTriggerEnter(Collider other)
    {
        LocalGravityScript gravityScript = other.GetComponent<LocalGravityScript>();
        if (gravityScript == null)
            return;

        gravityScript.setGravityDir(m_Gravity);
    }
}
