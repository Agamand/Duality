/**
 *  HUDColorInvertScript
 *      --> Script Inverting the Color of the HUD depending on the world the player is in
 *     
 *      Members:
 *          - private WorldControlerScript m_Wc : the WorldControler used to define the world the player is in
 *          - private MeshRenderer m_Mr : the MeshRenderer of the gameObject on which you change the color
 *          - public bool m_Inverted : a boolean defining if the color is inverted or not : 
 *                     false -> white in world 1, black in world 2
 *                     true -> black in world 1, white in wordl 2
 *
 *  Authors: Jean-Vincent Lamberti
 **/

using UnityEngine;
using System.Collections;

public class HUDColorInvertScript : MonoBehaviour {

    private WorldControllerScript m_Wc;
    private MeshRenderer m_Mr;
    public bool m_Inverted = false;

	// Use this for initialization
	void Start () {
        m_Wc = GameObject.Find("GameWorld").GetComponent<WorldControllerScript>();
        m_Mr = gameObject.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!m_Inverted)
        {
            if (m_Wc.GetCurrentWorldNumber() == 0)
                m_Mr.material.color = Color.white;
            else
                m_Mr.material.color = Color.black;
        }
        else
        {
            if (m_Wc.GetCurrentWorldNumber() == 1)
                m_Mr.material.color = Color.white;
            else
                m_Mr.material.color = Color.black;
        }
	}
}
