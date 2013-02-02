/**
 *  ControllerScript
 *      --> The script used as a Character Controller which handles : 
 *          - player movement (partial jump handling)
 *          - input management
 *          - partial world change
 *          - camera
 *  
 *  Members: 
 *      public float m_Speed : the speed of the player
 *	    public float m_BackSpeed : the speed of the player while moving backwards
 *      public float m_Jump : the amount of force to give to the jump
 *	    public float m_MouseSpeed : the sensibility of the mouse
 *      public float m_MaxSpeed : the max speed of the player
 *      public float m_Baseforce : the base force factor to each speed and force
 *	    private Vector3 m_Lastpos : the last position of the mouse
 *      private float m_Incl : mouse gradient
 *      private float m_Rot_Y : mouse rotation
 *      private Vector3 m_RespawnPosition : the respawn position of the player
 *      private Quaternion m_RespawnRotation : the respawn rotation of the player
 *      private Vector3 m_InitialVelocity : the velocity of the player when respawning
 *	    private JumperScript m_JumpHandler : the JumperScript attached to the "Jumper" GameObject composing the player prefab
 *	    private WorldControlerScript m_WorldHandler : the WorldControlerScript attached to the scene "GameWorld" on which the player and the world he's in depends
 *      private Quaternion m_Oldquaternion : 
 *      private Quaternion m_Newquaternion : 
 *      private float m_AnimationTimer :
 *      private const float m_AnimationTime :
 *      private bool m_IsInAnimation : 
 *      
 *  Authors: Cyril Basset
 **/

using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour {
	
	
    public float m_Speed = 1.0f;
	public float m_BackSpeed = 0.5f;
    public float m_Jump = 40000.0f;
	public float m_MouseSpeed = 100.0f;
    public float m_MaxSpeed = 30.0f;
    public float m_Baseforce = 40000;
	private Vector3 m_Lastpos;
    private float m_Incl;
    private float m_Rot_Y;
    private Vector3 m_RespawnPosition;
    private Quaternion m_RespawnRotation;
    private Vector3 m_InitialVelocity;

	
	JumperScript m_JumpHandler = null;
	WorldControllerScript m_WorldHandler = null;

    /*Animation for changeGravity*/

    private Quaternion m_Oldquaternion;
    private Quaternion m_Newquaternion;
    private float m_AnimationTimer = 0.0f;
    private const float m_AnimationTime = 1.0f;
    private bool m_IsInAnimation = false;

	
	
	void Start () {
        m_Incl = 0.0f;
        m_Rot_Y = 0.0f;
        m_Lastpos = Input.mousePosition;
		m_JumpHandler = GetComponentInChildren<JumperScript>();
		GameObject world = GameObject.Find("GameWorld");
		m_WorldHandler = world.GetComponent<WorldControllerScript>();
        Screen.showCursor = false;
        m_RespawnPosition = transform.position;
        m_RespawnRotation = transform.rotation;
        m_InitialVelocity.Set(0, 0, 0);
	}
	
    /**
     * ???????
     **/
	private float Modulof(float a, float b)
	{
		return (a-b*Mathf.Floor(a/b));
	}

    /**
     * ??????
     * */
    void OnMouseDown()
    {
        // Lock the cursor
        Screen.lockCursor = true;
    }

    /**
     * ????????
     * */
	private void UpdateMouse(Vector3 mdiff)
	{
		float inclInc = mdiff.y*m_MouseSpeed*Time.deltaTime;
		m_Rot_Y = -mdiff.x*m_MouseSpeed*Time.deltaTime;
		m_Rot_Y = Modulof(m_Rot_Y,360.0f);

        if (m_Incl + inclInc > 89.0f)
		{
            inclInc = 89.0f - m_Incl;
		}
        else if (m_Incl + inclInc < -89.0f)
		{
            inclInc = -89.0f - m_Incl;
		}

        m_Incl += inclInc;
		
		transform.FindChild("Camera").Rotate(Vector3.right,inclInc);
        transform.FindChild("Light").Rotate(Vector3.right, inclInc);		
	}
	
    /**
     * IsOnGround
     *  --> returns whether or not the player is touching the ground
     *  
     * -> true : the player is touching the ground
     * -> false : the player is not touching the ground
     * 
     * */
	private bool IsOnGround()
	{
		if(!m_JumpHandler)
			return true;
		else return m_JumpHandler.IsOnGround();
	}


    /**
     * ???????????
     * */
    public void OnChangeGravity(Vector3 from, Vector3 to)
    {
        from = -from;
        to = -to;
        if (Vector3.Dot(from, to) >= 1.0f)
            return;
        m_AnimationTimer = m_AnimationTime;
        m_Oldquaternion = transform.rotation;
        Debug.Log("rotate from " + from.ToString() + " to " + to.ToString());
        m_Newquaternion = Quaternion.FromToRotation(from, to);

        m_IsInAnimation = true;
    }
	
	void Update () {

        Vector3 pos_diff = m_Lastpos - Input.mousePosition;
		m_Lastpos = Input.mousePosition;
	
		UpdateMouse(pos_diff);
		transform.Rotate(Vector3.up,m_Rot_Y);
		Vector3 vforce = new Vector3(0.0f,0.0f,0.0f);
		float dTime = Time.deltaTime;
		
		float force = m_Speed;
		
        if (Input.GetKey(KeyCode.Z))
            vforce += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
		{
            vforce += Vector3.back;
			force = m_BackSpeed;	
		}
        if (Input.GetKey(KeyCode.D))
            vforce += Vector3.right;

        if (Input.GetKey(KeyCode.Q))
            vforce += Vector3.left;

        if (Input.GetKey(KeyCode.R))
        {
            RespawnPlayer();
        }
		
		if(Input.GetKeyDown(KeyCode.A))
		{
			Debug.Log("a");
			m_WorldHandler.SwitchWorld();
            m_JumpHandler.SetMaxCharge(m_WorldHandler.GetCurrentWorldNumber()); 
		}
		
		vforce = Vector3.Normalize(vforce) * force * m_Baseforce * dTime;
		
		if(!IsOnGround())
			vforce = vforce*0.5f;
		
        if(Input.GetKeyDown(KeyCode.Space) && m_JumpHandler.CanJump())
		{
            vforce += Vector3.up * m_Jump;
            m_JumpHandler.OnJump();
            Debug.Log("JUMPING");
		}
		
		vforce = transform.rotation*vforce;
		rigidbody.AddForce(vforce);
        UpdateAnimation();
		//Debug.Log("velocity : " + rigidbody.velocity.magnitude + ", (" + rigidbody.velocity.ToString() + ")");	
	}


    /**
     *  RespawnPlayer(): 
     *      --> respawns the player at the position, rotation and velocity defined in the data members
     * */
    public void RespawnPlayer()
    {
        transform.position = m_RespawnPosition;
        transform.rotation = m_RespawnRotation;
        transform.rigidbody.velocity = m_InitialVelocity;

    }

    /**
     *  SetRespawnPosition(Vector3 new_Position)
     *      --> sets the respawn position of the player
     *      
     *  Arguments:
     *      - Vector3 new_position : the position to set as the respawn position
     * */
    public void SetRespawnPosition(Vector3 newPosition)
    {
        m_RespawnPosition = newPosition;
    }

    /**
     *  SetRespawnRotation(Vector3 new_Position)
     *      --> sets the respawn rotation of the player
     *      
     *  Arguments:
     *      - Vector3 new_position : the position to set as the respawn rotation
     * */
    public void SetRespawnRotation(Quaternion newRotation)
    {
        m_RespawnRotation = newRotation;
    }

    /**
     * ???????????
     * */
    private void UpdateAnimation()
    {
        if (!m_IsInAnimation)
            return;

        float dTime = Time.deltaTime;

        if (m_AnimationTimer >= dTime)
            m_AnimationTimer -= dTime;
        else
        {
            m_AnimationTimer = 0.0f;
        }

        transform.rotation = Quaternion.Lerp(m_Oldquaternion, m_Newquaternion * m_Oldquaternion, (m_AnimationTime - m_AnimationTimer) / m_AnimationTime);
        if (m_AnimationTimer == 0.0f)
            m_IsInAnimation = false;
    }
}
