/**
 * JumperScript
 *  --> defines when the player can jump considering the world he is in
 *  
 * Members: 
 * 	- public float m_JumpCooldown : the mandatory amount of time elapsed between two jumps
 *  - private float m_JumpTimer : the amount of time elapsed since the last jump
 *	- private bool m_IsOnGround : defines whether or not the player is touching the ground
 *  - private ArrayList m_ColliderList : the stack of colliders in contact with the gameObject collider
 *  - private int m_JumpCharge : the amount of jump charges the player has
 *  - private int m_MaxJumpCharge : the maximum amount of charges the player can have
 *  
 * Authors: Cyril Basset
 * */
using UnityEngine;
using System.Collections;

public class JumperScript : MonoBehaviour {
	
	public float m_JumpCooldown = 1.0f;
	private float m_JumpTimer = -1.0f;
	private bool m_IsOnGround = true;
    private ArrayList m_ColliderList;
    private int m_JumpCharge = 0;
    private int m_MaxJumpCharge = 0;

	
	void Start () 
	{
        m_ColliderList = new ArrayList();
	}
	
    /**
     * IsOnGround()
     *  --> returns whether or not the player is touching the ground
     * */
	public bool IsOnGround()
	{
		return m_IsOnGround;
	}

    /**
     * SetCurrentCharge(int c)
     *  --> sets the amount of charge the player has
     *  
     * Members: 
     *  - int c : the amount of charges to set
     * */
    public void SetCurrentCharge(int c)
    {
        m_JumpCharge = c;
    }

    /**
     * SetMaxCharge(int max)
     *  --> sets the max amount of charges the player can have
     *  
     * Members:
     *  -  int max : the max amount of charges
     * */
    public void SetMaxCharge(int max)
    {
        m_MaxJumpCharge = max;
        if (!m_IsOnGround)
            m_JumpCharge = 0;
        else m_JumpCharge = max;
    }
	
    /**
     * CanJump() 
     *  --> returns whether or not the player can jump
     * */
	public bool CanJump()
	{
        if (m_JumpTimer < 0.0f && (m_IsOnGround || m_JumpCharge > 0))
			return true;
		
		return false;
	}

    /**
     * OnJump()
     *  --> called when the player jumps, decrease the amount of charges the player has
     *      - decreases the amount of charges left
     *      - initiates the cooldown
     * */
    public void OnJump()
    {
        if(!m_IsOnGround)
            m_JumpCharge--;
        Cooldown();
    }
	
    /**
     * Cooldown()
     *  --> called when the player jumps, makes a second jump impossible 
     *      while the amount of time elapsed since the first jump is lesser than m_JumpCooldown (the mandatory time elapsed between two jumps)
     * */
	public void Cooldown()
	{
		m_JumpTimer = m_JumpCooldown;
	}
	
    /**
     * OnTriggerEnter
     *  --> called when the player collide with another object
     *         - resets the charges if the player is only touching the ground
     *         - adds the collider to the list
     *         - passes the state of the player to OnGround
     *  
     * Arguments: 
     *  - Collider other : the collider of the gameobject colliding with the player
     * */
    void OnTriggerEnter ( Collider other )
    {
        if (m_ColliderList.Count == 0)
        {
            Cooldown();
            m_JumpCharge = m_MaxJumpCharge;
        }
        m_ColliderList.Add(other);
		m_IsOnGround = true;
		
    }
 
    /**
     *  OnTriggerExit(Collider other)
     *   --> called when a GameObject is no longer colliding with the player
     *      -  removes the collider of the GameObject no longer colliding from the list
     *      -  passes the state of isOnGround to false if the player is no longer colliding with anything
     *      
     *  Arguments:
     *      - Collider other: the collider of the GameObject no longer colliding with the player
     * */
    void OnTriggerExit ( Collider  other )
    {
        m_ColliderList.Remove(other);

        if (m_ColliderList.Count == 0)
		    m_IsOnGround = false;

    }

	void Update()
	{
		float dTime = Time.deltaTime;
		if(m_JumpTimer >= dTime)
			m_JumpTimer -= dTime;
		else m_JumpTimer = -1.0f;
		
		Debug.Log("jumpTimer : " + m_JumpTimer + ", _isOnGround : "+ m_IsOnGround + ", canJump() : " + CanJump()+ ", charge : " + m_JumpCharge);
	}
}
