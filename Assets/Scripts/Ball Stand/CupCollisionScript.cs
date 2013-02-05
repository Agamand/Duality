/**
 * CupColiisionScript()
 *  Trigger for switches, flicks its parent switch depending on whether or not a ball has been put into it
 *  
 * Members: 
 *  - private BallStandScript m_Bsc: the parent switch's script
 * 
 * Authors: Jean-Vincent Lamberti
 * */

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

    /**
     * Called when an object is touching the cup of the Ball Stand
     *  --> Flicks the switch on if a ball has entered in collision with the cup
     * */
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ball"))
            m_Bsc.FlickOn();
    }

    /**
     * Called when an object is no longer touching the cup of the Ball Stand
     *  --> Flicks the switch off if a ball is no longer in collision with the cup
     * */
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ball"))
            m_Bsc.FlickOff();
    }

}
