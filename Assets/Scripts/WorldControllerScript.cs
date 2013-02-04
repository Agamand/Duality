/**
 * WorldControllerScript
 *  --> defines the world the player is currently in and make the world switch possible
 *  
 * Members: 
 *  - private int m_CurrentWorld : represents the world the player is currently in
 *      0 if World1
 *      1 if World2
 *  - private GameObject m_Wordl1 : the World1 GameObject
 *  - private GameObject m_World2 : the World2 GameObject
 * 
 * Authors : Cyril Basset
 * */

using UnityEngine;
using System.Collections;

public class WorldControllerScript : MonoBehaviour
{


    private int m_CurrentWorld = 0;
    private GameObject m_World1;
    private GameObject m_World2;

    void Start()
    {
        m_World1 = GameObject.Find("World1");
        m_World2 = GameObject.Find("World2");
        Physics.IgnoreLayerCollision(8, 9);
        SetWorld(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * SwitchWorld()
     *  --> switch the world the player is in
     * */
    public void SwitchWorld()
    {
        SetWorld(m_CurrentWorld == 0 ? 1 : 0);
    }

    /**
     * SetWorld(int world)
     *  --> change the world the player is currently in to the world given in parameter
     *  
     * Arguments:
     *  - int world : the index of the world you want the player to switch to
     * */
    public void SetWorld(int world)
    {
        m_CurrentWorld = world;

        Physics.IgnoreLayerCollision(10, 8, m_CurrentWorld != 0);
        Physics.IgnoreLayerCollision(10, 9, m_CurrentWorld == 0);
        Debug.Log("current_world : " + m_CurrentWorld);
        int childCount = m_World1.transform.GetChildCount();
        for (int i = 0; i < childCount; i++)
        {
            Transform go = m_World1.transform.GetChild(i);
            if (go.GetComponent<Renderer>() != null)
            {
                Color c = go.renderer.material.color;
                c.a = m_CurrentWorld == 0 ? 1.0f : 0.3f;
                go.renderer.material.color = c;
            }
        }

        childCount = m_World2.transform.GetChildCount();
        for (int i = 0; i < childCount; i++)
        {
            Transform go = m_World2.transform.GetChild(i);
            if (go.GetComponent<Renderer>() != null)
            {
                Color c = go.renderer.material.color;
                c.a = m_CurrentWorld != 0 ? 1.0f : 0.3f;
                go.renderer.material.color = c;
            }
        }

    }

    /**
     * GetCurrentWorld()
     *  --> return the world (GameObject) the player is currently in
     * */
    public GameObject GetCurrentWorld()
    {
        return m_CurrentWorld == 0 ? m_World1 : m_World2;
    }

    /**
     * GetCurrentWorldNumber()
     *  --> return the index of the world the player is currently in
     * */
    public int GetCurrentWorldNumber()
    {
        return m_CurrentWorld == 0 ? 0 : 1;
    }
}
