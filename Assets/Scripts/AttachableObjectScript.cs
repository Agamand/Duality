/**
 *  AttachableObjectScript
 *      --> Script used to define if an object is attachable to another : 
 *          - keep the initial parent of the object in order to restore it while unattached
 *          
 *   Public Members: 
 *      - private Transform m_OriginalTransform : the parent of the gameObject before any dynamic attachment
 *      - private Vector3 m_OriginalGravity: the inital Gravity before the object is being grabbed of attached;
 *      - private GameObject m_Grabber: the GameObject carrying the gameObject if the gameObject is grabbable
 *      - public bool m_IsGrabbable: whether or not the gameObject can be carried by another
 *          
 *  Authors: Jean-Vincent Lamberti
 **/

using UnityEngine;
using System.Collections;

public class AttachableObjectScript : MonoBehaviour {

    private Transform m_OriginalTransform;
    private Vector3 m_OriginalGravity;
    private GameObject m_Grabber;
    public bool m_IsGrabbable = false;
	
    // Use this for initialization
	void Start () {
        m_OriginalTransform = gameObject.transform.parent;
        if (gameObject.transform.GetComponent<LocalGravityScript>())
            m_OriginalGravity = gameObject.transform.GetComponent<LocalGravityScript>().GetStartDir();
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

    /**
     * GetOriginalGravity()
     *  --> returns the original gravity of the gameObject
     * */
    public Vector3 GetOriginalGravity()
    {
        return m_OriginalGravity;
    }

    /**
     * SetGrabber(GameObject grabber)
     *  --> Defines the grabber actually carrying the object
     *  
     * Arguments: 
     *  - GameObject grabber: the GameObject the grabbable object is attached to
     * */
    public void SetGrabber(GameObject grabber)
    {
        m_Grabber = grabber;
    }

    void OnCollisionEnter(Collision col)
    {
        if (m_Grabber != null)
        {

            if (!col.gameObject.CompareTag("Dynamic"))
            {
                m_Grabber.GetComponent<AttachToPlayerScript>().Release();
            }
        }
    }
}
