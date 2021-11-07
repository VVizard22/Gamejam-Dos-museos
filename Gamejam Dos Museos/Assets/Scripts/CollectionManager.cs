using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] BaitBehaviour _gancho = null;
    private string[] _frasesPeces = new string[3];

    private int _cantPeces;
    private int _score = 0;
    private void Start()
    {
        _frasesPeces[0] = "¡No, Carnomauro! A ese pez le quedaban 3 días para jubilarse.";
        _frasesPeces[1] = "¿Cuándo te vas a dar cuenta, Carnomauro, que tus acciones tienen consecuencias?";
        _frasesPeces[2] = "Wow. ¿Te dicen buque chino? Porque estás arrasando con la población marítima.";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Anzuelo"))
        {
            GameObject toCollect = _gancho._grabbed;
            if (toCollect != null)
            {
                if (toCollect.CompareTag("Trofeo"))
                {
                    Debug.Log("Lore de: " + toCollect.name);
                }
                else if (toCollect.CompareTag("Pescado") && _cantPeces < _frasesPeces.Length)
                {
                    Debug.Log(_frasesPeces[_cantPeces]);
                    _cantPeces++;
                }
                _score += toCollect.GetComponent<ObjetoMovil>().GetPuntos();
                Destroy(toCollect);
                _gancho.setGrabbed(null);
            }
        }
        
        Debug.Log(_score);
    }

}
