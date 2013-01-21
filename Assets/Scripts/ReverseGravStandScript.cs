using UnityEngine;
using System.Collections;

public class ReverseGravStandScript : MonoBehaviour {

    public Vector3 _gravity = new Vector3();

    void OnTriggerEnter(Collider _other)
    {
        LocalGravityScript gravityScript = _other.GetComponent<LocalGravityScript>();
        if (gravityScript == null)
            return;

        gravityScript.setGravityDir(_gravity);
    }
}
