using UnityEngine;
using System.Collections;

public class JumperScript : MonoBehaviour {
	
	public float jumpCooldown = 1.0f;
	private float jumpTimer = -1.0f;
	private bool _isOnGround = true;
    private ArrayList collider_list;
	
	void Start () 
	{
        collider_list = new ArrayList();
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
        if (collider_list.Count == 0)
            cooldown();

        collider_list.Add(_other);
		_isOnGround = true;
		
    }
 
    void OnTriggerExit ( Collider  _other )
    {
        collider_list.Remove(_other);
        
        if(collider_list.Count == 0)
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
