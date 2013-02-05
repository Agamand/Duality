using UnityEngine;
using System.Collections;

public class CupCollisionScript : MonoBehaviour {

    private BallStandScript m_Bsc;
	// Use this for initialization
	void Start () {
        m_Bsc = gameObject.transform.parent.GetComponent<BallStandScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ball"))
            m_Bsc.FlickOn();
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ball"))
            m_Bsc.FlickOff();
    }

}
