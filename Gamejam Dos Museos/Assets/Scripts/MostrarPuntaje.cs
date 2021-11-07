using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarPuntaje : MonoBehaviour
{
    [SerializeField] private Text texto;
    // Start is called before the first frame update

    void Start()
    {
        texto.text = "" + PlayerStats.Points;
    }
}
