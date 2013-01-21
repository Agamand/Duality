using UnityEngine;
using System.Collections;

public class LocalGravityScript : MonoBehaviour {

    static float gravity_acceleration = 9.8f;
    public Vector3 startDir = new Vector3(0f, -1.0f, 0f);

    Vector3 gravity;
    ConstantForce _constForce = null;
    private GameObject _gameobject = null;
    private ControlerScript _player = null;
    private Rigidbody _body = null;
    void Start()
    {
        _gameobject = this.gameObject;
        _body = this.rigidbody;

        if (!_body)
        {
            Debug.Log("Warning : LocalGravity is attach to a gameobject without rigidbody !");
            return;
        }


        _body.useGravity = false;

        _constForce = _gameobject.AddComponent<ConstantForce>();
        setGravityDir(startDir);
        _player = GetComponent<ControlerScript>();
    }

    public void setGravityDir(Vector3 newGravity)
    {
        float mass = _body ? _body.mass : 1.0f;

        Vector3 old = Vector3.Normalize(gravity);
        Debug.Log("newGravity = " + newGravity.ToString());
        gravity = newGravity;
        Debug.Log("Gravity = " + gravity.ToString());
        gravity *= gravity_acceleration;
        Debug.Log("Gravity = " + gravity.ToString());
        _constForce.force = gravity * mass;

        Debug.Log("Change Gravity Dir, from " + old.ToString() + " to " + newGravity.ToString());
        if (_player)
            _player.OnChangeGravity(old, newGravity);

    }

    void Update() { }
}
