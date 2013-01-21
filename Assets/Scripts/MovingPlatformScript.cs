using UnityEngine;
using System.Collections;

public class MovingHPlatformScript : MonoBehaviour {

    public float minIncrement;
    public float maxIncrement;
    public enum axisEnum { x, y, z };
    public axisEnum axis;
    private float relPos;
    public float decrement;
    private Vector3 initialPosition;
    private WorldControlerScript worldControler;
    private int currentWorldNumber;
    private Vector3 relPosition;
    private Vector3[] translations;


	// Use this for initialization
	void Start () {
        initialPosition = gameObject.transform.position;
        worldControler = GameObject.Find("GameWorld").GetComponent<WorldControlerScript>();
        currentWorldNumber = worldControler.getCurrentWorldNumber();
        /*minIncrement = 10.0f;
        maxIncrement = 10.0f;*/
        //axis = 'x';       
        //decrement = 0.2f;
        relPosition.Set(0, 0, 0);
        translations = new Vector3[3];
        translations[0].Set(decrement, 0, 0);
        translations[1].Set(0, decrement, 0);
        translations[2].Set(0, 0, decrement);
	}
	
	// Update is called once per frame
	void Update () {
        currentWorldNumber = worldControler.getCurrentWorldNumber();
        if (currentWorldNumber == 0)
        {
           
                if (relPosition.x < minIncrement && relPosition.y < minIncrement && relPosition.z < minIncrement)
                {
                    gameObject.transform.Translate(translations[(int)axis]);
                }

            relPosition = gameObject.transform.position + initialPosition;
        }
        else
        {
            if (relPosition.x > maxIncrement * -1 && relPosition.y > maxIncrement * -1 && relPosition.z > maxIncrement * -1)
            {
                gameObject.transform.Translate(-1*translations[(int)axis]);
                relPosition = gameObject.transform.position + initialPosition;
            }
        }
	}
}
