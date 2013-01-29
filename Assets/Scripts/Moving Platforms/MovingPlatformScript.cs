/** 
* MovingPlatformScript
*  --> makes the platform move between one point to another depending on the world the player choose
*       - changes the color of the bumpers (red for the world 2, blue for the world 1)
*  
* Members: 
*  - public float m_MinIncrement: the distance you want the platform to go backwards
*  - public float m_MaxIncrement: the distance you want the platform to go forward
*  - public enum axisEnum { x, y, z }: an enum allowing axis to be passed as parameters through Unity Editor 
*  - public axisEnum m_Axis: the axis on which you want the platform to move
*  - public float m_Speed: the speed of the platform movement
*  - public bool m_StartBackwards: defines whether or not the movement starts backwards or forwards
*  - private Vector3 m_InitialPosition: the position of the platform before the automated movement
*  - private WorldControlerScript m_WorldControler: the WorldControlerScript attached to the scene "GameWorld" defining in which world the player is
*  - private int m_CurrentWorldNumber: the index of the World the player is in, 0 if World 1, 1 if World 2
*  - private Vector3 m_RelPosition: the actual position of the platform relative to its initial position
*  - private Vector3[] m_Translations: the Vector on which you want the translation to happen considering m_Axis
*  - private Renderer[] m_BumpersRenderer: the renderers of the bumpers attached to the platform
*
* Authors: Jean-Vincent Lamberti
* */

using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour {

    public float m_MinIncrement;
    public float m_MaxIncrement;
    public enum axisEnum { x, y, z };
    public axisEnum m_Axis;
    public float m_Speed;
    public bool m_StartBackwards = false;
    private Vector3 m_InitialPosition;
    private WorldControlerScript m_WorldControler;
    private int m_CurrentWorldNumber;
    private Vector3 m_RelPosition;
    private Vector3[] m_Translations;
    private Renderer[] m_BumpersRenderer;

	// Use this for initialization
	void Start () {
        m_InitialPosition = gameObject.transform.position;
        m_WorldControler = GameObject.Find("GameWorld").GetComponent<WorldControlerScript>();
        m_CurrentWorldNumber = m_WorldControler.getCurrentWorldNumber();
        m_RelPosition.Set(0, 0, 0);
        m_Translations = new Vector3[3];
        m_Translations[0].Set(m_Speed, 0, 0);
        m_Translations[1].Set(0, m_Speed, 0);
        m_Translations[2].Set(0, 0, m_Speed);
        m_BumpersRenderer = new Renderer[2];
        m_BumpersRenderer[0] = gameObject.transform.FindChild("bumperLeft").GetComponent<Renderer>();
        m_BumpersRenderer[1] = gameObject.transform.FindChild("bumperRight").GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        m_CurrentWorldNumber = m_WorldControler.getCurrentWorldNumber();

        if (m_CurrentWorldNumber == (m_StartBackwards == true ? 0 : 1))
        {
            foreach (Renderer r in m_BumpersRenderer){
                r.material.color = Color.red;
            }
            if (m_RelPosition[(int)m_Axis] < m_MinIncrement) {
                gameObject.transform.Translate(m_Translations[(int)m_Axis] * Time.deltaTime);                
                m_RelPosition = gameObject.transform.position + m_InitialPosition;
            }
        }
        else {
            foreach (Renderer r in m_BumpersRenderer)
            {
                r.material.color = Color.blue;
            }
            if (m_RelPosition[(int)m_Axis] > m_MaxIncrement * -1)
            {
                gameObject.transform.Translate(-1 * m_Translations[(int)m_Axis] * Time.deltaTime);
                m_RelPosition = gameObject.transform.position + m_InitialPosition;
            }
        }
	}
}
