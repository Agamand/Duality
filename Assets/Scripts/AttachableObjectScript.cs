using UnityEngine;
using System.Collections;

public class AttachableObjectScript : MonoBehaviour {

    private Transform originalTransform;
	// Use this for initialization
	void Start () {
        originalTransform = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Transform GetOriginalTransform()
    {
        return originalTransform;
    }
}
