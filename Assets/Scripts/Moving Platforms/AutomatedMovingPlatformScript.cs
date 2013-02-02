/** 
* AutomatedMovingPlatformScript
*  --> makes the platform move between one point to another, looping
*  
* Members: 
*  - public float m_MinIncrement: the distance you want the platform to go backwards
*  - public float m_MaxIncrement: the distance you want the platform to go forward
*  - public enum axisEnum { x, y, z }: an enum allowing axis to be passed as parameters through Unity Editor 
*  - public axisEnum m_Axis: the axis on which you want the platform to move
*  - public bool m_StartBackwards: defines whether or not the movement starts backwards or forwards
*  - public float m_Speed: the speed of the platform movement
*  - public float m_CooldownStart: the amount of time the platform stays at the closer point of its course
*  - public float m_CooldownEnd: the amount of time the platform stays at the further point of its course
*  - private Vector3 m_InitialPosition: the position of the platform before the automated movement
*  - private Vector3 m_RelPosition: the actual position of the platform relative to its initial position
*  - private Vector3[] m_Translations: the Vector on which you want the translation to happen considering m_Axis
*  - private bool m_IsAtEnd: allows to know wether or not the platform is at one end of its perpetual movement
*
* Authors: Jean-Vincent Lamberti
* */


using UnityEngine;
using System.Collections;


public class AutomatedMovingPlatformScript : MonoBehaviour {

    public float m_MinIncrement;
    public float m_MaxIncrement;
    public enum axisEnum { x, y, z };
    public axisEnum m_Axis;
    public bool m_StartBackwards = false;
    public float m_Speed;
    public float m_CooldownStart = 0;
    public float m_CooldownEnd = 0;
    private Vector3 m_InitialPosition;
    private Vector3 m_RelPosition;
    private Vector3[] m_Translations;
    private bool m_IsAtEnd = false;

    // Use this for initialization
    void Start()
    {
        m_InitialPosition = gameObject.transform.position;
        m_RelPosition.Set(0, 0, 0);
        m_Translations = new Vector3[3];
        m_Translations[0].Set(m_Speed, 0, 0);
        m_Translations[1].Set(0, m_Speed, 0);
        m_Translations[2].Set(0, 0, m_Speed);
    }

    /**
     * CooldownStart()
     *  --> called when the palform reaches its closer end
     *         - waits for the amount of time given in parameter before going back to its perpetual movement
     * */
    IEnumerator CooldownStart()
    {
        yield return new WaitForSeconds(m_CooldownStart);
        m_IsAtEnd = m_StartBackwards;
    }

    /**
     * CooldownStart()
     *  --> called when the palform reaches its further end
     *         - waits for the amount of time given in parameter before going back to its perpetual movement
     * */
    IEnumerator CooldownEnd()
    {
        yield return new WaitForSeconds(m_CooldownEnd);
        m_IsAtEnd = !m_StartBackwards;
    }


    // Update is called once per frame
    void Update()
    {

        if (m_IsAtEnd == !m_StartBackwards)
        {

            if (m_RelPosition[(int)m_Axis] < m_MinIncrement)
            {
                gameObject.transform.Translate(m_Translations[(int)m_Axis] * Time.deltaTime);
                m_RelPosition = gameObject.transform.position + m_InitialPosition;
            }
            else
            {              
                StartCoroutine(CooldownStart());        
            }
        }
        else
        {
            if (m_RelPosition[(int)m_Axis] > m_MaxIncrement * -1)
            {
                gameObject.transform.Translate(-1 * m_Translations[(int)m_Axis] * Time.deltaTime);
                m_RelPosition = gameObject.transform.position + m_InitialPosition;
            }
            else
            {
                StartCoroutine(CooldownEnd());
            }
        }
    }

}
