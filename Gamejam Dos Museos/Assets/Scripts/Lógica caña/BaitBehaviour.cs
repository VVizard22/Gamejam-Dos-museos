using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitBehaviour : MonoBehaviour
{
    [SerializeField] float _impulse;
    private Rigidbody2D _rigidbody;
    private Vector2 _lastDirectionImpulse;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveBait(int direction)
    {
        Vector2 _forceDirection = new Vector2(0, direction);
        _lastDirectionImpulse = _forceDirection;
        _rigidbody.position += new Vector2(0, direction * _impulse * Time.deltaTime);
    }

}
