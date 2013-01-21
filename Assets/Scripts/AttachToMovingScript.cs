using UnityEngine;
using System.Collections;

public class AttachToMovingScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.transform.parent.GetComponent<AttachableObjectScript>() != null)
        {
            Debug.Log("is attachable");
           if (col.gameObject.transform.parent.name.Equals("Player"))
                col.gameObject.transform.parent.transform.parent = gameObject.transform;
           else
               col.gameObject.transform.parent = gameObject.transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.transform.parent.GetComponent<AttachableObjectScript>() != null)
        {
           if (col.gameObject.transform.parent.name.Equals("Player"))
               col.gameObject.transform.parent.transform.parent = col.gameObject.transform.parent.GetComponent<AttachableObjectScript>().GetOriginalTransform();
           else
               col.gameObject.transform.parent = col.gameObject.transform.parent.GetComponent<AttachableObjectScript>().GetOriginalTransform();
        }
    }

}
