/** 
* LocalGravityScript
*  --> change the gravity of ????
*  
* Members: 
* - static float m_gravityAcceleration: ???????????
* - public Vector3 m_StartDir: ?????????
* - private Vector3 m_Gravity: ?????????
* - private ConstantForce m_ConstForce: ?????????
* - private GameObject m_GameObject: ???????
* - private ControlerScript m_Player: ???????
* - private Rigidbody m_Body: ????????
*  
* Authors: Cyril Basset
* */

using UnityEngine;
using System.Collections;

public class LocalGravityScript : MonoBehaviour {

    static float m_gravityAcceleration = 9.8f;
    public Vector3 m_StartDir = new Vector3(0f, -1.0f, 0f);

    private Vector3 m_Gravity;
    private ConstantForce m_ConstForce = null;
    private GameObject m_GameObject = null;
    private ControllerScript m_Player = null;
    private Rigidbody m_Body = null;

    void Start()
    {
        m_GameObject = this.gameObject;
        m_Body = this.rigidbody;

        if (!m_Body)
        {
            Debug.Log("Warning : LocalGravity is attach to a gameobject without rigidbody !");
            return;
        }


        m_Body.useGravity = false;

        m_ConstForce = m_GameObject.AddComponent<ConstantForce>();
        setGravityDir(m_StartDir);
        m_Player = GetComponent<ControllerScript>();
    }

    /**
     * ???????????????????????
     * */
    public void setGravityDir(Vector3 newGravity)
    {
        float mass = m_Body ? m_Body.mass : 1.0f;

        Vector3 old = Vector3.Normalize(m_Gravity);
        Debug.Log("newGravity = " + newGravity.ToString());
        m_Gravity = newGravity;
        Debug.Log("Gravity = " + m_Gravity.ToString());
        m_Gravity *= m_gravityAcceleration;
        Debug.Log("Gravity = " + m_Gravity.ToString());
        m_ConstForce.force = m_Gravity * mass;

        Debug.Log("Change Gravity Dir, from " + old.ToString() + " to " + newGravity.ToString());
        if (m_Player)
            m_Player.OnChangeGravity(old, newGravity);

    }

    void Update() { }
}
