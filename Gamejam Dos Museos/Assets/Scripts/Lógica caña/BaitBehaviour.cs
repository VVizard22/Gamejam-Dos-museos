using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitBehaviour : MonoBehaviour
{
    [SerializeField] float _impulse;
    private Rigidbody2D _rigidbody;

    public GameObject _grabbed { get; private set; } = null;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveBait(int direction)
    {
        Vector2 _forceDirection = new Vector2(0, direction);
        _rigidbody.position += new Vector2(0, direction * _impulse * Time.deltaTime);
    }

    public bool TryToCatch(GameObject target)
    {
        bool r = false;
        if(_grabbed == null){
            _grabbed = target;
            r = true;
        }
        return r;
    }

    public void setGrabbed(GameObject g) => _grabbed = g;
}
