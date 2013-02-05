using UnityEngine;
using System.Collections;

public class BallStandScript : MonoBehaviour {

    private bool m_IsOn = false;
    private Light[] m_Lights;


	// Use this for initialization
	void Start () {
        m_Lights = new Light[2];
        m_Lights[0] = gameObject.transform.FindChild("leftLight").light;
        m_Lights[1] = gameObject.transform.FindChild("rightLight").light;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FlickOn()
    {
        Debug.Log("Allumez bitches!!!!");
        m_IsOn = true;
        foreach (Light l in m_Lights)
                l.color = Color.green;
    }

    public void  FlickOff()
    {
        Debug.Log("On éteint tout les débiles");
        m_IsOn = false;
        foreach (Light l in m_Lights)
            l.color = Color.red;
    }

    public bool GetState()
    {
        return m_IsOn;
    }
}
