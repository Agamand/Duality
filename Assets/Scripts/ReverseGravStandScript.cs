using UnityEngine;
using System.Collections;

public class ReverseGravStandScript : MonoBehaviour {

    public Vector3 _gravity = new Vector3();
    private ControlerScript player = null;
    void OnTriggerEnter(Collider _other)
    {
        if (!player)
        {
            GameObject gamePlayer = GameObject.Find("Player");
            player = gamePlayer.GetComponent<ControlerScript>();
        }
        Vector3 from = Physics.gravity;
        Vector3 to = Physics.gravity.magnitude * _gravity;
        player.changeGravity(-Vector3.Normalize(from),-Vector3.Normalize(to));
        Physics.gravity = to;
        Debug.Log("Change Gravity from " + from.ToString() + " to " + to.ToString());
    }
}
