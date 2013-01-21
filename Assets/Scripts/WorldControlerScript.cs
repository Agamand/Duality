#define TRUC

using UnityEngine;
using System.Collections;

public class WorldControlerScript : MonoBehaviour {
	



	int current_World = 0;
	// Use this for initialization
	GameObject world1;
	GameObject world2;
	public void switchWorld()
	{
		setWorld(current_World == 0 ? 1 : 0);
	}
	
	public void setWorld(int world)
	{
		current_World = world;

        Physics.IgnoreLayerCollision(10, 8, current_World != 0);
        Physics.IgnoreLayerCollision(10, 9, current_World == 0);
		Debug.Log("current_world : " + current_World);
		int childCount = world1.transform.GetChildCount();
		for(int i = 0; i < childCount; i++) 
		{
            Transform go = world1.transform.GetChild(i);
            if (go.GetComponent<Renderer>() != null)
            {
                Color c = go.renderer.material.color;
                c.a = current_World == 0 ? 1.0f : 0.3f;
                go.renderer.material.color = c;
            }
		}

        childCount = world2.transform.GetChildCount();
		for(int i = 0; i < childCount; i++) 
		{ 
            Transform go = world2.transform.GetChild(i);
            if (go.GetComponent<Renderer>() != null)
            {
                Color c = go.renderer.material.color;
                c.a = current_World != 0 ? 1.0f : 0.3f;
                go.renderer.material.color = c;
            }
		}
		
	}

    public GameObject getCurrentWorld()
    {
        return current_World == 0 ? world1 : world2;
    }

    public int getCurrentWorldNumber()
    {
        return current_World == 0 ? 0 : 1;
    }

	void Start () 
	{
		world1 = GameObject.Find("World1");
		world2 = GameObject.Find("World2");
        Physics.IgnoreLayerCollision(8, 9);
		//Debug.Log(world.ToString());
		setWorld(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
