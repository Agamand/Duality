/** 
* AttachToMovingPlatform
*  --> attach every objects having a AttachableObjectScript which collides with the gameObject the script is attached to
*  
* Authors: Jean-Vincent Lamberti
* */


using UnityEngine;
using System.Collections;

public class AttachToMovingScript : MonoBehaviour {


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /**
     * OnTriggerEnter(Collider col)
     *  --> called when a collider enter in collision with the platform collider
     *  
     * Arguments: 
     *  - Collider col: the collider which enter in collision with the platform collider
     * */
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("COLLISION SUR PLATEFORME");
        Debug.Log("col.gameObject.transform.parent: " + col.gameObject.transform.parent.name);
        if (col.gameObject.transform.parent.name.Equals("Player"))
             col.gameObject.transform.parent.transform.parent = gameObject.transform;
        else if (col.gameObject.transform.GetComponent<AttachableObjectScript>() != null)
                col.gameObject.transform.parent = gameObject.transform;

    }

    /**
     * OnTriggerExit(Collider col)
     *  --> called when the collider is no longer touching the platform
     *  
     * Arguments: 
     *  - Collider col: the collider which no longer touches the platform
     * */
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.transform.parent.name.Equals("Player"))
             col.gameObject.transform.parent.transform.parent = col.gameObject.transform.parent.GetComponent<AttachableObjectScript>().GetOriginalTransform();
        else if (col.gameObject.transform.GetComponent<AttachableObjectScript>() != null)
             col.gameObject.transform.parent = col.gameObject.transform.GetComponent<AttachableObjectScript>().GetOriginalTransform();
    }

}
