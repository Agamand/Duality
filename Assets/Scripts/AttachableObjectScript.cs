/**
 *  AttachableObjectScript
 *      --> Script used to define if an object is attachable to another : 
 *          - keep the initial parent of the object in order to restore it while unattached
 *          
 *   Public Members: 
 *      - private Transform m_OriginalTransform : the parent of the gameObject before any dynamic attachment
 *          
 *  Authors: Jean-Vincent Lamberti
 **/

using UnityEngine;
using System.Collections;

public class AttachableObjectScript : MonoBehaviour {

    private Transform m_OriginalTransform;
	// Use this for initialization
	void Start () {
        m_OriginalTransform = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /**
     * GetOriginalTransform()
     *  --> returns the initial parent of the gameObject
     **/
    public Transform GetOriginalTransform()
    {
        return m_OriginalTransform;
    }
}
