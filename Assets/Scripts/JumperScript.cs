using UnityEngine;
using System.Collections;

public class JumperScript : MonoBehaviour {
	
	public float jumpCooldown = 1.0f;
	private float jumpTimer = -1.0f;
	private bool _isOnGround = true;
    private ArrayList collider_list;
    private int jumpCharge = 1;
    private int maxJumpCharge = 2;
    private WorldControlerScript worldControler;

	
	void Start () 
	{
        collider_list = new ArrayList();
        worldControler = GameObject.Find("GameWorld").GetComponent<WorldControlerScript>();
	}
	
	public bool isOnGround()
	{
		return _isOnGround;
	}
	
	public bool canJump()
	{
		if(jumpTimer < 0.0f && _isOnGround | jumpCharge > 0)
			return true;
		
		return false;
	}

    public void onJump()
    {
        jumpCharge--;
        cooldown();
    }
	
	public void cooldown()
	{
		jumpTimer = jumpCooldown;
	}
	
    void OnTriggerEnter ( Collider  _other )
    {
        if (collider_list.Count == 0)
        {
            cooldown();
            jumpCharge = maxJumpCharge;
        }
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
        if (worldControler.getCurrentWorldNumber() == 0 && jumpCharge > 1 && _isOnGround)
            jumpCharge = 1;
        else if (worldControler.getCurrentWorldNumber() == 0 && jumpCharge > 0 && !_isOnGround)
            jumpCharge = 0;
        else if (worldControler.getCurrentWorldNumber() == 1 && _isOnGround)
            jumpCharge = 2;

		float dTime = Time.deltaTime;
		if(jumpTimer >= dTime)
			jumpTimer -= dTime;
		else jumpTimer = -1.0f;
		
		Debug.Log("jumpTimer : " + jumpTimer + ", _isOnGround : "+ _isOnGround + ", canJump() : " + canJump()+ ", charge : " + jumpCharge);
	}
}
