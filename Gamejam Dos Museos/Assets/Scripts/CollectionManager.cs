using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] BaitBehaviour _gancho = null;
    [SerializeField] private Text textoPuntaje;
    [SerializeField] private Text textoObjetos;
    [SerializeField] private AudioSource sonidoBasura;
    [SerializeField] private AudioSource sonidoPez;
    [SerializeField] private AudioSource sonidoTesoro;

    private string[] _frasesPeces = new string[3];

    private int _cantPeces;
    private int objetosRecuperados;
    private int _score;

    private void Start()
    {
        _frasesPeces[0] = "¡No, Carnomauro! A ese pez le quedaban 3 días para jubilarse.";
        _frasesPeces[1] = "¿Cuándo te vas a dar cuenta, Carnomauro, que tus acciones tienen consecuencias?";
        _frasesPeces[2] = "Wow. ¿Te dicen buque chino? Porque estás arrasando con la población marítima.";

        objetosRecuperados = 0;
        PlayerStats.set(0);
        textoPuntaje.text = "Puntaje: " + PlayerStats.Points;
        textoObjetos.text = "Objetos recuperados: " + objetosRecuperados + "/7";
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

                    sonidoTesoro.Play();

                    objetosRecuperados++;

                    textoObjetos.text = "Objetos recuperados: " + objetosRecuperados + "/7";
                    if (objetosRecuperados == 7)
                    {

                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
                else if (toCollect.CompareTag("Basura"))
                {
                    sonidoBasura.Play();
                }
                else if (toCollect.CompareTag("Pescado"))
                {
                    sonidoPez.Play();
                }
                PlayerStats.sumarPuntos(toCollect.GetComponent<ObjetoMovil>().GetPuntos());
                Destroy(toCollect);
                _gancho.setGrabbed(null);
            }
        }

        textoPuntaje.text = "Puntaje: " + PlayerStats.Points;
    }

}
