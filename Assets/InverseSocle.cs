using UnityEngine;
using System.Collections;

public class InverseSocle : MonoBehaviour {

    public Vector3 _gravity = new Vector3();
    private Controler player = null;
    void OnTriggerEnter(Collider _other)
    {
        if (!player)
        {
            GameObject gamePlayer = GameObject.Find("Player");
            player = gamePlayer.GetComponent<Controler>();
        }
        Vector3 from = Physics.gravity;
        Vector3 to = Physics.gravity.magnitude * _gravity;
        player.changeGravity(-Vector3.Normalize(from),-Vector3.Normalize(to));
        Physics.gravity = to;
        Debug.Log("Change Gravity from " + from.ToString() + " to " + to.ToString());
    }
}
