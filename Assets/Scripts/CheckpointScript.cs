using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    ControlerScript cs = null;
    public Quaternion playerRotation;

	// Use this for initialization
	void Start () {
        cs = GameObject.Find("Player").GetComponent<ControlerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
            cs.setRespawnPosition(transform.position);
            cs.setRespawnRotation(playerRotation);
            gameObject.SetActive(false);
    }
}
