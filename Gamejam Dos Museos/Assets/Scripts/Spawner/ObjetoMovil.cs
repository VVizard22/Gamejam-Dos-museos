using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjetoMovil : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int _puntos;
    private Rigidbody2D rb;
    public GameObject fueGeneradoPor;
    public int posEnArray;
    public bool fuePescado;

    private bool _grabbed;
    GameObject _gancho;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        fuePescado = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Anzuelo"))
        {
            _gancho = collision.transform.gameObject;
            _grabbed = _gancho.GetComponent<BaitBehaviour>().TryToCatch(gameObject);
        }
    }

    private void Update()
    {
        if (_grabbed)
        {
            rb.position = _gancho.GetComponent<Rigidbody2D>().position;
        }
    }

    private void OnBecameInvisible()
    {
        if (CompareTag("Trofeo") && !_grabbed)
        {
            fueGeneradoPor.GetComponent<Generador>().reincorporarCosa(posEnArray);
        }
        Destroy(this.gameObject);
    }

    public void Free()
    {
        _grabbed = false;
        rb.velocity = new Vector2(-speed, 0);
    }

    public int GetPuntos(){
        int r = _puntos;
        return r;
    }
}

