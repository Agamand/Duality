/**
 * LaserBeamScript
 *  --> Global Script : activates and deactivates all the lasers tagged with "laserBeam" in the level depdending on the world the player is in
 *  
 * Members: 
 * 	- private GameObject []m_LaserBeams : an array containing all the GameObjects tagged with "laserBeam"
 *  - private GameObject m_CurrentWorld : the world the player is in;
 *  - private WorldControlerScript m_worldController : the WorldControlerScript attached to the scene "GameWorld" defining in which world the player is
 *  
 * Authors: Jean-Vincent Lamberti
 * */


using UnityEngine;
using System.Collections;

public class LaserBeamScript : MonoBehaviour {

    private GameObject []m_LaserBeams;
    private GameObject m_CurrentWorld;
    private WorldControlerScript m_worldController;

	// Use this for initialization
	void Start () {
        m_LaserBeams = GameObject.FindGameObjectsWithTag("laserBeam");
        m_worldController = GameObject.Find("GameWorld").GetComponent<WorldControlerScript>();
        m_CurrentWorld = m_worldController.getCurrentWorld();
	}
	
	// Update is called once per frame
	void Update () {
            m_CurrentWorld = m_worldController.getCurrentWorld();
            for (int i = 0; i < m_LaserBeams.Length; i++)
            {
                if (m_CurrentWorld.layer == m_LaserBeams[i].layer)
                {
                    m_LaserBeams[i].SetActive(true);

                }
                if (m_CurrentWorld.layer != m_LaserBeams[i].layer)
                {
                    m_LaserBeams[i].SetActive(false);
                }

            }          
	}
}
