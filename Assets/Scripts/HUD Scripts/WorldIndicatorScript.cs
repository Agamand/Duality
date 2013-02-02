/**
 * WorldIndicatorScript
 *  --> print on the HUD the current world
 *  
 * Members: 
 *  - private WorldControlerScript m_Wc: the WorldControler of the scene "GameWorld" defining in which world is the player
 *  - private TextMesh m_Tm: the TextMesh indicating which world the player is in
 *  
 * Authors: Jean-Vincent Lamberti
 * */
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class WorldIndicatorScript : MonoBehaviour {


    private WorldControllerScript m_Wc;
    private TextMesh m_Tm;

	// Use this for initialization
	void Start () {
        m_Wc = GameObject.Find("GameWorld").GetComponent<WorldControllerScript>();
        m_Tm = gameObject.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_Wc.GetCurrentWorldNumber() == 0)
            m_Tm.text = "World 1";
        else
            m_Tm.text = "World 2";
	}
}
