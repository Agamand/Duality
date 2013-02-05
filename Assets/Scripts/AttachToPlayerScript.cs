/** 
* AttachToPlayerScript
*  --> attach a grabbable object to the player allowing him to carry it
*  
* Members: 
*  - private bool m_IsGrabbing: whether or not the gameObject is carrying another gameObject
*  - private ArrayList m_Colliders: the list of colliders colliding with the grabber
*  - private Collider m_Grabbed: the gameObject currently hold
*  - private WorldControllerScript m_WorldController: the world controller defining in which world the player is currently in
*  
* Authors: Jean-Vincent Lamberti
* */


using UnityEngine;
using System.Collections;

public class AttachToPlayerScript : MonoBehaviour
{
    private bool m_IsGrabbing = false;
    //private ArrayList m_Colliders;
    private Collider m_Grabbed;
    private WorldControllerScript m_WorldController;

    // Use this for initialization
    void Start()
    {
        //m_Colliders = new ArrayList();
        m_WorldController = GameObject.Find("GameWorld").GetComponent<WorldControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsGrabbing)
        {
            m_Grabbed.transform.position = gameObject.transform.position;
            m_Grabbed.transform.rotation = gameObject.transform.rotation;

        }
    }

    /**
     * OnTriggerEnter(Collider col)
     *  --> called when a collider enter in collision with the grabber
     *  
     * Arguments: 
     *  - Collider col: the collider which enter in collision with the grabber
     * */
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.GetComponent<AttachableObjectScript>() != null && col.gameObject.transform.GetComponent<LocalGravityScript>() != null
            && col.gameObject.transform.GetComponent<AttachableObjectScript>().m_IsGrabbable)
        { m_Grabbed = col; Debug.Log("Colliding"); }
    }

    /**
     * OnTriggerExit(Collider col)
     *  --> called when a collider is no longer touching the grabber
     *  
     * Arguments: 
     *  - Collider col: the collider which no longer touches the grabber
     * */
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.transform.GetComponent<AttachableObjectScript>() != null && col.gameObject.transform.GetComponent<LocalGravityScript>() != null
            && col.gameObject.transform.GetComponent<AttachableObjectScript>().m_IsGrabbable)
        { m_Grabbed = null; Debug.Log("No More colliding"); }
    }

    /**
     *  Grab()
     *     --> Called when the player presses 'E' and is touching a grabbable object, allow him to carry the object
     * */
    public void Grab()
    {
        if (m_Grabbed)
        {
            m_Grabbed.transform.GetComponent<LocalGravityScript>().setGravityDir(new Vector3(0, 0, 0));
            m_Grabbed.gameObject.transform.parent = gameObject.transform;

            Color c = m_Grabbed.gameObject.renderer.material.color;
            c.a = 0.3f;
            m_Grabbed.gameObject.renderer.material.color = c;

            m_Grabbed.gameObject.GetComponent<AttachableObjectScript>().SetGrabber(gameObject);
            m_IsGrabbing = true;
        }
    }

    /**
     *  Release()
     *      --> Called when the player presses 'E' and is currently holding an object, allow him to release it
     *      --> Called when the carried object enter in collision with a static object
     * */
    public void Release()
    {
        if (m_IsGrabbing == true)
        {
            m_Grabbed.gameObject.transform.parent = m_Grabbed.gameObject.transform.GetComponent<AttachableObjectScript>().GetOriginalTransform();
            m_IsGrabbing = false;

            Color c = m_Grabbed.gameObject.renderer.material.color;
            if (m_Grabbed.gameObject.layer == 8 || m_Grabbed.gameObject.layer == 9)
                c.a = m_WorldController.GetCurrentWorld().layer == m_Grabbed.gameObject.layer ? 1.0f : 0.3f;
            else
                c.a = 1.0f;

            m_Grabbed.gameObject.renderer.material.color = c;
            m_Grabbed.gameObject.GetComponent<AttachableObjectScript>().SetGrabber(null);
            Debug.Log("Original Gravity : " + m_Grabbed.transform.GetComponent<AttachableObjectScript>().GetOriginalGravity());
            m_Grabbed.transform.GetComponent<LocalGravityScript>().setGravityDir(m_Grabbed.transform.GetComponent<AttachableObjectScript>().GetOriginalGravity());

        }
    }

    /*
     * GetGrabbed()
     *  --> Returns the object currently grabbed
     * */
    public GameObject GetGrabbed()
    {
        return m_Grabbed.gameObject;
    }

    /*
     * IsGrabbing()
     *  --> Returns whether or not the player is currently carrying an object
     * */
    public bool IsGrabbing()
    {
        return m_IsGrabbing;
    }
}
