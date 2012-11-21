using UnityEngine;
using System.Collections;

public class Controler : MonoBehaviour {
	
	
    public float speed = 1.0f;
	public float backSpeed = 0.5f;
    public float jump = 1.0f;
	public float mouseSpeed = 1.0f;
    public float maxSpeed = 30.0f;
	private Vector3 lastpos;
    private float incl;
    private float rot_Y;
	private const float baseforce = 1500;
	JumperScript jumpHandler = null;
	
	
	void Start () {
        incl = 0.0f;
        rot_Y = 0.0f;
        lastpos = Input.mousePosition;
		jumpHandler = GetComponentInChildren<JumperScript>();
	}
	
	private float modulof(float a, float b)
	{
		return (a-b*Mathf.Floor(a/b));
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
		transform.FindChild("Light").Rotate(Vector3.right,inclInc);
		
	}
	
	private bool isOnGround()
	{
		if(!jumpHandler)
			return true;
		else return jumpHandler.isOnGround();
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
		
		vforce = Vector3.Normalize(vforce) * force * baseforce * dTime;
		
		if(!isOnGround())
			vforce = vforce*0.5f;
		
        if(Input.GetKeyDown(KeyCode.Space) && jumpHandler.canJump())
		{
            vforce += Vector3.up * jump;
			jumpHandler.cooldown();
		}
		
		
		/*Vector3 actual_velocity = rigidbody.velocity;
		
		
		actual_velocity += vforce/rigidbody.mass*Time.deltaTime;
		
		if(actual_velocity.sqrMagnitude > 30.0f)
		{
			actual_velocity = Vector3.Normalize(actual_velocity)*30.0f;
			actual_velocity -= rigidbody.velocity;
			vforce = actual_velocity/Time.deltaTime*rigidbody.mass;
		}*/
		
		
		
		
		vforce = transform.rotation*vforce;
		
		
		
		rigidbody.AddForce(vforce);
		
		
		
		//Debug.Log("velocity : " + rigidbody.velocity.sqrMagnitude + ", (" + rigidbody.velocity.ToString() + ")");
		
		
			
	}
}
