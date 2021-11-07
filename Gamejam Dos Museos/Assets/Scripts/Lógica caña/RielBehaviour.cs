using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script sera añadido al GameObject Riel
public class RielBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _riel;
    [SerializeField] GameObject _manija;
    [SerializeField] BaitBehaviour _anzuelo;
    
    private Vector2 _centerPosition;
    private Vector2 _lastPosition;

    private float _distanceToCenter;

    private bool _mouseOver;
    private bool _isDragging;

    private Camera _mainCamera;

    //El anzuelo tiene 3 estados (0 = quieto, 1 = subiendo, -1 = bajando)
    private int _movement = 0;
    void Awake()
    {
        //transfor..position devuelve la posicion del centro del riel
        _centerPosition = _riel.transform.localPosition;

        _lastPosition = transform.localPosition;

        /*Se cashea la camara principal del juego para ya tener una referencia
           y no necesitar correr un algoritmo de busqueda cada vez*/
        _mainCamera = Camera.main;

        _mouseOver = false;
        _isDragging = false;
        _distanceToCenter = Vector2.Distance(_centerPosition, _lastPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (_mouseOver)
        {
            Vector2 _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            /*
             * Si el mouse se encuentra sobre la manivela y se registra un click, entonces
             * se calcula el arrastre de la manivela y se dice que la esta arrastrando
             */
            if (Input.GetMouseButton(0))
            {
                _isDragging = true;
                //Debug.Log("MovementStart");
                //Vector2 _manivelaLastPos = _manivela.transform.position;


                //float _distanceToMouse = Vector2.Distance(_mousePosition, _centerPosition);
                Vector2 _directionVector = _centerPosition - _mousePosition;
                _directionVector.Normalize();
                CalculateDirection(_directionVector);

                transform.localPosition = _centerPosition - _directionVector * _distanceToCenter;

                Vector3 target = transform.position;
                target.z = 0f;

                target.x -= _manija.transform.position.x;
                target.y -= _manija.transform.position.y;

                float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;


                _manija.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                if (_movement != 0)
                    _anzuelo.MoveBait(_movement);
            }

            /*
             * Si se estaba arrastrando y luego se levanta el boton del mouse, entonces 
             * se resetean las variables para que vuelva al estado principal y se deje de
             * arrastrar
             */
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                _mouseOver = false;
                _movement = 0;
            }
        }
        //Debug.Log(_movement);             
    }

    private void CalculateDirection(Vector2 directionVector)
    {
        Vector2 _actualPosition = transform.position;
        //_actualPosition = _centerPosition - _actualPosition;
        //_actualPosition.Normalize();
        if (_actualPosition != _lastPosition)
        {
            if (directionVector.x < 0 && directionVector.y < 0)
            {
                //Debug.Log("Cuadrante 1");
                if (_lastPosition.y < _actualPosition.y)
                    _movement = -1;
                else
                    _movement = 1;
            }
            else if (directionVector.x > 0 && directionVector.y < 0)
            {
                //Debug.Log("Cuadrante 2");
                if (_lastPosition.y > _actualPosition.y)
                    _movement = -1;
                else
                    _movement = 1;
            }
            else if (directionVector.x > 0 && directionVector.y > 0)
            {
                //Debug.Log("Cuadrante 3");
                if (_lastPosition.y > _actualPosition.y)
                    _movement = -1;
                else
                    _movement = 1;
            }
            else if (directionVector.x < 0 && directionVector.y > 0)
            {
                //Debug.Log("Cuadrante 4");
                if (_lastPosition.y < _actualPosition.y)
                    _movement = -1;
                else
                    _movement = 1;
            }
        }
        else
            _movement = 0;
        _lastPosition = _actualPosition;
    }

    void OnMouseEnter()
    {
        /*
         *   Si el mouse pasa por encima de la manivela se
         * activa la variable que permite agarrarla y arrastrarla
         */
        _mouseOver = true;
    }

    void OnMouseExit()
    {
        /*
         *          Si no se procede a agarrar la manivela
         *  se desactiva la variable que permite agarrarla y arrastrarla
         */
        if (!_isDragging)
        {
            _mouseOver = false;
        }
    }
}
