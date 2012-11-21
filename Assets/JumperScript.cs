using UnityEngine;
using System.Collections;

public class JumperScript : MonoBehaviour {
	
	public float jumpCooldown = 1.0f;
	private float jumpTimer = -1.0f;
	private bool _isOnGround = true;
	
	void Start () 
	{
	
	}
	
	public bool isOnGround()
	{
		return _isOnGround;
	}
	
	public bool canJump()
	{
		if(jumpTimer < 0.0f && _isOnGround)
			return true;
		
		return false;
	}
	
	public void cooldown()
	{
		jumpTimer = jumpCooldown;
	}
	
    void OnTriggerEnter ( Collider  _other )
    {
		_isOnGround = true;
		cooldown();
    }
 
    void OnTriggerExit ( Collider  _other )
    {
		_isOnGround = false;

    }
	void Update()
	{
		float dTime = Time.deltaTime;
		if(jumpTimer >= dTime)
			jumpTimer -= dTime;
		else jumpTimer = -1.0f;
		
		Debug.Log("jumpTimer : " + jumpTimer + ", _isOnGround : "+ _isOnGround + ", canJump() : " + canJump());
	}
}
