using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour {

    public float minIncrement;
    public float maxIncrement;
    public enum axisEnum { x, y, z };
    public axisEnum axis;
    public float speed;
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
        relPosition.Set(0, 0, 0);
        translations = new Vector3[3];
        translations[0].Set(speed, 0, 0);
        translations[1].Set(0, speed, 0);
        translations[2].Set(0, 0, speed);
	}
	
	// Update is called once per frame
	void Update () {
        currentWorldNumber = worldControler.getCurrentWorldNumber();
    
        if (currentWorldNumber == 0) {
            if (relPosition[(int)axis] < minIncrement) {
                gameObject.transform.Translate(translations[(int)axis]);
                relPosition = gameObject.transform.position + initialPosition;
            }
        }
        else {
            if (relPosition[(int)axis] > maxIncrement * -1) {
                gameObject.transform.Translate(-1*translations[(int)axis]);
                relPosition = gameObject.transform.position + initialPosition;
            }
        }
	}
}
