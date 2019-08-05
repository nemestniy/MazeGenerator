using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        var velocityXZ = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.y);
        _rigidbody.velocity = transform.TransformDirection(velocityXZ) * _speed * Time.deltaTime;
    }

    public void Rotate(float turn)
    {
        transform.Rotate(new Vector3(0, turn, 0) * _speed * Time.deltaTime);
    }
}
