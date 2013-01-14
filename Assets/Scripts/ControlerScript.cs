using UnityEngine;
using System.Collections;

public class ControlerScript : MonoBehaviour {
	
	
    public float speed = 1.0f;
	public float backSpeed = 0.5f;
    public float jump = 1.0f;
	public float mouseSpeed = 1.0f;
    public float maxSpeed = 30.0f;
    public float baseforce = 1500;
	private Vector3 lastpos;
    private float incl;
    private float rot_Y;
    private Vector3 respawnPosition;

	
	JumperScript jumpHandler = null;
	WorldControlerScript worldHandler = null;

    /*Animation for changeGravity*/

    private Quaternion oldquaternion;
    private Quaternion newquaternion;
    private float animationTimer = 0.0f;
    private const float animationTime = 1.0f;
    private bool isInAnimation = false;

	
	
	void Start () {
        incl = 0.0f;
        rot_Y = 0.0f;
        lastpos = Input.mousePosition;
		jumpHandler = GetComponentInChildren<JumperScript>();
		GameObject world = GameObject.Find("GameWorld");
		worldHandler = world.GetComponent<WorldControlerScript>();
        Screen.showCursor = false;
        respawnPosition = transform.position;
	}
	
	private float modulof(float a, float b)
	{
		return (a-b*Mathf.Floor(a/b));
	}

    void OnMouseDown()
    {
        // Lock the cursor
        Screen.lockCursor = true;
    }

	private void updateMouse(Vector3 mdiff)
	{
		float inclInc = mdiff.y*mouseSpeed*Time.deltaTime;
		rot_Y = -mdiff.x*mouseSpeed*Time.deltaTime;
		rot_Y = modulof(rot_Y,360.0f);
		
		if(incl+inclInc > 89.0f)
		{
			inclInc = 89.0f - incl;
		}
		else if(incl+inclInc < -89.0f)
		{
			inclInc = -89.0f - incl;
		}
		
		incl += inclInc;
		
		transform.FindChild("Camera").Rotate(Vector3.right,inclInc);
        transform.FindChild("Light").Rotate(Vector3.right, inclInc);		
	}
	
	private bool isOnGround()
	{
		if(!jumpHandler)
			return true;
		else return jumpHandler.isOnGround();
	}

    public void changeGravity(Vector3 from, Vector3 to)
    {
        if(Vector3.Dot(from,to) >=1.0f)
            return; //Same direction => no rotate;
        animationTimer = animationTime;
        oldquaternion = transform.rotation;
        Debug.Log("rotate from " + from.ToString() + " to " + to.ToString());
        newquaternion = Quaternion.Inverse(oldquaternion)*Quaternion.FromToRotation(from,to);
        isInAnimation = true;
    }
	
	void Update () {

        Vector3 pos_diff = lastpos - Input.mousePosition;
		lastpos = Input.mousePosition;
	
		updateMouse(pos_diff);
		transform.Rotate(Vector3.up,rot_Y);
		Vector3 vforce = new Vector3(0.0f,0.0f,0.0f);
		float dTime = Time.deltaTime;
		
		float force = speed;
		
        if (Input.GetKey(KeyCode.Z))
            vforce += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
		{
            vforce += Vector3.back;
			force = backSpeed;	
		}
        if (Input.GetKey(KeyCode.D))
            vforce += Vector3.right;

        if (Input.GetKey(KeyCode.Q))
            vforce += Vector3.left;

        if (Input.GetKey(KeyCode.R))
        {
            respawnPlayer();
        }
		
		if(Input.GetKeyDown(KeyCode.A))
		{
			Debug.Log("a");
			worldHandler.switchWorld();
		}
		
		vforce = Vector3.Normalize(vforce) * force * baseforce * dTime;
		
		if(!isOnGround())
			vforce = vforce*0.5f;
		
        if(Input.GetKeyDown(KeyCode.Space) && jumpHandler.canJump())
		{
            vforce += Vector3.up * jump;
            jumpHandler.onJump();
		}
		
		vforce = transform.rotation*vforce;
		rigidbody.AddForce(vforce);
        updateAnimation();
		//Debug.Log("velocity : " + rigidbody.velocity.magnitude + ", (" + rigidbody.velocity.ToString() + ")");	
	}

    public void respawnPlayer()
    {
        transform.position = respawnPosition;
    }

    public void setRespawnPosition(Vector3 new_position)
    {
        respawnPosition = new_position;
    }

    private void updateAnimation()
    {
        if (!isInAnimation)
            return;

        float dTime = Time.deltaTime;

        if (animationTimer >= dTime)
            animationTimer -= dTime;
        else
        {
            animationTimer = 0.0f;
        }

        transform.rotation = Quaternion.Lerp(Quaternion.identity, newquaternion, (animationTime - animationTimer) / animationTime)*oldquaternion;
        if (animationTimer == 0.0f)
            isInAnimation = false;
    }
}
