using UnityEngine;
using System.Collections;

public class InvertedAutomatedMovingPlatformScript : MonoBehaviour {

    public float minIncrement;
    public float maxIncrement;
    public enum axisEnum { x, y, z };
    public axisEnum axis;
    public float speed;
    public float cooldownStart = 0;
    public float cooldownEnd = 0;
    private Vector3 initialPosition;
    private WorldControlerScript worldControler;
    private Vector3 relPosition;
    private Vector3[] translations;
    private bool isAtEnd = false;

    // Use this for initialization
    void Start()
    {
        initialPosition = gameObject.transform.position;
        worldControler = GameObject.Find("GameWorld").GetComponent<WorldControlerScript>();
        relPosition.Set(0, 0, 0);
        translations = new Vector3[3];
        translations[0].Set(speed, 0, 0);
        translations[1].Set(0, speed, 0);
        translations[2].Set(0, 0, speed);

    }

    IEnumerator CooldownStart()
    {
        yield return new WaitForSeconds(cooldownStart);
        isAtEnd = false;
    }

    IEnumerator CooldownEnd()
    {
        yield return new WaitForSeconds(cooldownEnd);
        isAtEnd = true;
    }


    // Update is called once per frame
    void Update()
    {

        if (isAtEnd)
        {

            if (relPosition[(int)axis] < minIncrement)
            {
                gameObject.transform.Translate(translations[(int)axis] * Time.deltaTime);
                relPosition = gameObject.transform.position + initialPosition;
            }
            else
            {
                StartCoroutine(CooldownStart());
            }
        }
        else
        {
            if (relPosition[(int)axis] > maxIncrement * -1)
            {
                gameObject.transform.Translate(-1 * translations[(int)axis] * Time.deltaTime);
                relPosition = gameObject.transform.position + initialPosition;
            }
            else
            {
                StartCoroutine(CooldownEnd());
            }
        }
    }

}
