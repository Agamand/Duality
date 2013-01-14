using UnityEngine;
using System.Collections;

public class LaserBeamScript : MonoBehaviour {

    GameObject []laserBeams;
    GameObject currentWorld;
    WorldControlerScript worldController;

	// Use this for initialization
	void Start () {
        laserBeams = GameObject.FindGameObjectsWithTag("laserBeam");
        worldController = GameObject.Find("GameWorld").GetComponent<WorldControlerScript>();
        currentWorld = worldController.getCurrentWorld();
	}
	
	// Update is called once per frame
	void Update () {
            currentWorld = worldController.getCurrentWorld();
            for (int i = 0; i < laserBeams.Length; i++)
            {
                if (currentWorld.layer == laserBeams[i].layer)
                {
                    laserBeams[i].SetActive(true);

                }
                if (currentWorld.layer != laserBeams[i].layer)
                {
                    laserBeams[i].SetActive(false);
                }

            }          
	}
}
